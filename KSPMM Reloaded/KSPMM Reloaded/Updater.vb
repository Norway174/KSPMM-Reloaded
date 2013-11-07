Imports System.Net

Public Class Updater
    Dim Cancel As Boolean = False
    Dim Done As Boolean = False

    Dim mainExe As String = System.IO.Path.GetFileName(Application.ExecutablePath)
    Dim oldExeLoc As String = System.IO.Path.GetDirectoryName(mainExe)
    Dim oldExe As String = oldExeLoc & "KSPMM.Old"


    Private _passedText As String

    Public Property [PassedText]() As String
        Get
            Return _passedText
        End Get
        Set(ByVal Value As String)
            _passedText = Value
        End Set
    End Property

    Dim client As WebClient = New WebClient

    Sub StartUpdater() Handles Me.Load
        Label1.Text = "Downloading: " & _passedText

        'If My.Computer.FileSystem.FileExists(oldExe) Then My.Computer.FileSystem.DeleteFile(oldExe)
        My.Computer.FileSystem.RenameFile(mainExe, "KSPMM.Old")

        AddHandler client.DownloadProgressChanged, AddressOf client_ProgressChanged
        AddHandler client.DownloadFileCompleted, AddressOf client_DownloadCompleted
        client.DownloadFileAsync(New Uri(_passedText), mainExe)

    End Sub
    Sub CancelPressed() Handles Button1.Click
        If (MsgBox("Are you sure?", MsgBoxStyle.YesNo, "Cancel") = MsgBoxResult.Yes) Then
            Cancel = True
            client.CancelAsync()
        End If
    End Sub

    Private Sub client_ProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)
        Dim bytesIn As Double = Double.Parse(e.BytesReceived.ToString())
        Dim totalBytes As Double = Double.Parse(e.TotalBytesToReceive.ToString())
        Dim percentage As Double = bytesIn / totalBytes * 100

        ProgressBar1.Value = Int32.Parse(Math.Truncate(percentage).ToString())
        Label1.Text = percentage.ToString("#0.00") & "% (" & BytesTO(bytesIn.ToString, KB) & "KB/" & BytesTO(totalBytes.ToString, KB) & "KB)"
    End Sub

    Private Sub client_DownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        If Cancel = True Then
            MsgBox("Download Canceled")
        Else
            'My.Computer.FileSystem.DeleteFile(oldExe)
            If (MsgBox("Download Complete! Do wish to start KSPMM now?", MsgBoxStyle.YesNo, "Completed!") = MsgBoxResult.Yes) Then
                Process.Start(mainExe)
                Application.Exit()
            Else
                Application.Exit()
            End If
        End If

    End Sub

    Const KB = 1
    Const MB = 2
    Const GB = 3
    Const TB = 4
    Function BytesTO(ByVal lBytes, ByVal convertto)

        BytesTO = lBytes / (1024 ^ convertto)
    End Function

End Class