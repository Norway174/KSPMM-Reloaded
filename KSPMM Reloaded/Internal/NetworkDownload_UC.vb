Public Class NetworkDownload_UC
    Public Download As Internal.Download
    Sub New(ByVal _Download As Internal.Download)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Download = _Download
    End Sub
    Public Sub ChangeStatus(ByVal Status As String)
        DStatus.Text = Status
    End Sub

    Public Sub UpdateProgress(ByVal Progress As Short, ByVal ProgressText As String)
        DProgress.Text = ProgressText
        DProgress.Location = New Point((Me.Size.Width - DProgress.Size.Width) - 6, DProgress.Location.Y)
        DProgressBar.Value = Progress
    End Sub
End Class
