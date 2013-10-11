Namespace Internal
    Public Module Startup
        Public ReadOnly Property Plugin As IPlugin
            Get
                Dim p As New Plugin ' IPlugin
                p.Name = "Startup"
                p.Version = New Version(1, 0, 0, 0)
                p.Control = New Startup_UC
                Return p
            End Get
        End Property
    End Module
End Namespace