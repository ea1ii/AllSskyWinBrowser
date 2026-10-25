<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        StatusStrip1 = New StatusStrip()
        ToolStripStatusLabel1 = New ToolStripStatusLabel()
        ToolStripProgressBar1 = New ToolStripProgressBar()
        tslblActiveFilter = New ToolStripStatusLabel()
        tstlblDaysCount = New ToolStripStatusLabel()
        tsslblCountCrap = New ToolStripStatusLabel()
        tsslblCountPass = New ToolStripStatusLabel()
        tsslblCountNice = New ToolStripStatusLabel()
        tsslblCountCool = New ToolStripStatusLabel()
        ImageList1 = New ImageList(components)
        ToolStrip1 = New ToolStrip()
        tsbSetup = New ToolStripButton()
        tsbExit = New ToolStripButton()
        ToolStripButton1 = New ToolStripButton()
        TreeView1 = New TreeView()
        TableLayoutPanel1 = New TableLayoutPanel()
        Panel1 = New Panel()
        Panel4 = New Panel()
        Panel2 = New Panel()
        tlpHeader = New TableLayoutPanel()
        tsbPurge = New ToolStripButton()
        StatusStrip1.SuspendLayout()
        ToolStrip1.SuspendLayout()
        Panel1.SuspendLayout()
        Panel4.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(24, 24)
        StatusStrip1.Items.AddRange(New ToolStripItem() {ToolStripStatusLabel1, ToolStripProgressBar1, tslblActiveFilter, tstlblDaysCount, tsslblCountCrap, tsslblCountPass, tsslblCountNice, tsslblCountCool})
        StatusStrip1.Location = New Point(0, 1154)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Size = New Size(2364, 36)
        StatusStrip1.TabIndex = 3
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' ToolStripStatusLabel1
        ' 
        ToolStripStatusLabel1.BorderSides = ToolStripStatusLabelBorderSides.Left Or ToolStripStatusLabelBorderSides.Top Or ToolStripStatusLabelBorderSides.Right Or ToolStripStatusLabelBorderSides.Bottom
        ToolStripStatusLabel1.BorderStyle = Border3DStyle.SunkenInner
        ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        ToolStripStatusLabel1.Size = New Size(184, 29)
        ToolStripStatusLabel1.Text = "ToolStripStatusLabel1"
        ' 
        ' ToolStripProgressBar1
        ' 
        ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        ToolStripProgressBar1.Size = New Size(100, 28)
        ' 
        ' tslblActiveFilter
        ' 
        tslblActiveFilter.BorderSides = ToolStripStatusLabelBorderSides.Left Or ToolStripStatusLabelBorderSides.Top Or ToolStripStatusLabelBorderSides.Right Or ToolStripStatusLabelBorderSides.Bottom
        tslblActiveFilter.BorderStyle = Border3DStyle.SunkenInner
        tslblActiveFilter.Name = "tslblActiveFilter"
        tslblActiveFilter.Size = New Size(112, 29)
        tslblActiveFilter.Text = "Filltering: All"
        ' 
        ' tstlblDaysCount
        ' 
        tstlblDaysCount.BorderSides = ToolStripStatusLabelBorderSides.Left Or ToolStripStatusLabelBorderSides.Top Or ToolStripStatusLabelBorderSides.Right Or ToolStripStatusLabelBorderSides.Bottom
        tstlblDaysCount.BorderStyle = Border3DStyle.SunkenInner
        tstlblDaysCount.Name = "tstlblDaysCount"
        tstlblDaysCount.Size = New Size(26, 29)
        tstlblDaysCount.Text = "n"
        ' 
        ' tsslblCountCrap
        ' 
        tsslblCountCrap.Name = "tsslblCountCrap"
        tsslblCountCrap.Size = New Size(58, 29)
        tsslblCountCrap.Text = "Crap: "
        ' 
        ' tsslblCountPass
        ' 
        tsslblCountPass.Name = "tsslblCountPass"
        tsslblCountPass.Size = New Size(55, 29)
        tsslblCountPass.Text = "Pass: "
        ' 
        ' tsslblCountNice
        ' 
        tsslblCountNice.Name = "tsslblCountNice"
        tsslblCountNice.Size = New Size(55, 29)
        tsslblCountNice.Text = "Nice: "
        ' 
        ' tsslblCountCool
        ' 
        tsslblCountCool.Name = "tsslblCountCool"
        tsslblCountCool.Size = New Size(58, 29)
        tsslblCountCool.Text = "Cool: "
        ' 
        ' ImageList1
        ' 
        ImageList1.ColorDepth = ColorDepth.Depth32Bit
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), ImageListStreamer)
        ImageList1.TransparentColor = Color.Transparent
        ImageList1.Images.SetKeyName(0, "Image")
        ImageList1.Images.SetKeyName(1, "Metadata")
        ImageList1.Images.SetKeyName(2, "Empty")
        ImageList1.Images.SetKeyName(3, "Video")
        ImageList1.Images.SetKeyName(4, "Reload")
        ImageList1.Images.SetKeyName(5, "Doc")
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {tsbSetup, tsbExit, ToolStripButton1, tsbPurge})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(2364, 34)
        ToolStrip1.TabIndex = 5
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' tsbSetup
        ' 
        tsbSetup.Image = My.Resources.Resources.Cog_1__Streamline_Core
        tsbSetup.ImageTransparentColor = Color.Magenta
        tsbSetup.Name = "tsbSetup"
        tsbSetup.Size = New Size(86, 29)
        tsbSetup.Text = "Setup"
        ' 
        ' tsbExit
        ' 
        tsbExit.Alignment = ToolStripItemAlignment.Right
        tsbExit.Image = My.Resources.Resources.Door_Open_House_Exit_Logout__Streamline_Core
        tsbExit.ImageTransparentColor = Color.Magenta
        tsbExit.Name = "tsbExit"
        tsbExit.Size = New Size(67, 29)
        tsbExit.Text = "Exit"
        ' 
        ' ToolStripButton1
        ' 
        ToolStripButton1.Image = My.Resources.Resources.Triangle_Arrow_Synchronize_2__Streamline_Core
        ToolStripButton1.ImageTransparentColor = Color.Magenta
        ToolStripButton1.Name = "ToolStripButton1"
        ToolStripButton1.Size = New Size(94, 29)
        ToolStripButton1.Text = "Reload"
        ' 
        ' TreeView1
        ' 
        TreeView1.Dock = DockStyle.Left
        TreeView1.Location = New Point(0, 34)
        TreeView1.Name = "TreeView1"
        TreeView1.Size = New Size(233, 1120)
        TreeView1.TabIndex = 6
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        TableLayoutPanel1.AutoSize = True
        TableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink
        TableLayoutPanel1.ColumnCount = 9
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle())
        TableLayoutPanel1.Location = New Point(3, 3)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 1
        TableLayoutPanel1.RowStyles.Add(New RowStyle())
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        TableLayoutPanel1.Size = New Size(0, 0)
        TableLayoutPanel1.TabIndex = 7
        ' 
        ' Panel1
        ' 
        Panel1.BorderStyle = BorderStyle.Fixed3D
        Panel1.Controls.Add(Panel4)
        Panel1.Controls.Add(Panel2)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(233, 34)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(2131, 1120)
        Panel1.TabIndex = 8
        ' 
        ' Panel4
        ' 
        Panel4.AutoScroll = True
        Panel4.BorderStyle = BorderStyle.FixedSingle
        Panel4.Controls.Add(TableLayoutPanel1)
        Panel4.Dock = DockStyle.Fill
        Panel4.Location = New Point(0, 71)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(2127, 1045)
        Panel4.TabIndex = 2
        ' 
        ' Panel2
        ' 
        Panel2.AutoSize = True
        Panel2.AutoSizeMode = AutoSizeMode.GrowAndShrink
        Panel2.Controls.Add(tlpHeader)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(0, 0)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(2127, 71)
        Panel2.TabIndex = 1
        ' 
        ' tlpHeader
        ' 
        tlpHeader.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        tlpHeader.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset
        tlpHeader.ColumnCount = 9
        tlpHeader.ColumnStyles.Add(New ColumnStyle())
        tlpHeader.ColumnStyles.Add(New ColumnStyle())
        tlpHeader.ColumnStyles.Add(New ColumnStyle())
        tlpHeader.ColumnStyles.Add(New ColumnStyle())
        tlpHeader.ColumnStyles.Add(New ColumnStyle())
        tlpHeader.ColumnStyles.Add(New ColumnStyle())
        tlpHeader.ColumnStyles.Add(New ColumnStyle())
        tlpHeader.ColumnStyles.Add(New ColumnStyle())
        tlpHeader.ColumnStyles.Add(New ColumnStyle())
        tlpHeader.Location = New Point(3, 3)
        tlpHeader.Name = "tlpHeader"
        tlpHeader.RowCount = 1
        tlpHeader.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        tlpHeader.Size = New Size(2121, 65)
        tlpHeader.TabIndex = 0
        ' 
        ' tsbPurge
        ' 
        tsbPurge.Image = CType(resources.GetObject("tsbPurge.Image"), Image)
        tsbPurge.ImageTransparentColor = Color.Magenta
        tsbPurge.Name = "tsbPurge"
        tsbPurge.Size = New Size(86, 29)
        tsbPurge.Text = "Purge"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(2364, 1190)
        Controls.Add(Panel1)
        Controls.Add(TreeView1)
        Controls.Add(ToolStrip1)
        Controls.Add(StatusStrip1)
        Name = "Form1"
        Text = "Allsky Images Backup Browser"
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        Panel2.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents StatusStrip1 As StatusStrip

    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents tsbSetup As ToolStripButton
    Friend WithEvents tsbExit As ToolStripButton
    Friend WithEvents TreeView1 As TreeView
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents tlpHeader As TableLayoutPanel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar1 As ToolStripProgressBar
    Friend WithEvents ToolStripButton1 As ToolStripButton
    Friend WithEvents tslblActiveFilter As ToolStripStatusLabel
    Friend WithEvents tstlblDaysCount As ToolStripStatusLabel
    Friend WithEvents tsslblCountCrap As ToolStripStatusLabel
    Friend WithEvents tsslblCountPass As ToolStripStatusLabel
    Friend WithEvents tsslblCountNice As ToolStripStatusLabel
    Friend WithEvents tsslblCountCool As ToolStripStatusLabel
    Friend WithEvents tsbPurge As ToolStripButton

End Class


