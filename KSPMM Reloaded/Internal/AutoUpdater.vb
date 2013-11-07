Public Class AutoUpdater
    Public ReadOnly Property Plugin As Plugin
        Get
            Dim p As New Plugin
            p.MainDelegate = New IPlugin.PluginDelegate(AddressOf Startup)
            p.Description = "Basic Program-wide settings"
            p.Name = "Settings"
            p.TypeOfPlugin = KSPMM_Reloaded.Plugin.PluginType.RuntimeStartup
            p.Version = New Version(1, 0, 0, 0)
            Return p
        End Get
    End Property
    Const URL As String = "https://github.com/Norway174/KSPMM-Reloaded/tree/master/KSPMM%20Reloaded.exe"
    Public Sub Startup()
        Dim t As New Threading.Thread(AddressOf _Startup)
        t.Start()
    End Sub
    Private Sub _Startup()
        Dim w As New Net.WebClient
        AddHandler w.DownloadProgressChanged, Sub()

                                              End Sub
    End Sub
End Class
