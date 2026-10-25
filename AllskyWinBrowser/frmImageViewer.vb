Public Class ImageViewer
    Dim WithEvents canvas As SmoothImageViewer
    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(imagePath As String)
        Me.New

        canvas = New SmoothImageViewer

        canvas.Dock = DockStyle.Fill
        canvas.Zoom = 1.0
        canvas.TabStop = False

        canvas.ViewerImage = Image.FromFile(imagePath)

        AddHandler canvas.MouseWheel, AddressOf canvas_MouseWheel
        AddHandler canvas.MouseDown, AddressOf canvas_MouseDown
        AddHandler canvas.MouseMove, AddressOf canvas_MouseMove
        AddHandler canvas.MouseUp, AddressOf canvas_MouseUp
        AddHandler canvas.MouseLeave, AddressOf canvas_MouseLeave
        Me.Controls.Add(canvas)

        Me.Text = "Image Viewer - " & imagePath

        canvas.ZoomToFit()
    End Sub

    Private Sub canvas_MouseLeave(sender As Object, e As EventArgs)
        tsslblCoords.Text = "(X:-- , Y:--)"
    End Sub

    Private Sub canvas_MouseWheel(sender As Object, e As MouseEventArgs) Handles canvas.MouseWheel
        Dim delta As Single = If(e.Delta > 0, 1.1F, 1 / 1.1F)
        ZoomAtPoint(canvas, delta, New PointF(e.X, e.Y))
    End Sub

    Private dragging As Boolean = False
    Private lastPos As Point

    Private Sub canvas_MouseDown(sender As Object, e As MouseEventArgs) Handles canvas.MouseDown
        dragging = True
        lastPos = e.Location
    End Sub

    Private Sub canvas_MouseMove(sender As Object, e As MouseEventArgs) Handles canvas.MouseMove
        If dragging Then
            canvas.Offset = New Point(
            canvas.Offset.X + (e.X - lastPos.X),
            canvas.Offset.Y + (e.Y - lastPos.Y)
        )
            lastPos = e.Location
            tsslblCoords.Text = "(X:-- , Y:--)"
        Else
            Dim imgPoint = ScreenToImage(canvas, e.Location)
            If imgPoint.X < 0 Or imgPoint.Y < 0 Or imgPoint.X >= canvas.ViewerImage.Width Or imgPoint.Y >= canvas.ViewerImage.Height Then
                tsslblCoords.Text = "(X:-- , Y:--)"
            Else
                tsslblCoords.Text = $"(X:{imgPoint.X} , Y:{imgPoint.Y})"
            End If
        End If
    End Sub

    Private Function ScreenToImage(canvas As SmoothImageViewer, screenPoint As Point) As Point
        Dim imgX = (screenPoint.X - canvas.Offset.X) / canvas.Zoom
        Dim imgY = (screenPoint.Y - canvas.Offset.Y) / canvas.Zoom
        Return New Point(CInt(imgX), CInt(imgY))
    End Function

    Private Sub canvas_MouseUp(sender As Object, e As MouseEventArgs) Handles canvas.MouseUp
        dragging = False
    End Sub

    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click
        Me.Close()
    End Sub

    Private Sub tsbZoomIn_Click(sender As Object, e As EventArgs) Handles tsbZoomIn.Click
        ZoomAtCenter(canvas, 1.25F)
    End Sub

    Private Sub tsbZoomOut_Click(sender As Object, e As EventArgs) Handles tsbZoomOut.Click
        ZoomAtCenter(canvas, 1 / 1.25F)
    End Sub

    Private Sub tsbZoomActual_Click(sender As Object, e As EventArgs) Handles tsbZoomActual.Click
        canvas.Zoom = 1.0
    End Sub

    Private Sub tsbZoomReset_Click(sender As Object, e As EventArgs) Handles tsbZoomReset.Click
        canvas.ZoomToFit()
    End Sub

    Private Sub ImageViewer_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub ZoomAtPoint(canvas As SmoothImageViewer, zoomDelta As Single, zoomPoint As PointF)
        Dim oldZoom = canvas.Zoom
        Dim newZoom = oldZoom * zoomDelta

        canvas.Zoom = newZoom

        ' Adjust offset so zoomPoint stays visually fixed
        canvas.Offset = New Point(
        CInt(canvas.Offset.X - zoomPoint.X * (newZoom - oldZoom)),
        CInt(canvas.Offset.Y - zoomPoint.Y * (newZoom - oldZoom))
    )
    End Sub

    Private Sub ZoomAtCenter(canvas As SmoothImageViewer, zoomDelta As Single)
        Dim center As New PointF(canvas.Width / 2, canvas.Height / 2)
        ZoomAtPoint(canvas, zoomDelta, center)
    End Sub

    Private Function ClampToScreen(p As Point) As Point
        Dim screen = System.Windows.Forms.Screen.FromPoint(p).WorkingArea
        Dim x = Math.Max(screen.Left, Math.Min(p.X, screen.Right - Me.Width))
        Dim y = Math.Max(screen.Top, Math.Min(p.Y, screen.Bottom - Me.Height))
        Return New Point(x, y)
    End Function

    Private Sub ImageViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = My.Settings.ImageViewerWidth
        Me.Height = My.Settings.ImageViewerHeight

        Me.Location = ClampToScreen(New Point(My.Settings.ImageViewerX, My.Settings.ImageViewerY))
    End Sub

    Private Sub ImageViewer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        My.Settings.ImageViewerHeight = Me.Height
        My.Settings.ImageViewerWidth = Me.Width
        My.Settings.ImageViewerX = Me.Location.X
        My.Settings.ImageViewerY = Me.Location.Y
        My.Settings.Save()
    End Sub
End Class


