Namespace Internal
    Public Module Overpass
        Private OverpassUC As New Overpass_UC
        Public ReadOnly Property Plugin As Plugin
            Get
                Dim p As New Plugin ' IPlugin
                p.Name = "Overpass"
                p.Version = New Version(1, 0, 0, 0)
                p.Control = OverpassUC
                p.TypeOfPlugin = KSPMM_Reloaded.Plugin.PluginType.TabbedUserControl
                Return p
            End Get
        End Property
    End Module
End Namespace
