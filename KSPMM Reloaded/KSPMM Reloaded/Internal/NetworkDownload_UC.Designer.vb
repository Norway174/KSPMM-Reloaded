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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.LineShape1 = New Microsoft.VisualBasic.PowerPacks.LineShape()
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 39)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Image" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Goes" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Here"
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
        'NetworkDownload_UC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.Label1)
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents LineShape1 As Microsoft.VisualBasic.PowerPacks.LineShape

End Class
