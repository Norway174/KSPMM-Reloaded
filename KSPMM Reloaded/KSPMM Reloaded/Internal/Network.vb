Imports System.Net
Imports System.IO
Namespace Internal
    Public Structure QueuedDownload
        Sub New(ByVal _Download As Download, Optional ByVal _Priority As Priority = Network.Priority.Normal)
            Download = _Download
            Priority = _Priority
        End Sub
        Public Download As Download
        Public Priority As Priority
    End Structure
    Public Module Network
        Public NetworkUC As New Network_UC
        Public ReadOnly Property Plugin As IPlugin
            Get
                Dim p As New Plugin ' IPlugin
                p.Name = "Downloads"
                p.Version = New Version(1, 0, 0, 0)
                p.Control = NetworkUC
                Return p
            End Get
        End Property
        Public Enum Priority
            Lowest = 0
            Low = 1
            BelowNormal = 2
            Normal = 3
            AboveNormal = 4
            High = 5
            Highest = 6
        End Enum
        Public Downloads As New List(Of QueuedDownload)
        Public Sub Add(ByRef Download As Download)
            AddHandler Download.Complete, AddressOf DownloadComplete
            Dim q As New QueuedDownload(Download, Download.Priority)
            NetworkUC.AddDownload(q, NetworkUC.DownloadPanel)
            Downloads.Add(q)
        End Sub
        Public Sub DownloadComplete(ByVal Download As QueuedDownload, ByVal Success As Boolean, ByVal ErrorCode As Integer)

        End Sub
    End Module
    Public Class Download
        Private Request As HttpWebRequest
        Private Response As HttpWebResponse
        Private Timer As New Stopwatch
        Private _Stop As Boolean = False
        Private _Pause As Boolean = False
        Public ReadOnly Property NetworkStream As Stream
            Get
                Return Response.GetResponseStream
            End Get
        End Property
        Public ReadOnly Property Length As ULong
            Get
                Return Response.ContentLength
            End Get
        End Property


        Sub New(ByVal URI As Uri, ByVal DownloadLocation As String, Optional Priority As Priority = Priority.Normal)
            RaiseEvent StatusChange("Idle")
            Try
                _URI = URI
                _Priority = Priority
                _Stream = New FileStream(DownloadLocation, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None)
                _Location = DownloadLocation
                Request = WebRequest.Create(URI)
                Response = Request.GetResponse
            Catch ex As Exception
                Try
                    RaiseEvent Complete(New QueuedDownload(Me), False, Response.StatusCode)
                    RaiseEvent StatusChange("Failed")
                    Exit Sub
                Catch ex1 As Exception
                    RaiseEvent Complete(New QueuedDownload(Me), False, 0)
                    RaiseEvent StatusChange("Failed")
                    Exit Sub
                End Try
            End Try
        End Sub
        Public Sub Start(Optional ByVal BufferSize As ULong = 4096)
            RaiseEvent StatusChange("Downloading...")
            _isDownloading = True
            Dim readings As Byte = 0
            Do Until _Stop Or _Pause
                Timer.Start()

                Dim readBytes(BufferSize - 1) As Byte
                Dim bytesread As ULong = Response.GetResponseStream.Read(readBytes, 0, BufferSize)

                If bytesread = 0 Then
                    Timer.Stop()
                    Exit Do
                End If

                _Position += bytesread
                _Stream.Write(readBytes, 0, bytesread)

                Timer.Stop()

                readings += 1
                If readings >= 5 Then
                    _Speed = ((4096 * readings) / (Timer.ElapsedMilliseconds / 1000)) * 1024
                    Timer.Reset()
                    readings = 0
                End If
            Loop
            _isDownloading = False

            If _Pause Then
                Exit Sub
                RaiseEvent StatusChange("Paused")
            End If

            Response.GetResponseStream.Close()
            _Stream.Close()

            If _Stop Then
                File.Delete(Location)
                RaiseEvent Complete(New QueuedDownload(Me, Priority), False, -1)
                RaiseEvent StatusChange("Cancelled")
            Else
                RaiseEvent Complete(New QueuedDownload(Me, Priority), True, 0)
                RaiseEvent StatusChange("Finished")
            End If
        End Sub
        Public Sub Pause()
            RaiseEvent StatusChange("Pausing...")
            _Pause = True
        End Sub
        Public Sub Cancel()
            RaiseEvent StatusChange("Stopping...")
            _Stop = True
        End Sub
        Public Sub Unpause()
            _Pause = False

            _Stream = New FileStream(Location, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None)
            _Stream.Position = Position
            Request.AddRange(CLng(Position))
            Request = WebRequest.Create(URI)
            Response = Request.GetResponse
        End Sub


        Public Event Complete(ByVal Download As QueuedDownload, ByVal Success As Boolean, ByVal ErrorCode As Integer)
        Public Event StatusChange(ByVal Status As String)

        Private _Speed As Double = -1
        Public ReadOnly Property Speed As Double
            Get
                Return _Speed
            End Get
        End Property

        Private _Position As ULong = 0
        Public ReadOnly Property Position As ULong
            Get
                Return _Position
            End Get
        End Property

        Private _Location As String = ""
        Public ReadOnly Property Location As String
            Get
                Return _Location
            End Get
        End Property

        Private _Stream As FileStream
        Public ReadOnly Property FileStream As FileStream
            Get
                Return _Stream
            End Get
        End Property

        Private _URI As Uri
        Public ReadOnly Property URI As Uri
            Get
                Return _URI
            End Get
        End Property

        Private _isDownloading As Boolean = False
        Public ReadOnly Property isDownloading As Boolean
            Get
                Return isDownloading
            End Get
        End Property

        Private _Priority As Priority
        Public ReadOnly Property Priority As Priority
            Get
                Return _Priority
            End Get
        End Property
    End Class
End Namespace
