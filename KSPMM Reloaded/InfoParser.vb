Public Class InfoParser
    Public Property Name As String
    Public _nname As String
    Public Property Description As String
    Public Property Version As Version
    Public Property KSPVersion As String
    Public Property DownloadLink1 As String
    Public Property DownloadLink2 As String
    Sub New(ByVal Text As String)
        Dim name = My.Computer.FileSystem.SpecialDirectories.Temp & "\topkek"
        Dim w As New IO.StreamWriter(name)
        w.Write(Text)
        w.Close()
        Dim t As New IO.StreamReader(name)
        Dim x As Xml.XmlTextReader = New Xml.XmlTextReader(t)
        Dim prev As String = ""
        Do While x.Read()
            Select Case x.NodeType
                Case Xml.XmlNodeType.Element
                    prev = x.Name
                Case Xml.XmlNodeType.Text
                    Select Case prev
                        Case "name"
                            Name = x.Value
                        Case "description"
                            Description = x.Value
                        Case "version"
                            Version = System.Version.Parse(x.Value)
                        Case "kspversion"
                            KSPVersion = x.Value
                        Case "downloadlink1"
                            DownloadLink1 = x.Value
                        Case "downloadlink2"
                            DownloadLink2 = x.Value
                        Case Else
                    End Select
            End Select
        Loop
        x.Close()
        _nname = name
        t.Close()
    End Sub
End Class
