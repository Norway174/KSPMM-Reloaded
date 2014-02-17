Imports System.Net
Imports System.IO
Namespace Internal
    Public Module Network
        Public NetworkUC As New Network_UC
        Private _downloads As UInteger = 0
        Public ReadOnly Property ActiveDownloads As UInteger
            Get
                Return _downloads
            End Get
        End Property
        Public ReadOnly Property MaxSimultaneousDownloads As UShort
            Get
                Return 4 'When settings are introduced this will be replaced
            End Get
        End Property
        Public ReadOnly Property Plugin As IPlugin
            Get
                Dim p As New Plugin ' IPlugin
                p.Name = "Downloads"
                p.Version = New Version(1, 0, 0, 0)
                p.Control = NetworkUC
                p.TypeOfPlugin = KSPMM_Reloaded.Plugin.PluginType.TabbedUserControl
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
        Public Downloads As New List(Of Download)
        Public Sub Add(ByRef Download As Download)
            AddHandler Download.Complete, AddressOf DownloadComplete
            NetworkUC.AddDownload(Download, NetworkUC.DownloadPanel)
            Downloads.Add(Download)
        End Sub
        Public Event CheckDownload(ByVal Download As Download)
        Public Sub DownloadComplete(ByVal Download As Download, ByVal Success As Boolean, ByVal ErrorCode As Integer)
            _downloads -= 1
            RaiseEvent CheckDownload(Download)
            CheckDownloads()

        End Sub
        Public Sub DownloadReady(ByVal Download As Download)
            RaiseEvent CheckDownload(Download)
            CheckDownloads()
        End Sub

        Private Structure cdownload
            Sub New(ByRef _Control As Control, ByRef _Download As Download)
                Control = _Control
                Download = _Download
            End Sub
            Public Control As NetworkDownload_UC
            Public Download As Download
        End Structure
        Public Sub CheckDownloads()
            Dim lowest As New List(Of cdownload)
            Dim low As New List(Of cdownload)
            Dim bnormal As New List(Of cdownload)
            Dim normal As New List(Of cdownload)
            Dim anormal As New List(Of cdownload)
            Dim high As New List(Of cdownload)
            Dim highest As New List(Of cdownload)

            For Each c As NetworkDownload_UC In NetworkUC.DownloadPanel.Controls
                Select Case c.Download.Priority
                    Case Priority.Lowest : lowest.Add(New cdownload(c, c.Download))
                    Case Priority.Low : low.Add(New cdownload(c, c.Download))
                    Case Priority.BelowNormal : bnormal.Add(New cdownload(c, c.Download))
                    Case Priority.Normal : normal.Add(New cdownload(c, c.Download))
                    Case Priority.AboveNormal : anormal.Add(New cdownload(c, c.Download))
                    Case Priority.High : high.Add(New cdownload(c, c.Download))
                    Case Priority.Highest : highest.Add(New cdownload(c, c.Download))
                End Select
            Next

            For Each d As cdownload In highest
                If ActiveDownloads < MaxSimultaneousDownloads AndAlso d.Download.isFinished = False AndAlso d.Download.Failed = False Then
                    For Each c As NetworkDownload_UC In NetworkUC.DownloadPanel.Controls
                        If c Is d.Control Then
                            _downloads += 1
                            c.Download.Start()
                        End If
                    Next
                End If
            Next
            For Each d As cdownload In highest
                If ActiveDownloads < MaxSimultaneousDownloads AndAlso d.Download.isFinished = False AndAlso d.Download.Failed = False Then
                    For Each c As NetworkDownload_UC In NetworkUC.DownloadPanel.Controls
                        If c Is d.Control Then
                            _downloads += 1
                            c.Download.Start()
                        End If
                    Next
                End If
            Next
            For Each d As cdownload In high
                If ActiveDownloads < MaxSimultaneousDownloads AndAlso d.Download.isFinished = False AndAlso d.Download.Failed = False Then
                    For Each c As NetworkDownload_UC In NetworkUC.DownloadPanel.Controls
                        If c Is d.Control Then
                            _downloads += 1
                            c.Download.Start()
                        End If
                    Next
                End If
            Next
            For Each d As cdownload In anormal
                If ActiveDownloads < MaxSimultaneousDownloads AndAlso d.Download.isFinished = False AndAlso d.Download.Failed = False Then
                    For Each c As NetworkDownload_UC In NetworkUC.DownloadPanel.Controls
                        If c Is d.Control Then
                            _downloads += 1
                            c.Download.Start()
                        End If
                    Next
                End If
            Next
            For Each d As cdownload In normal
                If ActiveDownloads < MaxSimultaneousDownloads AndAlso d.Download.isFinished = False AndAlso d.Download.Failed = False Then
                    For Each c As NetworkDownload_UC In NetworkUC.DownloadPanel.Controls
                        If c Is d.Control Then
                            _downloads += 1
                            c.Download.Start()
                        End If
                    Next
                End If
            Next
            For Each d As cdownload In bnormal
                If ActiveDownloads < MaxSimultaneousDownloads AndAlso d.Download.isFinished = False AndAlso d.Download.Failed = False Then
                    For Each c As NetworkDownload_UC In NetworkUC.DownloadPanel.Controls
                        If c Is d.Control Then
                            _downloads += 1
                            c.Download.Start()
                        End If
                    Next
                End If
            Next
            For Each d As cdownload In low
                If ActiveDownloads < MaxSimultaneousDownloads AndAlso d.Download.isFinished = False AndAlso d.Download.Failed = False Then
                    For Each c As NetworkDownload_UC In NetworkUC.DownloadPanel.Controls
                        If c Is d.Control Then
                            _downloads += 1
                            c.Download.Start()
                        End If
                    Next
                End If
            Next
            For Each d As cdownload In lowest
                If ActiveDownloads < MaxSimultaneousDownloads AndAlso d.Download.isFinished = False AndAlso d.Download.Failed = False Then
                    For Each c As NetworkDownload_UC In NetworkUC.DownloadPanel.Controls
                        If c Is d.Control Then
                            _downloads += 1
                            c.Download.Start()
                        End If
                    Next
                End If
            Next
        End Sub
    End Module
    Public Class Download
        Private Request As HttpWebRequest
        Private Response As HttpWebResponse
        Private Timer As New Stopwatch
        Private _Stop As Boolean = False
        Private _Pause As Boolean = False
        Public Property InstallOnDownloadCompletion As Boolean = False
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

        Sub New(ByVal URI As Uri, ByVal InstallWhenDownloaded As Boolean, ByVal DownloadLocation As String, Optional Priority As Priority = Priority.Normal)
            RaiseEvent StatusChange("Idle")
            Try
                InstallOnDownloadCompletion = InstallWhenDownloaded
                _URI = URI
                _Priority = Priority
                _Stream = New FileStream(DownloadLocation, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None)
                _Location = DownloadLocation
                Request = WebRequest.Create(URI)
                Response = Request.GetResponse
            Catch ex As Exception
                Try
                    RaiseEvent Complete(Me, False, Response.StatusCode)
                    RaiseEvent StatusChange("Failed")
                    _Failed = True
                    Exit Sub
                Catch ex1 As Exception
                    RaiseEvent Complete(Me, False, -9001)
                    RaiseEvent StatusChange("Failed")
                    _Failed = True
                    Exit Sub
                End Try
            End Try
        End Sub
        Public Sub Start(Optional ByVal BufferSize As ULong = 4096)
            Dim t As New Threading.Thread(Sub(var As Object)
                                              _Start(var)
                                          End Sub)
            t.Start(BufferSize)
        End Sub
        Private Function _Start(ByVal Buffersize As ULong)
            Try
                RaiseEvent StatusChange("Downloading...")
                _isDownloading = True
                Dim readings As Byte = 0
                Do Until _Stop Or _Pause
                    Timer.Start()

                    Dim readBytes(Buffersize - 1) As Byte
                    Dim bytesread As ULong = Response.GetResponseStream.Read(readBytes, 0, Buffersize)

                    _Position += bytesread
                    RaiseEvent ProgressUpdate((Position / Length) * 100, BasicUFL.Size(Position) & " / " & BasicUFL.Size(Length))

                    If bytesread = 0 Then
                        Timer.Stop()
                        Exit Do
                    End If

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
                _isFinished = True

                If _Pause Then
                    RaiseEvent StatusChange("Paused")
                    Return Nothing
                    Exit Function
                End If

                Response.GetResponseStream.Close()
                _Stream.Close()

                If _Stop Then
                    File.Delete(Location)
                    RaiseEvent Complete(Me, False, -1)
                    RaiseEvent StatusChange("Cancelled")
                Else
                    RaiseEvent Complete(Me, True, 0)
                    RaiseEvent StatusChange("Finished")
                End If
            Catch ex As Exception
                RaiseEvent Complete(Me, False, 0)
                RaiseEvent StatusChange("Failed - " & ex.Message)
                _Failed = True
                Return Nothing
                Exit Function
            End Try
            Return Nothing
        End Function
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


        Public Event Complete(ByVal Download As Download, ByVal Success As Boolean, ByVal ErrorCode As Integer)
        Public Event StatusChange(ByVal Status As String)
        Public Event ProgressUpdate(ByVal Progress As Short, ByVal ProgressText As String)

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

        Private _isFinished As Boolean = False
        Public ReadOnly Property isFinished As Boolean
            Get
                Return _isFinished
            End Get
        End Property

        Private _Failed As Boolean = False
        Public ReadOnly Property Failed As Boolean
            Get
                Return _Failed
            End Get
        End Property
    End Class
End Namespace
