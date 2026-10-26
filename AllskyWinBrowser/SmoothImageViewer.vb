Public Class SmoothImageViewer
    Inherits Control

    Private _image As Image
    Private _zoom As Single = 1.0F
    Private _offset As Point = Point.Empty

    Public Sub New()
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint Or
                    ControlStyles.UserPaint Or
                    ControlStyles.OptimizedDoubleBuffer, True)
        Me.UpdateStyles()
    End Sub

    Public Property ViewerImage As Image
        Get
            Return _image
        End Get
        Set(value As Image)
            _image = value
            Invalidate()
        End Set
    End Property

    Public Property Zoom As Single
        Get
            Return _zoom
        End Get
        Set(value As Single)
            _zoom = Math.Max(0.01F, value)
            ClampOffset()
            Invalidate()
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

    Public Property Offset As Point
        Get
            Return _offset
        End Get
        Set(value As Point)
            _offset = value
            ClampOffset()
            Invalidate()
        End Set
    End Property

    Public Sub ZoomToLine(p1 As PointF, p2 As PointF)

        Dim minX As Single = Math.Min(p1.X, p2.X)
        Dim maxX As Single = Math.Max(p1.X, p2.X)

        Dim minY As Single = Math.Min(p1.Y, p2.Y)
        Dim maxY As Single = Math.Max(p1.Y, p2.Y)

        Dim boxWidth As Single = Math.Max(1, maxX - minX)
        Dim boxHeight As Single = Math.Max(1, maxY - minY)

        Dim zoomX As Single = ClientSize.Width / boxWidth
        Dim zoomY As Single = ClientSize.Height / boxHeight

        _zoom = Math.Min(zoomX, zoomY) * 0.25F

        Dim midX As Single = (p1.X + p2.X) / 2.0F
        Dim midY As Single = (p1.Y + p2.Y) / 2.0F

        _offset = New Point(
        CInt(ClientSize.Width / 2.0F - midX * _zoom),
        CInt(ClientSize.Height / 2.0F - midY * _zoom))

        Invalidate()

    End Sub

    Private Sub ClampOffset()

        If _image Is Nothing Then Return

        Dim imageWidth As Integer = CInt(_image.Width * _zoom)
        Dim imageHeight As Integer = CInt(_image.Height * _zoom)

        Dim x As Integer = _offset.X
        Dim y As Integer = _offset.Y

        ' Horizontal
        If imageWidth <= ClientSize.Width Then

            ' Image fits completely
            x = (ClientSize.Width - imageWidth) \ 2

        Else

            Dim minX As Integer = ClientSize.Width - imageWidth
            Dim maxX As Integer = 0

            x = Math.Max(minX, Math.Min(maxX, x))

        End If

        ' Vertical
        If imageHeight <= ClientSize.Height Then

            ' Image fits completely
            y = (ClientSize.Height - imageHeight) \ 2

        Else

            Dim minY As Integer = ClientSize.Height - imageHeight
            Dim maxY As Integer = 0

            y = Math.Max(minY, Math.Min(maxY, y))

        End If

        _offset = New Point(x, y)

    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        If _image Is Nothing Then Return

        Dim w = _image.Width * _zoom
        Dim h = _image.Height * _zoom

        e.Graphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        e.Graphics.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality

        e.Graphics.DrawImage(_image, New Rectangle(_offset.X, _offset.Y, w, h))
    End Sub
End Class

