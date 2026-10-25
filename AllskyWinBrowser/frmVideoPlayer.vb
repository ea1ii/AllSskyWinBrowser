Imports LibVLCSharp.Shared
Imports LibVLCSharp.WinForms

Public Class frmVideoPlayer

    Private libVLC As LibVLC
    Private mediaPlayer As MediaPlayer
    Private _path As String

    Public Sub New(path As String)
        InitializeComponent()
        _path = path

        Me.Text = "Video player - " & path
    End Sub

    Private Sub frmVideoPlayer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Core.Initialize()

        libVLC = New LibVLC()
        mediaPlayer = New MediaPlayer(libVLC)

        VideoView1.MediaPlayer = mediaPlayer

        Dim media = New Media(libVLC, _path, FromType.FromPath)
        mediaPlayer.Play(media)
    End Sub
    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click
        Me.Close()
    End Sub

    Private Sub tsbRev50_Click(sender As Object, e As EventArgs) Handles tsbRev50.Click
        mediaPlayer.Time -= 10000
    End Sub

    Private Sub tsbRev10_Click(sender As Object, e As EventArgs) Handles tsbRev10.Click
        mediaPlayer.Time -= 5000
    End Sub

    Private Sub tsbPlay_Click(sender As Object, e As EventArgs) Handles tsbPlay.Click
        mediaPlayer.Play()
    End Sub

    Private Sub tsbPause_Click(sender As Object, e As EventArgs) Handles tsbPause.Click
        mediaPlayer.Pause()
    End Sub

    Private Sub tsbStop_Click(sender As Object, e As EventArgs) Handles tsbStop.Click
        mediaPlayer.Stop()
    End Sub

    Private Sub tsbFwd10_Click(sender As Object, e As EventArgs) Handles tsbFwd10.Click
        mediaPlayer.Time += 5000
    End Sub

    Private Sub tsbFwd50_Click(sender As Object, e As EventArgs) Handles tsbFwd50.Click
        mediaPlayer.Time += 10000
    End Sub

    Private Sub tsbFwdFrame_Click(sender As Object, e As EventArgs) Handles tsbFwdFrame.Click
        'mediaPlayer.Pause()
        'mediaPlayer.NextFrame()
    End Sub

    Private Sub tsbFwdFrame_MouseDown(sender As Object, e As MouseEventArgs) Handles tsbFwdFrame.MouseDown
        mediaPlayer.NextFrame()
        Timer1.Start()
    End Sub

    Private Sub tsbFwdFrame_MouseUp(sender As Object, e As MouseEventArgs) Handles tsbFwdFrame.MouseUp
        Timer1.Stop()
    End Sub

    Private Sub tsbFwdFrame_MouseLeave(sender As Object, e As EventArgs) Handles tsbFwdFrame.MouseLeave
        Timer1.Stop()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        mediaPlayer.NextFrame()
    End Sub

    Private Sub frmVideoPlayer_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Function ClampToScreen(p As Point) As Point
        Dim screen = System.Windows.Forms.Screen.FromPoint(p).WorkingArea
        Dim x = Math.Max(screen.Left, Math.Min(p.X, screen.Right - Me.Width))
        Dim y = Math.Max(screen.Top, Math.Min(p.Y, screen.Bottom - Me.Height))
        Return New Point(x, y)
    End Function

    Private Sub ImageViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = My.Settings.VideoViewerWidth
        Me.Height = My.Settings.VideoViewerHight

        Me.Location = ClampToScreen(New Point(My.Settings.VideoViewerX, My.Settings.VideoViewerY))
    End Sub
    Private Sub frmVideoPlayer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        My.Settings.VideoViewerHight = Me.Height
        My.Settings.VideoViewerWidth = Me.Width
        My.Settings.VideoViewerX = Me.Location.X
        My.Settings.VideoViewerY = Me.Location.Y
        My.Settings.Save()
    End Sub
End Class