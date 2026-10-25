Public Class ZoomPictureBox
    Inherits Control

    Private _image As Image
    Private _zoom As Double = 1.0
    Private _offset As Point = Point.Empty
    Private _dragStart As Point
    Private _isDragging As Boolean = False

    Public Property Zoom As Double
        Get
            Return _zoom
        End Get
        Set(value As Double)
            _zoom = Math.Max(0.1, Math.Min(10.0, value)) ' clamp zoom
            Invalidate()
        End Set
    End Property

    Public Property DisplayImage As Image
        Get
            Return _image
        End Get
        Set(value As Image)
            _image = value
            ZoomToFit()
        End Set
    End Property

    Public Sub ZoomToFit()
        If _image Is Nothing Then Return

        Dim zoomX = Me.Width / _image.Width
        Dim zoomY = Me.Height / _image.Height
        _zoom = Math.Min(zoomX, zoomY)

        _offset = Point.Empty
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        If _image Is Nothing Then Return

        e.Graphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic

        Dim w = CInt(_image.Width * _zoom)
        Dim h = CInt(_image.Height * _zoom)

        e.Graphics.DrawImage(_image, New Rectangle(_offset.X, _offset.Y, w, h))
    End Sub

    Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)
        MyBase.OnMouseWheel(e)

        Dim oldZoom = _zoom
        If e.Delta > 0 Then
            _zoom *= 1.1
        Else
            _zoom /= 1.1
        End If

        _zoom = Math.Max(0.1, Math.Min(10.0, _zoom))

        ' keep zoom centered on mouse position
        Dim mx = e.X - _offset.X
        Dim my = e.Y - _offset.Y

        Dim scale = _zoom / oldZoom
        _offset.X -= CInt(mx * (scale - 1))
        _offset.Y -= CInt(my * (scale - 1))

        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        _isDragging = True
        _dragStart = e.Location
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        If _isDragging Then
            _offset.X += (e.X - _dragStart.X)
            _offset.Y += (e.Y - _dragStart.Y)
            _dragStart = e.Location
            Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        _isDragging = False
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        ZoomToFit()
    End Sub

    Protected Overrides Sub OnDoubleClick(e As EventArgs)
        MyBase.OnDoubleClick(e)
        ZoomToFit()
    End Sub
End Class


