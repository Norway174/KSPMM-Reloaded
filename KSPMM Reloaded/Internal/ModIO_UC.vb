Imports KSPMM_Reloaded.Internal
Public Class ModIO_UC

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub ModIO_UC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Internal.ModIO.Mods = Internal.ModIO.LoadModsFromSettings()
        BuildTree()
    End Sub
    'Public Sub RebuildTree()
    '   TreeView1.Nodes.Clear()
    '  For Each m As Internal.Modification In Internal.ModIO.Mods
    'Dim n As New List(Of String)
    '       For Each mm As Internal.ModSelection In m.ModSelections
    '          n.Add(mm.ModEntryName)
    '     Next
    'Dim TN As TreeNode
    '       For Each nodePath As String In n
    '          TN = Nothing
    '         For Each node As String In nodePath.Split("/")
    '            If node = "" Then Continue For
    '           If IsNothing(TN) Then
    '              If TreeView1.Nodes.ContainsKey(node) Then
    '                 TN = TreeView1.Nodes(node)
    ''            Else
    '              TN = TreeView1.Nodes.Add(node, node)
    '         End If
    '    Else
    '       If TN.Nodes.ContainsKey(node) Then
    '          TN = TN.Nodes(node)
    '     Else
    '        TN = TN.Nodes.Add(node, node)
    '   End If
    '           End If
    '      Next
    ' Next
    '     Next
    'End Sub
    Public Sub RebuildTree()
        For Each t As TreeNode In TreeView1.Nodes
            Select Case Mods(t.Tag).Status
                Case ModStatus.Uninstalled : t.ForeColor = Color.Black
                Case ModStatus.Installed : t.ForeColor = Color.Green
            End Select
        Next
    End Sub
    Public Sub BuildTree()
        TreeView1.Nodes.Clear()
        Dim i = 0
        For Each m As Modification In Mods
            Dim t As New TreeNode(m.Name, 0, 0)
            Select Case m.Status
                Case ModStatus.Uninstalled : t.ForeColor = Color.Black
                Case ModStatus.Installed : t.ForeColor = Color.Green
            End Select
            t.Tag = i
            t.Checked = m.Use
            TreeView1.Nodes.Add(t)
            i += 1
        Next
    End Sub

    Public Sub StatusUpdate(ByVal Status As String)
        lblStatus.Text = Status
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim open As New OpenFileDialog
        open.Filter = "KSPMM Mod Files (*.kspmm)|*.kspmm|Compressed ZIP Folders (*.zip)|*.zip|All Files (*.*)|*.*"
        open.FileName = ""
        If open.ShowDialog() = DialogResult.Cancel Or open.FileName = "" Then Exit Sub
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
        If Internal.UnloadMods() Then StatusUpdate("Unloading Complete") Else MsgBox("Unloading Failed!", MsgBoxStyle.Critical)
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        If My.Settings.KSPDir = "" Then MsgBox("KSP Directory folder has not been selected.")
        ModIO.UnloadMods()
        For Each node As TreeNode In TreeView1.Nodes
            Mods(node.Tag).Use = node.Checked
        Next
        'For Each node As TreeNode In TreeView1.Nodes
        'Dim l As List(Of ModSelection) = GetChildren(node)
        'Dim i1 As Integer = 0
        'Dim ml As ModSelection() = ModIO.Mods(i).ModSelections.ToArray
        'For Each m As ModSelection In ml
        ' If m.ModEntryName.EndsWith("/") Then
        ' For Each g As ModSelection In l
        ' If g.ModEntryName & "/" = m.ModEntryName Then
        ' Dim g1 = g
        ' g1.ModEntryName &= "/"
        ' ModIO.Mods(i).ModSelections(i1) = g1
        ' End If
        ' Next
        ' Else
        ' For Each g As ModSelection In l
        ' If g.ModEntryName = m.ModEntryName Then
        ' ModIO.Mods(i).ModSelections(i1) = g
        ' End If
        ' Next
        ' End If
        ' i1 += 1
        ' Next
        ' i += 1
        ' Next
        If Internal.LoadMods(My.Settings.KSPDir) = False Then MsgBox("Loading Failed!", MsgBoxStyle.Critical)
    End Sub

    'Public Function GetChildren(parentNode As TreeNode) As List(Of ModSelection)
    'Dim nodes As New List(Of ModSelection)
    '   GetAllChildren(parentNode, nodes)
    '  Return nodes
    'End Function

    'Private Sub GetAllChildren(parentNode As TreeNode, nodes As List(Of ModSelection))
    '    For Each childNode As TreeNode In parentNode.Nodes
    '        nodes.Add(New ModSelection(childNode.FullPath, childNode.Checked))
    '        GetAllChildren(childNode, nodes)
    '    Next
    'End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles btnLocate.Click
        Dim fold As New FolderBrowserDialog
        fold.Description = "Find KSP root directory"
        If fold.ShowDialog() = DialogResult.Cancel Then Exit Sub
        My.Settings.KSPDir = fold.SelectedPath
        My.Settings.Save()
    End Sub

    Private Sub TreeView1_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterCheck
        For Each n As TreeNode In TreeView1.Nodes
            Mods(n.Tag).Use = n.Checked
        Next
        SaveModsToSettings(Mods)
    End Sub
End Class
