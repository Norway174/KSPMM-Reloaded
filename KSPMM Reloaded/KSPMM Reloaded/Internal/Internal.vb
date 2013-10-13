Imports KSPMM_Reloaded.Core
Namespace Internal
    Public Module Core
        ReadOnly Property ListOfInternalPlugins As IPlugin()
            Get
                Dim list As IPlugin() = {Internal.Startup.Plugin, _
                                         Internal.Network.Plugin}
                Return list
            End Get
        End Property
    End Module
End Namespace