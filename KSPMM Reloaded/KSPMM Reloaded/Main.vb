Public Class Main
    Public Core As Core
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Status.Text = "Loading Plugins..."
        Core = New Core(tabctrlMain)
        Status.Text = "Idle"
    End Sub
End Class
