Imports KSPMM_Reloaded.Internal
Imports Ionic.Zip
Public Class ModIO_UC

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub ModIO_UC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BuildTree()
    End Sub
    Public Sub RebuildTree()
        For Each t As TreeNode In TreeView1.Nodes
            Select Case Mods(t.Tag).Status
                Case ModStatus.Uninstalled : t.ForeColor = Color.Black
                Case ModStatus.Installed : t.ForeColor = Color.Green
                Case ModStatus.Installed : t.ForeColor = Color.Maroon
            End Select
        Next
    End Sub
    Public Sub BuildTree(Optional ByVal filter As String = "")
        TreeView1.Nodes.Clear()
        Dim i = 0
        For Each m As Modification In Mods
            If filter = "" Or m.Name.ToLower.Contains(filter.ToLower) Then
                Dim t As New TreeNode()
                Select Case m.Compression
                    Case Compression.KSPMM : t = New TreeNode(m.Name, 1, 1)
                    Case Else : t = New TreeNode(m.Name, 0, 0)
                End Select
                Select Case m.Status
                    Case ModStatus.Uninstalled : t.ForeColor = Color.Black
                    Case ModStatus.Installed : t.ForeColor = Color.Green
                    Case ModStatus.Installed : t.ForeColor = Color.Maroon
                End Select
                t.ContextMenuStrip = GenerateContextMenuStrip(i)
                t.Tag = i
                t.Checked = m.Use
                TreeView1.Nodes.Add(t)
                i += 1
            End If
        Next
    End Sub


#Region "Context Menu Actions"
    Public Function GenerateContextMenuStrip(ByVal Tag As Object) As ContextMenuStrip
        Dim c As New ContextMenuStrip()

        Dim i As New ToolStripMenuItem
        AddHandler c.ItemClicked, Sub()
                                      CRename(Tag)
                                  End Sub
        i.Text = "Rename"
        c.Items.Add(i)

        Return c
    End Function
    Public Sub CRename(ByVal tag As Object)
        Mods(tag).Name = InputBox("Rename mod", "Rename", Mods(tag).Name)
        ModIO.SaveModsToSettings(Mods)
        BuildTree()
    End Sub
#End Region

    Public Sub StatusUpdate(ByVal Status As String)
        lblStatus.Text = Status
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim open As New OpenFileDialog
        open.Filter = "KSPMM Mod Files (*.kspmm)|*.kspmm|Compressed ZIP Folders (*.zip)|*.zip|All Files (*.*)|*.*"
        open.FileName = ""
        If open.ShowDialog(Me) = DialogResult.Cancel Or open.FileName = "" Then Exit Sub
        Dim f As New IO.FileInfo(open.FileName)
        If f.Extension = ".zip" Then
            Internal.AddMod(New Internal.Modification(open.FileName, Internal.Compression.Zip))
        ElseIf f.Extension = ".kspmm" Then
            Internal.AddMod(New Internal.Modification(open.FileName, Internal.Compression.KSPMM))
        Else
            Internal.AddMod(New Internal.Modification(open.FileName, Internal.Compression.Other))
        End If
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        Try
            ModIO.RemoveMod(Mods(TreeView1.SelectedNode.Tag))
            BuildTree()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles btnUnload.Click
        SetButtonStatus(False)
        If Internal.UnloadMods() Then StatusUpdate("Unloading Complete") Else MsgBox("Unloading Failed!", MsgBoxStyle.Critical)
        SetButtonStatus(True)
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        If My.Settings.KSPDir = "" Then MsgBox("KSP Directory folder has not been selected.")
        SetButtonStatus(False)
        If ModIO.UnloadMods() = False Then MsgBox("Loading Failed!", MsgBoxStyle.Critical) : SetButtonStatus(True) : Exit Sub
        For Each node As TreeNode In TreeView1.Nodes
            Mods(node.Tag).Use = node.Checked
        Next
        If Internal.LoadMods(My.Settings.KSPDir) = False Then MsgBox("Loading Failed!", MsgBoxStyle.Critical)
        SetButtonStatus(True)
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles btnLocate.Click
        Dim fold As New FolderBrowserDialog
        fold.Description = "Find KSP root directory"
        If fold.ShowDialog(Me) = DialogResult.Cancel Then Exit Sub
        My.Settings.KSPDir = fold.SelectedPath
        My.Settings.Save()
    End Sub

    Private Sub TreeView1_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterCheck
        For Each n As TreeNode In TreeView1.Nodes
            Mods(n.Tag).Use = n.Checked
        Next
        SaveModsToSettings(Mods)
    End Sub

    Private Sub btnLoadModpack_Click(sender As Object, e As EventArgs) Handles btnLoadModpack.Click
        Dim open As New OpenFileDialog
        open.Filter = "KSPMM Mod Pack (*.ksppack)|*.ksppack"
        If open.ShowDialog(Me) = DialogResult.Cancel Then Exit Sub
        Using z As New ZipFile(open.FileName)
            For Each n As ZipEntry In z.EntriesSorted
                Dim i As New InfoParser(Misc.GetTextFromZipEntry(n))
                Dim down As New Internal.Download(New Uri(i.DownloadLink1), True, My.Computer.FileSystem.CurrentDirectory & "\" & i._nname & ".zip", Internal.Priority.Normal)
                Internal.Network.Add(down)
            Next
        End Using
    End Sub

    Private Sub btnEnableAll_Click(sender As Object, e As EventArgs) Handles btnEnableAll.Click
        For Each m As Modification In Mods
            m.Use = True
        Next
        BuildTree()
    End Sub

    Private Sub btnDisableAll_Click(sender As Object, e As EventArgs) Handles btnDisableAll.Click
        For Each m As Modification In Mods
            m.Use = False
        Next
        BuildTree()
    End Sub

    Public Property Presets As New List(Of Preset)
    Private Sub ToolStripDropDownButton1_Click(sender As Object, e As EventArgs) 'Handles tsddPresets.Click
        
    End Sub
    Private Sub PresetClick(sender As Object, e As EventArgs)
        Dim t As ToolStripItem = sender

    End Sub
    Public Structure Preset
        Sub New(ByVal PresetName As String, ByVal ModID As Integer, ByVal Use As Boolean)
            Name = PresetName
            ID = ModID
            Used = Use
        End Sub
        Public Name As String
        Public ID As Integer
        Public Used As Boolean
    End Structure

    Private Sub btnFilterMods_Click(sender As Object, e As EventArgs) Handles btnFilterMods.Click
        If ToolStrip2.Visible Then
            ToolStrip2.Visible = False
            ToolStrip2.BringToFront()
            txtFilter.Text = ""
            BuildTree()
            SetButtonStatus(True)
        Else
            ToolStrip2.Visible = True
            ToolStrip2.SendToBack()
            ToolStrip1.SendToBack()
            SetButtonStatus(False)
            btnFilterMods.Enabled = True
            btnRemove.Enabled = True
        End If
    End Sub

    Private Sub txtFilter_TextChanged(sender As Object, e As EventArgs) Handles txtFilter.TextChanged
        BuildTree(txtFilter.Text)
    End Sub

    Public Sub SetButtonStatus(ByVal Enabled As Boolean)
        btnAdd.Enabled = Enabled
        btnRemove.Enabled = Enabled
        btnLoad.Enabled = Enabled
        btnUnload.Enabled = Enabled
        btnLocate.Enabled = Enabled
        btnLoadModpack.Enabled = Enabled
        btnEnableAll.Enabled = Enabled
        btnDisableAll.Enabled = Enabled
        btnFilterMods.Enabled = Enabled
    End Sub
End Class
