Public Class Main
    Public Core As Core
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False 'Only for debug
        Status.Text = "Loading Plugins..."
        Core = New Core(tabctrlMain, Plugin.PluginType.TabbedUserControl)
        Status.Text = "Idle"
    End Sub
End Class
