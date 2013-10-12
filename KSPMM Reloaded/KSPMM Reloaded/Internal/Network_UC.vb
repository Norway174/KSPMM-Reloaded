Public Class Network_UC
    Public Sub AddDownload(ByVal Download As Internal.QueuedDownload, UC As Panel)
        Dim d As New NetworkDownload_UC
        d.DName.Text = Download.Download.URI.OriginalString
        d.DPriority.Text = [Enum].GetName(GetType(Internal.Priority), Download.Priority)
        AddHandler Download.Download.StatusChange, AddressOf d.ChangeStatus
        UC.Controls.Add(d)
        d.Dock = DockStyle.Top
        d.SendToBack()
        Download.Download.Start()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim save As New SaveFileDialog
        save.Filter = "All Files (*.*)|*.*"
        save.FileName = ""
        save.ShowDialog()
        Dim down As New Internal.Download(New Uri(TextBox1.Text), save.FileName, Internal.Priority.Normal)
        Internal.Network.Add(down)
    End Sub
End Class
