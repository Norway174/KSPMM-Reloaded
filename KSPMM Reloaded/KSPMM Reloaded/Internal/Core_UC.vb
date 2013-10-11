Public Class Core_UC

    Private Sub Core_UC_Load(sender As Object, e As EventArgs) Handles Me.Load
        WebBrowser1.Navigate(New Uri("http://kspmm.norway174.com/cms/"))
    End Sub
End Class

Namespace Internal
    Public Module Core
        Public ReadOnly Property Plugin As IPlugin
            Get
                Dim p As IPlugin = Nothing
                p.Name = "Core"
                p.Version = New Version(1, 0, 0, 0)
                p.Control = New Core_UC
                Return p
            End Get
        End Property
    End Module
End Namespace
