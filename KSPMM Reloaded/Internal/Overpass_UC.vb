Public Class Overpass_UC
    Private BaseTextboxLength As Integer = 400
    Private BaseFormWidth As Integer

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'BaseTextboxLength = URLTextbox.Width
        BaseFormWidth = Me.Width
    End Sub
    Public Event ResizeEvent()
    Private Sub Overpass_UC_Resize(sender As Object, e As EventArgs) Handles OverpassWebBrowser.SizeChanged
        Dim value = BaseTextboxLength + (Me.Width - BaseFormWidth)
        URLTextbox.Width = value
    End Sub

    Private Sub OverpassWebBrowser_Navigating(sender As Object, e As WebBrowserNavigatingEventArgs) Handles OverpassWebBrowser.Navigating
        If e.Url.ToString.EndsWith(".zip") Then
            e.Cancel = True
            OverpassWebBrowser.Stop()
            Dim down As New Internal.Download(e.Url, "C:/test.zip", Internal.Priority.Normal)
            Internal.Network.Add(down)
        Else
            URLTextbox.Text = e.Url.ToString
        End If
        ToolStripStatusLabel1.Text = OverpassWebBrowser.StatusText
    End Sub

    Private Sub OverpassWebBrowser_ProgressChanged(sender As Object, e As WebBrowserProgressChangedEventArgs) Handles OverpassWebBrowser.ProgressChanged
        ToolStripProgressBar1.Maximum = e.MaximumProgress
        ToolStripProgressBar1.Value = e.CurrentProgress
        ToolStripStatusLabel1.Text = OverpassWebBrowser.StatusText
    End Sub

    Private Sub OverpassWebBrowser_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles OverpassWebBrowser.DocumentCompleted
        ToolStripStatusLabel1.Text = OverpassWebBrowser.StatusText
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        OverpassWebBrowser.Navigate("http://kerbalspaceport.com/")
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        OverpassWebBrowser.Navigate(URLTextbox.Text)
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        OverpassWebBrowser.Stop()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        OverpassWebBrowser.Refresh()
    End Sub

    Private Sub Overpass_UC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        OverpassWebBrowser.Navigate("http://kerbalspaceport.com/")
    End Sub

    Private Sub OverpassWebBrowser_Navigated(sender As Object, e As WebBrowserNavigatedEventArgs) Handles OverpassWebBrowser.Navigated
        Dim theElementCollection As HtmlElementCollection
        theElementCollection = OverpassWebBrowser.Document.GetElementsByTagName("input")

        For Each curElement As HtmlElement In theElementCollection
            If curElement.OuterHtml.Contains("red_btn") Then
                curElement.SetAttribute("InnerText", "Download with KSPMM Reloaded!")
            End If
        Next
    End Sub
End Class
