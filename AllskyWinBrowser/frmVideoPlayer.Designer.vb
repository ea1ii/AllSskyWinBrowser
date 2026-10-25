<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVideoPlayer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVideoPlayer))
        ToolStrip1 = New ToolStrip()
        tsbRev50 = New ToolStripButton()
        tsbRev10 = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        tsbPlay = New ToolStripButton()
        tsbPause = New ToolStripButton()
        tsbStop = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        tsbFwd10 = New ToolStripButton()
        tsbFwd50 = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        tsbFwdFrame = New ToolStripButton()
        tsbExit = New ToolStripButton()
        ImageList1 = New ImageList(components)
        VideoView1 = New LibVLCSharp.WinForms.VideoView()
        Timer1 = New Timer(components)
        ToolStrip1.SuspendLayout()
        CType(VideoView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {tsbRev50, tsbRev10, ToolStripSeparator2, tsbPlay, tsbPause, tsbStop, ToolStripSeparator1, tsbFwd10, tsbFwd50, ToolStripSeparator3, tsbFwdFrame, tsbExit})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(1857, 33)
        ToolStrip1.TabIndex = 1
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' tsbRev50
        ' 
        tsbRev50.DisplayStyle = ToolStripItemDisplayStyle.Image
        tsbRev50.Image = CType(resources.GetObject("tsbRev50.Image"), Image)
        tsbRev50.ImageTransparentColor = Color.Magenta
        tsbRev50.Name = "tsbRev50"
        tsbRev50.Size = New Size(34, 28)
        tsbRev50.Text = "ToolStripButton2"
        ' 
        ' tsbRev10
        ' 
        tsbRev10.DisplayStyle = ToolStripItemDisplayStyle.Image
        tsbRev10.Image = CType(resources.GetObject("tsbRev10.Image"), Image)
        tsbRev10.ImageTransparentColor = Color.Magenta
        tsbRev10.Name = "tsbRev10"
        tsbRev10.Size = New Size(34, 28)
        tsbRev10.Text = "ToolStripButton1"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 33)
        ' 
        ' tsbPlay
        ' 
        tsbPlay.DisplayStyle = ToolStripItemDisplayStyle.Image
        tsbPlay.Image = CType(resources.GetObject("tsbPlay.Image"), Image)
        tsbPlay.ImageTransparentColor = Color.Magenta
        tsbPlay.Name = "tsbPlay"
        tsbPlay.Size = New Size(34, 28)
        tsbPlay.Text = "ToolStripButton1"
        ' 
        ' tsbPause
        ' 
        tsbPause.DisplayStyle = ToolStripItemDisplayStyle.Image
        tsbPause.Image = CType(resources.GetObject("tsbPause.Image"), Image)
        tsbPause.ImageTransparentColor = Color.Magenta
        tsbPause.Name = "tsbPause"
        tsbPause.Size = New Size(34, 28)
        tsbPause.Text = "ToolStripButton1"
        ' 
        ' tsbStop
        ' 
        tsbStop.DisplayStyle = ToolStripItemDisplayStyle.Image
        tsbStop.Image = CType(resources.GetObject("tsbStop.Image"), Image)
        tsbStop.ImageTransparentColor = Color.Magenta
        tsbStop.Name = "tsbStop"
        tsbStop.Size = New Size(34, 28)
        tsbStop.Text = "ToolStripButton1"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 33)
        ' 
        ' tsbFwd10
        ' 
        tsbFwd10.DisplayStyle = ToolStripItemDisplayStyle.Image
        tsbFwd10.Image = CType(resources.GetObject("tsbFwd10.Image"), Image)
        tsbFwd10.ImageTransparentColor = Color.Magenta
        tsbFwd10.Name = "tsbFwd10"
        tsbFwd10.Size = New Size(34, 28)
        tsbFwd10.Text = "ToolStripButton1"
        ' 
        ' tsbFwd50
        ' 
        tsbFwd50.DisplayStyle = ToolStripItemDisplayStyle.Image
        tsbFwd50.Image = CType(resources.GetObject("tsbFwd50.Image"), Image)
        tsbFwd50.ImageTransparentColor = Color.Magenta
        tsbFwd50.Name = "tsbFwd50"
        tsbFwd50.Size = New Size(34, 28)
        tsbFwd50.Text = "ToolStripButton1"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 33)
        ' 
        ' tsbFwdFrame
        ' 
        tsbFwdFrame.DisplayStyle = ToolStripItemDisplayStyle.Image
        tsbFwdFrame.Image = CType(resources.GetObject("tsbFwdFrame.Image"), Image)
        tsbFwdFrame.ImageTransparentColor = Color.Magenta
        tsbFwdFrame.Name = "tsbFwdFrame"
        tsbFwdFrame.Size = New Size(34, 28)
        tsbFwdFrame.Text = "ToolStripButton1"
        ' 
        ' tsbExit
        ' 
        tsbExit.Alignment = ToolStripItemAlignment.Right
        tsbExit.DisplayStyle = ToolStripItemDisplayStyle.Image
        tsbExit.Image = CType(resources.GetObject("tsbExit.Image"), Image)
        tsbExit.ImageTransparentColor = Color.Magenta
        tsbExit.Name = "tsbExit"
        tsbExit.Size = New Size(34, 28)
        tsbExit.Text = "ToolStripButton1"
        ' 
        ' ImageList1
        ' 
        ImageList1.ColorDepth = ColorDepth.Depth32Bit
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), ImageListStreamer)
        ImageList1.TransparentColor = Color.Transparent
        ImageList1.Images.SetKeyName(0, "Close")
        ' 
        ' VideoView1
        ' 
        VideoView1.BackColor = Color.Black
        VideoView1.Dock = DockStyle.Fill
        VideoView1.Location = New Point(0, 33)
        VideoView1.MediaPlayer = Nothing
        VideoView1.Name = "VideoView1"
        VideoView1.Size = New Size(1857, 1248)
        VideoView1.TabIndex = 2
        VideoView1.TabStop = False
        VideoView1.Text = "VideoView1"
        ' 
        ' Timer1
        ' 
        Timer1.Interval = 150
        ' 
        ' frmVideoPlayer
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1857, 1281)
        Controls.Add(VideoView1)
        Controls.Add(ToolStrip1)
        DoubleBuffered = True
        KeyPreview = True
        Name = "frmVideoPlayer"
        Text = "frmVideoPlayer"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(VideoView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents tsbExit As ToolStripButton
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents VideoView1 As LibVLCSharp.WinForms.VideoView
    Friend WithEvents tsbRev50 As ToolStripButton
    Friend WithEvents tsbRev10 As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents tsbPlay As ToolStripButton
    Friend WithEvents tsbPause As ToolStripButton
    Friend WithEvents tsbStop As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents tsbFwd10 As ToolStripButton
    Friend WithEvents tsbFwd50 As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents tsbFwdFrame As ToolStripButton
    Friend WithEvents Timer1 As Timer
End Class
