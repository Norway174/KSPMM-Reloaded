Namespace Internal
    Public Module Startup
        Private _UC As New Startup_UC
        Public ReadOnly Property Plugin As IPlugin
            Get
                Dim p As New Plugin ' IPlugin
                p.Name = "Welcome Page"
                p.Version = New Version(1, 0, 0, 0)
                p.Control = _UC
                Return p
            End Get
        End Property
    End Module
End Namespace