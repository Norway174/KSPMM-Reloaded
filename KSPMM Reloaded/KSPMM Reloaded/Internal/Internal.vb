Imports KSPMM_Reloaded.Core
Namespace Internal
    Public Module Internal
        ReadOnly Property ListOfInternalPlugins As IPlugin()
            Get
                Dim list As IPlugin() = {KSPMM_Reloaded.Internal.Core.Plugin}
                Return list
            End Get
        End Property
    End Module
End Namespace