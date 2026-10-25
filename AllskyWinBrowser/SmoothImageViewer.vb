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
            _zoom = value
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
            Invalidate()
        End Set
    End Property

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

