<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ModIO_UC
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ModIO_UC))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnAdd = New System.Windows.Forms.ToolStripButton()
        Me.btnRemove = New System.Windows.Forms.ToolStripButton()
        Me.btnLoad = New System.Windows.Forms.ToolStripButton()
        Me.btnUnload = New System.Windows.Forms.ToolStripButton()
        Me.btnLocate = New System.Windows.Forms.ToolStripButton()
        Me.btnLoadModpack = New System.Windows.Forms.ToolStripButton()
        Me.btnEnableAll = New System.Windows.Forms.ToolStripButton()
        Me.btnDisableAll = New System.Windows.Forms.ToolStripButton()
        Me.btnFilterMods = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsddPresets = New System.Windows.Forms.ToolStripDropDownButton()
        Me.AddPresetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.FileImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.prgOverall = New System.Windows.Forms.ToolStripProgressBar()
        Me.prgIndividual = New System.Windows.Forms.ToolStripProgressBar()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.txtFilter = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator2, Me.btnAdd, Me.btnRemove, Me.btnLoad, Me.btnUnload, Me.btnLocate, Me.btnLoadModpack, Me.btnEnableAll, Me.btnDisableAll, Me.btnFilterMods, Me.ToolStripSeparator1, Me.tsddPresets})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(501, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnAdd
        '
        Me.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), System.Drawing.Image)
        Me.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(23, 22)
        Me.btnAdd.Text = "Add Mod"
        Me.btnAdd.ToolTipText = "Add Mod"
        '
        'btnRemove
        '
        Me.btnRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnRemove.Image = CType(resources.GetObject("btnRemove.Image"), System.Drawing.Image)
        Me.btnRemove.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(23, 22)
        Me.btnRemove.Text = "Remove Mod"
        Me.btnRemove.ToolTipText = "Remove Mod"
        '
        'btnLoad
        '
        Me.btnLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnLoad.Image = CType(resources.GetObject("btnLoad.Image"), System.Drawing.Image)
        Me.btnLoad.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(23, 22)
        Me.btnLoad.Text = "Load Mods"
        Me.btnLoad.ToolTipText = "Load Mods"
        '
        'btnUnload
        '
        Me.btnUnload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnUnload.Image = CType(resources.GetObject("btnUnload.Image"), System.Drawing.Image)
        Me.btnUnload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnUnload.Name = "btnUnload"
        Me.btnUnload.Size = New System.Drawing.Size(23, 22)
        Me.btnUnload.Text = "Unload Mods"
        Me.btnUnload.ToolTipText = "Unload Mods"
        '
        'btnLocate
        '
        Me.btnLocate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnLocate.Image = CType(resources.GetObject("btnLocate.Image"), System.Drawing.Image)
        Me.btnLocate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLocate.Name = "btnLocate"
        Me.btnLocate.Size = New System.Drawing.Size(23, 22)
        Me.btnLocate.Text = "Locate KSP Folder"
        '
        'btnLoadModpack
        '
        Me.btnLoadModpack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnLoadModpack.Image = CType(resources.GetObject("btnLoadModpack.Image"), System.Drawing.Image)
        Me.btnLoadModpack.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLoadModpack.Name = "btnLoadModpack"
        Me.btnLoadModpack.Size = New System.Drawing.Size(23, 22)
        Me.btnLoadModpack.Text = "Load Modpack"
        '
        'btnEnableAll
        '
        Me.btnEnableAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnEnableAll.Image = CType(resources.GetObject("btnEnableAll.Image"), System.Drawing.Image)
        Me.btnEnableAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEnableAll.Name = "btnEnableAll"
        Me.btnEnableAll.Size = New System.Drawing.Size(23, 22)
        Me.btnEnableAll.Text = "Enable All"
        '
        'btnDisableAll
        '
        Me.btnDisableAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnDisableAll.Image = CType(resources.GetObject("btnDisableAll.Image"), System.Drawing.Image)
        Me.btnDisableAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDisableAll.Name = "btnDisableAll"
        Me.btnDisableAll.Size = New System.Drawing.Size(23, 22)
        Me.btnDisableAll.Text = "Disable All"
        '
        'btnFilterMods
        '
        Me.btnFilterMods.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnFilterMods.Image = CType(resources.GetObject("btnFilterMods.Image"), System.Drawing.Image)
        Me.btnFilterMods.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnFilterMods.Name = "btnFilterMods"
        Me.btnFilterMods.Size = New System.Drawing.Size(23, 22)
        Me.btnFilterMods.Text = "Filter Mods"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'tsddPresets
        '
        Me.tsddPresets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsddPresets.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddPresetToolStripMenuItem, Me.ToolStripSeparator3})
        Me.tsddPresets.Image = CType(resources.GetObject("tsddPresets.Image"), System.Drawing.Image)
        Me.tsddPresets.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsddPresets.Name = "tsddPresets"
        Me.tsddPresets.Size = New System.Drawing.Size(57, 22)
        Me.tsddPresets.Text = "Presets"
        Me.tsddPresets.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'AddPresetToolStripMenuItem
        '
        Me.AddPresetToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AddPresetToolStripMenuItem.Name = "AddPresetToolStripMenuItem"
        Me.AddPresetToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.AddPresetToolStripMenuItem.Text = "Add Preset"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(128, 6)
        '
        'TreeView1
        '
        Me.TreeView1.CheckBoxes = True
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.ImageIndex = 0
        Me.TreeView1.ImageList = Me.FileImageList
        Me.TreeView1.Location = New System.Drawing.Point(0, 25)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.PathSeparator = "/"
        Me.TreeView1.SelectedImageIndex = 0
        Me.TreeView1.Size = New System.Drawing.Size(501, 278)
        Me.TreeView1.TabIndex = 1
        '
        'FileImageList
        '
        Me.FileImageList.ImageStream = CType(resources.GetObject("FileImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.FileImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.FileImageList.Images.SetKeyName(0, "box.png")
        Me.FileImageList.Images.SetKeyName(1, "box-label.png")
        Me.FileImageList.Images.SetKeyName(2, "document.png")
        Me.FileImageList.Images.SetKeyName(3, "document-text.png")
        Me.FileImageList.Images.SetKeyName(4, "script-text.png")
        Me.FileImageList.Images.SetKeyName(5, "bullet_green.png")
        Me.FileImageList.Images.SetKeyName(6, "bullet_go.png")
        Me.FileImageList.Images.SetKeyName(7, "arrow_down.png")
        Me.FileImageList.Images.SetKeyName(8, "accept.png")
        Me.FileImageList.Images.SetKeyName(9, "error.png")
        Me.FileImageList.Images.SetKeyName(10, "cancel.png")
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.prgOverall, Me.prgIndividual, Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 303)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(501, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'prgOverall
        '
        Me.prgOverall.Name = "prgOverall"
        Me.prgOverall.Size = New System.Drawing.Size(100, 16)
        '
        'prgIndividual
        '
        Me.prgIndividual.Name = "prgIndividual"
        Me.prgIndividual.Size = New System.Drawing.Size(100, 16)
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(26, 17)
        Me.lblStatus.Text = "Idle"
        '
        'ToolStrip2
        '
        Me.ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtFilter})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 25)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(501, 25)
        Me.ToolStrip2.TabIndex = 3
        Me.ToolStrip2.Text = "ToolStrip2"
        Me.ToolStrip2.Visible = False
        '
        'txtFilter
        '
        Me.txtFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(400, 25)
        '
        'ModIO_UC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ToolStrip2)
        Me.Controls.Add(Me.TreeView1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Name = "ModIO_UC"
        Me.Size = New System.Drawing.Size(501, 325)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents FileImageList As System.Windows.Forms.ImageList
    Friend WithEvents btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRemove As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnLoad As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnUnload As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnLocate As System.Windows.Forms.ToolStripButton
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents prgOverall As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents prgIndividual As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents btnLoadModpack As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEnableAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnDisableAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsddPresets As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnFilterMods As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Friend WithEvents txtFilter As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AddPresetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator

End Class
