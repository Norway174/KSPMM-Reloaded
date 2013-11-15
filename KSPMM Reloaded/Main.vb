Public Class Main
    Public TabCore As Core
    Public ScriptCore As Core
    Public UpdateAvailable As Boolean = False
    Public Const Version As Integer = 2
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        StartupTimer.Enabled = True
        StartupTimer.Start()
    End Sub

    Private Sub CheckForUpdatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckForUpdatesToolStripMenuItem.Click
        'Dim s = Internal.Settings.ObtainSetting("AutoUpdate")
        'Dim b As Boolean
        'If s Is Nothing Then
        'b = False
        'Else
        'b = s.Setting(0)
        'End If
        Dim b As Boolean = My.Settings.AutoUpdate
        Select Case b
            Case True
                CheckForUpdatesToolStripMenuItem.Image = Nothing
                'Internal.Settings.ChangeSettings(Internal.SettingMode.CreateorUpdate, New Internal.InternalSetting({False}, "AutoUpdate"))
                My.Settings.AutoUpdate = False
            Case False
                CheckForUpdatesToolStripMenuItem.Image = My.Resources.tick
                'Internal.Settings.ChangeSettings(Internal.SettingMode.CreateorUpdate, New Internal.InternalSetting({True}, "AutoUpdate"))
                My.Settings.AutoUpdate = True
        End Select
        My.Settings.Save()
    End Sub

    Public CheckingForUpdates As Boolean = False
    Public Sub CheckForUpdates()
        Dim t As New Threading.Thread(Sub()
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
                                      End Sub)
        t.Start()
    End Sub

    Private Sub ToolStripSplitButton1_ButtonClick(sender As Object, e As EventArgs) Handles UpdateButton.ButtonClick
        If UpdateAvailable = False Then
            If CheckingForUpdates = False Then
                CheckingForUpdates = True
                CheckForUpdates()
            End If
            Exit Sub
        End If
        Dim UpdW As New Updater
        UpdW.PassedText = "https://raw.github.com/Norway174/KSPMM-Reloaded/master/KSPMM%20Reloaded.exe"
        UpdW.ShowDialog(Me)
    End Sub

    Private Sub StartupTimer_Tick(sender As Object, e As EventArgs) Handles StartupTimer.Tick
        StartupTimer.Stop()
        StartupTimer.Enabled = False
        Status.Text = "Loading Plugins..."
        TabCore = New Core(tabctrlMain, Plugin.PluginType.TabbedUserControl)
        Status.Text = "Running Scripts..."
        ScriptCore = New Core(tabctrlMain, Plugin.PluginType.MainFormStartup)
        Status.Text = "Idle"
        If My.Settings.AutoUpdate = True Then
            CheckForUpdatesToolStripMenuItem.Image = My.Resources.tick
            CheckForUpdates()
        End If
        'Dim s = Internal.Settings.ObtainSetting("AutoUpdate")
        'Dim b As Boolean
        'If s Is Nothing Then
        'b = False
        'Else
        'b = s.Setting(0)
        'End If
        'Select Case b
        '   Case True
        'CheckForUpdatesToolStripMenuItem.Image = My.Resources.tick
        '    Case False
        'End Select
    End Sub
End Class
