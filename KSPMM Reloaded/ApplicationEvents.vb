Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Public Sub Exception(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            BasicUFL.CriticalError(sender, e)
        End Sub
        Public Sub Start() Handles Me.Startup
            Dim c As New Core(Nothing, Plugin.PluginType.RuntimeScript)
        End Sub
        Public Sub AppStop() Handles Me.Shutdown
            Dim c As New Core(Nothing, Plugin.PluginType.ShutdownScript)
            My.Settings.Save()
        End Sub
    End Class
End Namespace

