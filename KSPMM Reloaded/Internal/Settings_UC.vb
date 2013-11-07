Imports KSPMM_Reloaded.Internal

Public Class Settings_UC
    Public slist As New List(Of Setting)
    Private Sub Settings_UC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim i As Integer = 0
        For Each s As Setting In Internal.ListOfInternalSettings
            Dim ss As Setting = s

        Next
    End Sub
End Class
