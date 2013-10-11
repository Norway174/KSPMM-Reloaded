Public Class Startup_UC

    Private Sub Startup_UC_Load(sender As Object, e As EventArgs) Handles Me.Load
        WebBrowser1.Navigate(New Uri("http://kspmm.norway174.com/cms/"))
    End Sub
End Class
