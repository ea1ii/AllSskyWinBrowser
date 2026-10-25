<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ImageViewer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ImageViewer))
        ImageList1 = New ImageList(components)
        ToolStrip1 = New ToolStrip()
        tsbZoomIn = New ToolStripButton()
        tsbZoomOut = New ToolStripButton()
        tsbZoomActual = New ToolStripButton()
        tsbZoomReset = New ToolStripButton()
        tsbExit = New ToolStripButton()
        StatusStrip1 = New StatusStrip()
        tsslblCoords = New ToolStripStatusLabel()
        ToolStrip1.SuspendLayout()
        StatusStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ImageList1
        ' 
        ImageList1.ColorDepth = ColorDepth.Depth32Bit
        ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), ImageListStreamer)
        ImageList1.TransparentColor = Color.Transparent
        ImageList1.Images.SetKeyName(0, "Close")
        ImageList1.Images.SetKeyName(1, "ZoomIn")
        ImageList1.Images.SetKeyName(2, "ZoomOut")
        ImageList1.Images.SetKeyName(3, "ZoomReset")
        ImageList1.Images.SetKeyName(4, "ZoomActual")
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {tsbZoomIn, tsbZoomOut, tsbZoomActual, tsbZoomReset, tsbExit})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(1773, 33)
        ToolStrip1.TabIndex = 0
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' tsbZoomIn
        ' 
        tsbZoomIn.DisplayStyle = ToolStripItemDisplayStyle.Image
        tsbZoomIn.Image = CType(resources.GetObject("tsbZoomIn.Image"), Image)
        tsbZoomIn.ImageTransparentColor = Color.Magenta
        tsbZoomIn.Name = "tsbZoomIn"
        tsbZoomIn.Size = New Size(34, 28)
        tsbZoomIn.Text = "ToolStripButton1"
        ' 
        ' tsbZoomOut
        ' 
        tsbZoomOut.DisplayStyle = ToolStripItemDisplayStyle.Image
        tsbZoomOut.Image = CType(resources.GetObject("tsbZoomOut.Image"), Image)
        tsbZoomOut.ImageTransparentColor = Color.Magenta
        tsbZoomOut.Name = "tsbZoomOut"
        tsbZoomOut.Size = New Size(34, 28)
        tsbZoomOut.Text = "ToolStripButton2"
        ' 
        ' tsbZoomActual
        ' 
        tsbZoomActual.DisplayStyle = ToolStripItemDisplayStyle.Image
        tsbZoomActual.Image = CType(resources.GetObject("tsbZoomActual.Image"), Image)
        tsbZoomActual.ImageTransparentColor = Color.Magenta
        tsbZoomActual.Name = "tsbZoomActual"
        tsbZoomActual.Size = New Size(34, 28)
        tsbZoomActual.Text = "ToolStripButton3"
        ' 
        ' tsbZoomReset
        ' 
        tsbZoomReset.DisplayStyle = ToolStripItemDisplayStyle.Image
        tsbZoomReset.Image = CType(resources.GetObject("tsbZoomReset.Image"), Image)
        tsbZoomReset.ImageTransparentColor = Color.Magenta
        tsbZoomReset.Name = "tsbZoomReset"
        tsbZoomReset.Size = New Size(34, 28)
        tsbZoomReset.Text = "ToolStripButton4"
        ' 
        ' tsbExit
        ' 
        tsbExit.Alignment = ToolStripItemAlignment.Right
        tsbExit.DisplayStyle = ToolStripItemDisplayStyle.Image
        tsbExit.Image = CType(resources.GetObject("tsbExit.Image"), Image)
        tsbExit.ImageTransparentColor = Color.Magenta
        tsbExit.Name = "tsbExit"
        tsbExit.Size = New Size(34, 28)
        tsbExit.Text = "ToolStripButton5"
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(24, 24)
        StatusStrip1.Items.AddRange(New ToolStripItem() {tsslblCoords})
        StatusStrip1.Location = New Point(0, 1092)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Size = New Size(1773, 32)
        StatusStrip1.TabIndex = 1
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' tsslblCoords
        ' 
        tsslblCoords.BorderStyle = Border3DStyle.SunkenInner
        tsslblCoords.Name = "tsslblCoords"
        tsslblCoords.Size = New Size(65, 25)
        tsslblCoords.Text = "(X: , Y:)"
        ' 
        ' ImageViewer
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1773, 1124)
        Controls.Add(StatusStrip1)
        Controls.Add(ToolStrip1)
        DoubleBuffered = True
        KeyPreview = True
        MinimizeBox = False
        Name = "ImageViewer"
        ShowInTaskbar = False
        Text = "Image Viewer"
        TopMost = True
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents tsbZoomIn As ToolStripButton
    Friend WithEvents tsbZoomOut As ToolStripButton
    Friend WithEvents tsbZoomActual As ToolStripButton
    Friend WithEvents tsbZoomReset As ToolStripButton
    Friend WithEvents tsbExit As ToolStripButton
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents tsslblCoords As ToolStripStatusLabel
End Class
