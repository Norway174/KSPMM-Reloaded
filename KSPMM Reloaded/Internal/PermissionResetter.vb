Imports KSPMM_Reloaded.Internal
Namespace Internal
    Public Module PermissionResetter
        Public ReadOnly Property Plugin As IPlugin
            Get
                Dim p As New Plugin ' IPlugin
                p.Name = "Permission Resetter"
                p.Version = New Version(1, 0, 0, 0)
                p.MainDelegate = AddressOf MainSub
                p.TypeOfPlugin = KSPMM_Reloaded.Plugin.PluginType.ShutdownScript
                Return p
            End Get
        End Property
        Public Sub MainSub()
            For Each m As Modification In ModIO.Mods
                Dim f As New IO.FileInfo(m.Filename)
                f.Attributes = IO.FileAttributes.Normal
                f.IsReadOnly = False
            Next
        End Sub
    End Module
End Namespace
