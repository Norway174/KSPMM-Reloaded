Imports KSPMM_Reloaded.Core
Namespace Internal
    Public Module Core
        ReadOnly Property ListOfInternalPlugins As IPlugin()
            Get
                'Internal.Startup.Plugin, _
                Dim list As IPlugin() = {Internal.ModIO.Plugin, _
                                         Internal.ModIO.StartupPlugin, _
                                         Internal.Network.Plugin, _
                                         Internal.Settings.Plugin, _
                                         Internal.Overpass.Plugin, _
                                         Internal.PermissionResetter.Plugin}
                Return list
            End Get
        End Property
        ReadOnly Property ListOfInternalSettings As Setting()
            Get
                Dim list As Setting() = {Internal.Startup.Plugin, _
                                         Internal.Network.Plugin}
                Return list
            End Get
        End Property
    End Module
End Namespace