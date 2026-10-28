Imports System.Runtime.InteropServices.JavaScript.JSType
Imports System.Text.Json.Nodes

Public Class ImageViewer
    Dim WithEvents canvas As SmoothImageViewer
    Dim meteorNum As Integer = 0
    Dim metIdx As Integer = 0
    Dim metsArray As New List(Of meteorPoint)
    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(imagePath As String, Optional metaPath As String = "")
        Me.New

        canvas = New SmoothImageViewer

        canvas.Dock = DockStyle.Fill
        canvas.Zoom = 1.0
        canvas.TabStop = False

        Using fs As New System.IO.FileStream(imagePath, IO.FileMode.Open, IO.FileAccess.Read)
            Using tmp As Image = Image.FromStream(fs)
                canvas.ViewerImage = New Bitmap(tmp)
            End Using
        End Using
        'canvas.ViewerImage = Image.FromFile(imagePath)

        AddHandler canvas.MouseWheel, AddressOf canvas_MouseWheel
        AddHandler canvas.MouseDown, AddressOf canvas_MouseDown
        AddHandler canvas.MouseMove, AddressOf canvas_MouseMove
        AddHandler canvas.MouseUp, AddressOf canvas_MouseUp
        AddHandler canvas.MouseLeave, AddressOf canvas_MouseLeave
        Me.Controls.Add(canvas)

        Me.Text = "Image Viewer - " & imagePath

        canvas.ZoomToFit()

        If metaPath <> "" AndAlso IO.File.Exists(metaPath) Then
            Dim json As String = IO.File.ReadAllText(metaPath)
            Dim arr As JsonArray = JsonArray.Parse(json)

            For Each met In arr
                meteorNum += 1
                Dim p1a = met("p1").AsArray()
                Dim p2a = met("p2").AsArray()

                Dim p1 As New PointF(CSng(p1a(0).GetValue(Of Double)()), CSng(p1a(1).GetValue(Of Double)()))
                Dim p2 As New PointF(CSng(p2a(0).GetValue(Of Double)()), CSng(p2a(1).GetValue(Of Double)()))

                Dim centre As PointF
                centre = New PointF((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2)
                Dim dist As Double = Math.Sqrt((p1.X - p2.X) ^ 2 + (p1.Y - p2.Y) ^ 2)

                'Dim mp As meteorPoint = New meteorPoint(centre, dist)
                Dim mp As meteorPoint = New meteorPoint(p1, p2)

                metsArray.Add(mp)

            Next
            tsbMeteor.Visible = True
        End If
    End Sub

    ''' <summary>
    ''' Reset the coordinates label when the mouse leaves the canvas area.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub canvas_MouseLeave(sender As Object, e As EventArgs)
        tsslblCoords.Text = "(X:-- , Y:--)"
    End Sub

    ''' <summary>
    ''' Handle mouse wheel events to zoom in or out of the image at the mouse cursor's position.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub canvas_MouseWheel(sender As Object, e As MouseEventArgs) Handles canvas.MouseWheel
        Dim delta As Single = If(e.Delta > 0, 1.1F, 1 / 1.1F)
        ZoomAtPoint(canvas, delta, New PointF(e.X, e.Y))
    End Sub

    Private dragging As Boolean = False
    Private lastPos As Point

    ''' <summary>
    ''' Handle mouse down events to initiate dragging of the image.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub canvas_MouseDown(sender As Object, e As MouseEventArgs) Handles canvas.MouseDown
        dragging = True
        lastPos = e.Location
    End Sub

    ''' <summary>
    ''' Handle mouse move events to either drag the image or update the coordinates label based on the mouse position.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
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

    ''' <summary>
    ''' Convert a point from screen coordinates to image coordinates based on the current zoom and offset of the canvas.
    ''' </summary>
    ''' <param name="canvas"></param>
    ''' <param name="screenPoint"></param>
    ''' <returns></returns>
    Private Function ScreenToImage(canvas As SmoothImageViewer, screenPoint As Point) As Point
        Dim imgX = (screenPoint.X - canvas.Offset.X) / canvas.Zoom
        Dim imgY = (screenPoint.Y - canvas.Offset.Y) / canvas.Zoom
        Return New Point(CInt(imgX), CInt(imgY))
    End Function

    ''' <summary>
    ''' Handle mouse up events to stop dragging the image.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub canvas_MouseUp(sender As Object, e As MouseEventArgs) Handles canvas.MouseUp
        dragging = False
    End Sub

    ''' <summary>
    ''' Handle the exit button click event to close the image viewer form.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' Handle the zoom in button click event to zoom in on the image at the center of the canvas.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tsbZoomIn_Click(sender As Object, e As EventArgs) Handles tsbZoomIn.Click
        ZoomAtCenter(canvas, 1.25F)
    End Sub

    ''' <summary>
    ''' Handle the zoom out button click event to zoom out of the image at the center of the canvas.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tsbZoomOut_Click(sender As Object, e As EventArgs) Handles tsbZoomOut.Click
        ZoomAtCenter(canvas, 1 / 1.25F)
    End Sub

    ''' <summary>
    ''' Handle the zoom actual size button click event to reset the zoom level to 1.0 (actual size).
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tsbZoomActual_Click(sender As Object, e As EventArgs) Handles tsbZoomActual.Click
        canvas.Zoom = 1.0
    End Sub

    ''' <summary>
    ''' Handle the zoom to fit button click event to adjust the zoom level so that the entire image fits within the canvas.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tsbZoomReset_Click(sender As Object, e As EventArgs) Handles tsbZoomReset.Click
        canvas.ZoomToFit()
    End Sub

    ''' <summary>
    ''' Handle key down events to close the image viewer form when the Escape key is pressed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ImageViewer_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Zoom the image at a specific point on the canvas by adjusting the zoom level and offset to keep the specified point visually fixed.
    ''' </summary>
    ''' <param name="canvas"></param>
    ''' <param name="zoomDelta"></param>
    ''' <param name="zoomPoint"></param>
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

    ''' <summary>
    ''' Zoom the image at the center of the canvas by calling ZoomAtPoint with the center point of the canvas.
    ''' </summary>
    ''' <param name="canvas"></param>
    ''' <param name="zoomDelta"></param>
    Private Sub ZoomAtCenter(canvas As SmoothImageViewer, zoomDelta As Single)
        Dim center As New PointF(canvas.Width / 2, canvas.Height / 2)
        ZoomAtPoint(canvas, zoomDelta, center)
    End Sub

    ''' <summary>
    ''' Clamp the form's location to ensure it stays within the bounds of the screen's working area, preventing it from being positioned off-screen.
    ''' </summary>
    ''' <param name="p"></param>
    ''' <returns></returns>
    Private Function ClampToScreen(p As Point) As Point
        Dim screen = System.Windows.Forms.Screen.FromPoint(p).WorkingArea
        Dim x = Math.Max(screen.Left, Math.Min(p.X, screen.Right - Me.Width))
        Dim y = Math.Max(screen.Top, Math.Min(p.Y, screen.Bottom - Me.Height))
        Return New Point(x, y)
    End Function

    ''' <summary>
    ''' Handle the form's Load event to restore the form's size and position from the application settings, ensuring it opens in the same state as when it was last closed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ImageViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = My.Settings.ImageViewerWidth
        Me.Height = My.Settings.ImageViewerHeight

        Me.Location = ClampToScreen(New Point(My.Settings.ImageViewerX, My.Settings.ImageViewerY))
    End Sub

    ''' <summary>
    ''' Handle the form's FormClosing event to save the current size and position of the form to the application settings, allowing it to be restored the next time the form is opened.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ImageViewer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        My.Settings.ImageViewerHeight = Me.Height
        My.Settings.ImageViewerWidth = Me.Width
        My.Settings.ImageViewerX = Me.Location.X
        My.Settings.ImageViewerY = Me.Location.Y
        My.Settings.Save()
    End Sub

    ''' <summary>
    ''' Handle the meteor button click event to cycle through the detected meteors in the image, zooming in on each meteor's location in sequence.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub tsbMeteor_Click(sender As Object, e As EventArgs) Handles tsbMeteor.Click
        metIdx += 1
        If metIdx >= meteorNum Then
            metIdx = 0
        End If

        'ZoomAtPoint(canvas, canvas.Height / metsArray(metIdx).Distance / 10.0, metsArray(metIdx).Centre)
        canvas.ZoomToLine(metsArray(metIdx).P1, metsArray(metIdx).P2)

    End Sub

End Class

''' <summary>
''' Represents a meteor point with its center, distance, and endpoints. This class is used to store information about detected meteors in an image, including their center point, the distance between two points (p1 and p2), and the coordinates of those points.
''' </summary>
Public Class meteorPoint
    Private _Centre As PointF
    Private _distance As Double
    Private _p1 As PointF
    Private _p2 As PointF

    Public Sub New(centre As PointF, distance As Double)
        _Centre = centre
        _distance = distance
    End Sub
    Public Sub New(p1 As PointF, p2 As PointF)
        _p1 = p1
        _p2 = p2
        _Centre = New PointF((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2)
        _distance = Math.Sqrt((p1.X - p2.X) ^ 2 + (p1.Y - p2.Y) ^ 2)
    End Sub
    Public ReadOnly Property Centre As PointF
        Get
            Return _Centre
        End Get
    End Property
    Public ReadOnly Property Distance As Double
        Get
            Return _distance
        End Get
    End Property
    Public ReadOnly Property P1 As PointF
        Get
            Return _p1
        End Get
    End Property
    Public ReadOnly Property P2 As PointF
        Get
            Return _p2
        End Get
    End Property
End Class

