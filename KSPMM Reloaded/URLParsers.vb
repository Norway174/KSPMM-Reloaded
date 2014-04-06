Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions

Module GetURL
    Public Const SpaceportURL As String = "http://kerbalspaceport.com/wp/wp-admin/admin-ajax.php"
    Public Function GetSpaceportURL(ByVal ID As String) As Uri
        Dim data As String = "addonid=" & ID & "&action=downloadfileaddon"

        Dim req = Net.WebRequest.Create(SpaceportURL)
        req.Method = "POST"
        req.ContentType = "application/x-www-form-urlencoded"
        req.ContentLength = data.Length

        Using w As New StreamWriter(req.GetRequestStream())
            w.Write(data)
        End Using

        Dim html As String
        Dim response As HttpWebResponse = req.GetResponse()
        Using reader As StreamReader = New StreamReader(response.GetResponseStream())
            html = Trim$(reader.ReadToEnd)
            If html = "Please login or Register to make the purchase" Then
                Return Nothing
            End If
        End Using
        Return New Uri(html)
    End Function
    Public Function GetDropboxURL(ByVal URL As String)
        Dim file As String = URL.Remove(0, URL.LastIndexOf("/"c) + 1)
        file = file.Remove(file.LastIndexOf("."))
        Dim r As New Regex("href=""""([^""""]*?" & file & ".*?)""""")
        Dim html As String = New System.Net.WebClient().DownloadString(URL)
        Dim s As Match = r.Match(html)
        If s.Success Then
            Return WebUtility.HtmlEncode(s.Value)
        Else
            Throw New ArgumentException("Input URL not valid")
        End If
    End Function
    'kNO\s*=\s*"([^"]*?#{Regexp.escape(file)}.*?)"
    Public Function GetMediafireURL(ByVal URL As String)
        Dim file As String = URL.Remove(0, URL.LastIndexOf("/"c) + 1)
        file = file.Remove(file.LastIndexOf("."))
        Dim r As New Regex("kNO\s*=\s*""""([^""""]*?" & file & ".*?)""""")
        Dim html As String = New System.Net.WebClient().DownloadString(URL)
        Dim s As Match = r.Match(html)
        If s.Success Then
            Return WebUtility.HtmlEncode(s.Value)
        Else
            Throw New ArgumentException("Input URL not valid")
        End If
    End Function
    Public Function UrlIsValid(ByVal url As String) As Boolean
        Dim is_valid As Boolean = False
        If url.ToLower().StartsWith("www.") Then url = _
            "http://" & url

        Dim web_response As HttpWebResponse = Nothing
        Try
            Dim web_request As HttpWebRequest = _
                HttpWebRequest.Create(url)
            web_response = _
                DirectCast(web_request.GetResponse(),  _
                HttpWebResponse)
            Return True
        Catch ex As Exception
            Return False
        Finally
            If Not (web_response Is Nothing) Then _
                web_response.Close()
        End Try
    End Function
End Module
