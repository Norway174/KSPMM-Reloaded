<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NetworkDownload_UC
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
        Me.DImage = New System.Windows.Forms.PictureBox()
        Me.DName = New System.Windows.Forms.Label()
        Me.DStatus = New System.Windows.Forms.Label()
        Me.DPriority = New System.Windows.Forms.Label()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.LineShape1 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.DProgressBar = New System.Windows.Forms.ProgressBar()
        Me.DProgress = New System.Windows.Forms.Label()
        CType(Me.DImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DImage
        '
        Me.DImage.Dock = System.Windows.Forms.DockStyle.Left
        Me.DImage.Location = New System.Drawing.Point(0, 0)
        Me.DImage.Name = "DImage"
        Me.DImage.Size = New System.Drawing.Size(50, 50)
        Me.DImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.DImage.TabIndex = 1
        Me.DImage.TabStop = False
        '
        'DName
        '
        Me.DName.AutoSize = True
        Me.DName.Location = New System.Drawing.Point(56, 2)
        Me.DName.Name = "DName"
        Me.DName.Size = New System.Drawing.Size(35, 13)
        Me.DName.TabIndex = 2
        Me.DName.Text = "Name"
        '
        'DStatus
        '
        Me.DStatus.AutoSize = True
        Me.DStatus.ForeColor = System.Drawing.Color.Silver
        Me.DStatus.Location = New System.Drawing.Point(56, 18)
        Me.DStatus.Name = "DStatus"
        Me.DStatus.Size = New System.Drawing.Size(37, 13)
        Me.DStatus.TabIndex = 3
        Me.DStatus.Text = "Status"
        '
        'DPriority
        '
        Me.DPriority.AutoSize = True
        Me.DPriority.ForeColor = System.Drawing.Color.Red
        Me.DPriority.Location = New System.Drawing.Point(56, 34)
        Me.DPriority.Name = "DPriority"
        Me.DPriority.Size = New System.Drawing.Size(38, 13)
        Me.DPriority.TabIndex = 4
        Me.DPriority.Text = "Priority"
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.LineShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(263, 50)
        Me.ShapeContainer1.TabIndex = 6
        Me.ShapeContainer1.TabStop = False
        '
        'LineShape1
        '
        Me.LineShape1.Name = "LineShape1"
        Me.LineShape1.X1 = 50
        Me.LineShape1.X2 = 50
        Me.LineShape1.Y1 = 0
        Me.LineShape1.Y2 = 50
        '
        'DProgressBar
        '
        Me.DProgressBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DProgressBar.Location = New System.Drawing.Point(128, 34)
        Me.DProgressBar.Name = "DProgressBar"
        Me.DProgressBar.Size = New System.Drawing.Size(132, 13)
        Me.DProgressBar.TabIndex = 7
        '
        'DProgress
        '
        Me.DProgress.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DProgress.AutoSize = True
        Me.DProgress.Location = New System.Drawing.Point(218, 18)
        Me.DProgress.Name = "DProgress"
        Me.DProgress.Size = New System.Drawing.Size(44, 13)
        Me.DProgress.TabIndex = 8
        Me.DProgress.Text = "0B / 0B"
        Me.DProgress.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'NetworkDownload_UC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.DProgress)
        Me.Controls.Add(Me.DProgressBar)
        Me.Controls.Add(Me.DPriority)
        Me.Controls.Add(Me.DStatus)
        Me.Controls.Add(Me.DName)
        Me.Controls.Add(Me.DImage)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.Name = "NetworkDownload_UC"
        Me.Size = New System.Drawing.Size(263, 50)
        CType(Me.DImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DImage As System.Windows.Forms.PictureBox
    Friend WithEvents DName As System.Windows.Forms.Label
    Friend WithEvents DStatus As System.Windows.Forms.Label
    Friend WithEvents DPriority As System.Windows.Forms.Label
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents LineShape1 As Microsoft.VisualBasic.PowerPacks.LineShape
    Friend WithEvents DProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents DProgress As System.Windows.Forms.Label

End Class
