Public Class ModIO_UC

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim open As New OpenFileDialog
        open.Filter = "Compressed ZIP Folders (*.zip)|*.zip|All Files (*.*)|*.*"
        open.FileName = ""
        If open.ShowDialog() = DialogResult.Cancel Or open.FileName = "" Then Exit Sub
        Internal.AddMod(New Internal.Modification(open.FileName, Internal.Compression.Zip))
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim fold As New FolderBrowserDialog
        fold.Description = "Find KSP root directory"
        If fold.ShowDialog() = DialogResult.Cancel Then Exit Sub
        Internal.LoadMods(fold.SelectedPath)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Internal.UnloadMods()
    End Sub
End Class
