<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class sandbox
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(sandbox))
        ToolTip1 = New ToolTip(components)
        TableLayoutPanel1 = New TableLayoutPanel()
        FlowLayoutPanel1 = New FlowLayoutPanel()
        Label5 = New Label()
        Label6 = New Label()
        Label7 = New Label()
        Label8 = New Label()
        Label9 = New Label()
        FlowLayoutPanel2 = New FlowLayoutPanel()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        PictureBox1 = New PictureBox()
        PictureBox2 = New PictureBox()
        TableLayoutPanel1.SuspendLayout()
        FlowLayoutPanel1.SuspendLayout()
        FlowLayoutPanel2.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        TableLayoutPanel1.ColumnCount = 2
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Controls.Add(FlowLayoutPanel1, 1, 1)
        TableLayoutPanel1.Controls.Add(FlowLayoutPanel2, 1, 0)
        TableLayoutPanel1.Controls.Add(PictureBox1, 0, 0)
        TableLayoutPanel1.Controls.Add(PictureBox2, 0, 1)
        TableLayoutPanel1.Location = New Point(235, 181)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 2
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.Size = New Size(930, 546)
        TableLayoutPanel1.TabIndex = 1
        ' 
        ' FlowLayoutPanel1
        ' 
        FlowLayoutPanel1.Controls.Add(Label5)
        FlowLayoutPanel1.Controls.Add(Label6)
        FlowLayoutPanel1.Controls.Add(Label7)
        FlowLayoutPanel1.Controls.Add(Label8)
        FlowLayoutPanel1.Controls.Add(Label9)
        FlowLayoutPanel1.Dock = DockStyle.Fill
        FlowLayoutPanel1.Location = New Point(57, 245)
        FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        FlowLayoutPanel1.Size = New Size(870, 316)
        FlowLayoutPanel1.TabIndex = 0
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(3, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(63, 25)
        Label5.TabIndex = 0
        Label5.Text = "Label5"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(72, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(63, 25)
        Label6.TabIndex = 1
        Label6.Text = "Label6"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(141, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(63, 25)
        Label7.TabIndex = 2
        Label7.Text = "Label7"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(210, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(63, 25)
        Label8.TabIndex = 3
        Label8.Text = "Label8"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(279, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(63, 25)
        Label9.TabIndex = 4
        Label9.Text = "Label9"
        ' 
        ' FlowLayoutPanel2
        ' 
        FlowLayoutPanel2.Controls.Add(Label1)
        FlowLayoutPanel2.Controls.Add(Label2)
        FlowLayoutPanel2.Controls.Add(Label3)
        FlowLayoutPanel2.Controls.Add(Label4)
        FlowLayoutPanel2.Dock = DockStyle.Fill
        FlowLayoutPanel2.Location = New Point(57, 3)
        FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        FlowLayoutPanel2.Size = New Size(870, 236)
        FlowLayoutPanel2.TabIndex = 1
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(3, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(63, 25)
        Label1.TabIndex = 0
        Label1.Text = "Label1"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(72, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(63, 25)
        Label2.TabIndex = 1
        Label2.Text = "Label2"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(141, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(63, 25)
        Label3.TabIndex = 2
        Label3.Text = "Label3"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(210, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(63, 25)
        Label4.TabIndex = 3
        Label4.Text = "Label4"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Dock = DockStyle.Fill
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(3, 3)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(48, 236)
        PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage
        PictureBox1.TabIndex = 2
        PictureBox1.TabStop = False
        ' 
        ' PictureBox2
        ' 
        PictureBox2.Dock = DockStyle.Fill
        PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), Image)
        PictureBox2.Location = New Point(3, 245)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(48, 316)
        PictureBox2.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox2.TabIndex = 3
        PictureBox2.TabStop = False
        ' 
        ' sandbox
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1277, 856)
        Controls.Add(TableLayoutPanel1)
        Name = "sandbox"
        Text = "sandbox"
        TableLayoutPanel1.ResumeLayout(False)
        FlowLayoutPanel1.ResumeLayout(False)
        FlowLayoutPanel1.PerformLayout()
        FlowLayoutPanel2.ResumeLayout(False)
        FlowLayoutPanel2.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents FlowLayoutPanel2 As FlowLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
End Class
