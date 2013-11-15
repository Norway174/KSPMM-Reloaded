Public Class Startup_UC

    Private Sub Startup_UC_Load(sender As Object, e As EventArgs) Handles Me.Load
        WebBrowser1.Navigate(New Uri("http://kspmm.norway174.com/cms/"))
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted

    End Sub
End Class
