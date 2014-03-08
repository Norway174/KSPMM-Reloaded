Public Class Main
    Public TabCore As Core
    Public ScriptCore As Core
    Public UpdateAvailable As Boolean = False
    Public Const Version As Integer = 3
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        'Dim t As New Threading.Thread(AddressOf StartupThread)
        ' t.IsBackground = True
        't.Start()
        StartupThread()
    End Sub
    Public Sub StartupThread()
        Status.Text = "Loading Plugins..."
        TabCore = New Core(tabctrlMain, Plugin.PluginType.TabbedUserControl)
        Status.Text = "Running Scripts..."
        ScriptCore = New Core(tabctrlMain, Plugin.PluginType.MainFormStartup)
        Status.Text = "Idle"
        If My.Settings.AutoUpdate = True Then
            CheckForUpdatesToolStripMenuItem.Image = My.Resources.tick
            CheckForUpdates()
        End If
    End Sub

    Private Sub CheckForUpdatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckForUpdatesToolStripMenuItem.Click
        Dim b As Boolean = My.Settings.AutoUpdate
        Select Case b
            Case True
                CheckForUpdatesToolStripMenuItem.Image = Nothing
                My.Settings.AutoUpdate = False
            Case False
                CheckForUpdatesToolStripMenuItem.Image = My.Resources.tick
                My.Settings.AutoUpdate = True
        End Select
        My.Settings.Save()
    End Sub

    Public CheckingForUpdates As Boolean = False
    Public Sub CheckForUpdates()
        UpdateButton.Image = My.Resources.arrow_refresh_small
        UpdateButton.Text = "Checking for updates..."
        UpdateButton.ForeColor = Color.Gray
        CheckingForUpdates = True
        ToolStripProgressBar1.Style = ProgressBarStyle.Marquee

        Dim s As String = ""
        Dim w As New Net.WebClient
        Try
            s = w.DownloadString("https://raw.github.com/Norway174/KSPMM-Reloaded/master/version.txt")
        Catch ex As Exception
            CheckingForUpdates = False
            UpdateButton.Image = My.Resources._error
            UpdateButton.Text = "Error - Retry"
            UpdateButton.ForeColor = Color.Black
            ToolStripProgressBar1.Style = ProgressBarStyle.Blocks
            Exit Sub
        End Try
        If CInt(s) > Version Then
            UpdateAvailable = True
            UpdateButton.Image = My.Resources.application_get
            UpdateButton.Text = "Update"
            UpdateButton.ForeColor = Color.Black
            ToolStripProgressBar1.Value = 100
        Else
            CheckingForUpdates = False
            UpdateButton.Image = My.Resources.tick
            UpdateButton.Text = "No update found"
            ToolStripProgressBar1.Style = ProgressBarStyle.Blocks
            ToolStripProgressBar1.Value = 100
        End If
    End Sub

    Private Sub ToolStripSplitButton1_ButtonClick(sender As Object, e As EventArgs) Handles UpdateButton.ButtonClick
        If UpdateAvailable = False Then
            If CheckingForUpdates = False Then
                CheckingForUpdates = True
                Dim t As New Threading.Thread(AddressOf CheckForUpdates)
                t.Start()
            End If
            Exit Sub
        End If
        Dim UpdW As New Updater
        UpdW.PassedText = "https://raw.github.com/Norway174/KSPMM-Reloaded/master/KSPMM%20Reloaded.exe"
        UpdW.ShowDialog(Me)
    End Sub

    Dim ubhidden As Boolean = False
    Private Sub HideUpdateBarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HideUpdateBarToolStripMenuItem.Click
        If ubhidden Then
            HideUpdateBarToolStripMenuItem.Image = Nothing
            UpdateStatusStrip.Visible = True
            UpdateStatusStrip.SendToBack()
            ubhidden = False
        Else
            HideUpdateBarToolStripMenuItem.Image = My.Resources.tick
            UpdateStatusStrip.Visible = False
            UpdateStatusStrip.BringToFront()
            ubhidden = True
        End If
    End Sub
End Class
