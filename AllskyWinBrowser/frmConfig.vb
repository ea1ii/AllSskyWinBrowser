Public Class frmConfig
    Private _cfg As SettingsSnapshot

    Public Sub New(cfg As SettingsSnapshot)
        InitializeComponent()
        _cfg = cfg
    End Sub

    Private Sub frmConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'set no focus buttons
        Dim btnDT As New NoFocusButton()
        btnDT.Name = "btnInsertDT"
        btnDT.Text = "[DT]"
        btnDT.Size = New Size(61, 41)
        btnDT.Location = New Point(465, 300)
        btnDT.TabStop = False

        AddHandler btnDT.Click, AddressOf btnInsertDateTime_Click
        Me.Controls.Add(btnDT)

        Dim btnD As New NoFocusButton()
        btnD.Name = "btnInsertD"
        btnD.Text = "[D]"
        btnD.Size = New Size(61, 41)
        btnD.Location = New Point(530, 300)
        btnD.TabStop = False

        AddHandler btnD.Click, AddressOf btnInsertDate_Click
        Me.Controls.Add(btnD)

        'put data in controls

        Me.txtbDateFormat.Text = _cfg.DateFormat
        Me.txtbDatetimeFormat.Text = _cfg.DatetimeFormat
        Me.txtbBasePath.Text = _cfg.BasePath
        Me.txtbDayFolderMask.Text = _cfg.DayFolderMask

        Me.txtbNightTimelapsePath.Text = _cfg.NightTimelapsePath
        Me.txtbNightTimelapseMask.Text = _cfg.NightTimelapseMask
        Me.txtbNightTimelapseThumbnailPath.Text = _cfg.NightTimelapseThumbnailPath
        Me.txtbNightTimelapseThumbnailMask.Text = _cfg.NightTimelapseThumbnailMask

        Me.txtbDayTimelapsePath.Text = _cfg.DayTimelapsePath
        Me.txtbDayTimelapseMask.Text = _cfg.DayTimelapseMask
        Me.txtbDayTimeLapseThumbnailPath.Text = _cfg.DayTimeLapseThumbnailPath
        Me.txtbDayTimeLapseThumbnailMask.Text = _cfg.DayTimelapseThumbnailMask

        Me.txtbKeogramPath.Text = _cfg.KeogramPath
        Me.txtbKeogramMask.Text = _cfg.KeogramMask
        Me.txtbKeogramThumbnailPath.Text = _cfg.KeogramThumbnailPath
        Me.txtbKeogramThumbnailMask.Text = _cfg.KeogramThumbnailMask

        Me.txtbDayKeogramPath.Text = _cfg.DayKeogramPath
        Me.txtbDayKeogramMask.Text = _cfg.DayKeogramMask
        Me.txtbDayKeogramThumbnailPath.Text = _cfg.DayKeogramThumbnailPath
        Me.txtbDayKeogramThumbnailMask.Text = _cfg.DayKeogramThumbnailMask

        Me.txtbKeolapsePath.Text = _cfg.KeolapsePath
        Me.txtbKeolapseMask.Text = _cfg.KeolapseMask
        Me.txtbKeolapseThumbnailPath.Text = _cfg.KeolapseThumbnailPath
        Me.txtbKeolapseThumbnailMask.Text = _cfg.KeolapseThumbnailMask

        Me.txtbStartrailPath.Text = _cfg.StartrailPath
        Me.txtbStartrailMask.Text = _cfg.StartrailMask
        Me.txtbStartrailThumbnailPath.Text = _cfg.StartrailThumbnailPath
        Me.txtbStartrailThumbnailMask.Text = _cfg.StartrailThumbnailMask

        Me.txtbStartraillapsePath.Text = _cfg.StartraillapsePath
        Me.txtbStartraillapseMask.Text = _cfg.StartraillapseMask
        Me.txtbStartraillapseThumbnailPath.Text = _cfg.StartraillapseThumbnailPath
        Me.txtbStartraillapseThumbnailMask.Text = _cfg.StartraillapseThumbnailMask

        Me.txtbRecapPath.Text = _cfg.RecapPath
        Me.txtbRecapImageMask.Text = _cfg.RecapImageMask
        Me.txtbRecapVideoMask.Text = _cfg.RecapVideoMask
        Me.txtbRecapMetadataMask.Text = _cfg.RecapMetadataMask

        Me.txtbMeteorPath.Text = _cfg.MeteorPath
        Me.txtbMeteorMask.Text = _cfg.MeteorMask
        Me.txtbMeteorThumbnailPath.Text = _cfg.MeteorThumbnailPath
        Me.txtbMeteorThumbnailMask.Text = _cfg.MeteorThumbnailMask
        Me.txtbMeteorMarkedMask.Text = _cfg.MeteorMarkedMask
        Me.txtbMeteorMarkedThumbnailMask.Text = _cfg.MeteorMarkedThumbnailMask
        Me.txtbMeteorMetadataMask.Text = _cfg.MeteorMetadataMask

        Me.chkbShowEmptyFolders.Checked = _cfg.ShowEmptyFolders
        Me.chkbDeleteEmptyFolders.Checked = _cfg.DeleteEmptyFolders

        Me.txtbThumbnailWidth.Text = _cfg.ThumbnailWidth.ToString()
        Me.txtbThumbnailHeight.Text = _cfg.ThumbnailHeight.ToString()
        Me.chkbThumbnailCreate.Checked = _cfg.ThumbnailCreate

    End Sub

    Private Sub btnAcept_Click(sender As Object, e As EventArgs) Handles btnAcept.Click
        'get date from controls
        _cfg.DateFormat = Me.txtbDateFormat.Text
        _cfg.DatetimeFormat = Me.txtbDatetimeFormat.Text
        _cfg.BasePath = Me.txtbBasePath.Text
        _cfg.DayFolderMask = Me.txtbDayFolderMask.Text

        _cfg.NightTimelapsePath = Me.txtbNightTimelapsePath.Text
        _cfg.NightTimelapseMask = Me.txtbNightTimelapseMask.Text
        _cfg.NightTimelapseThumbnailPath = Me.txtbNightTimelapseThumbnailPath.Text
        _cfg.NightTimelapseThumbnailMask = Me.txtbNightTimelapseThumbnailMask.Text

        _cfg.DayTimelapsePath = Me.txtbDayTimelapsePath.Text
        _cfg.DayTimelapseMask = Me.txtbDayTimelapseMask.Text
        _cfg.DayTimeLapseThumbnailPath = Me.txtbDayTimeLapseThumbnailPath.Text
        _cfg.DayTimelapseThumbnailMask = Me.txtbDayTimeLapseThumbnailMask.Text

        _cfg.KeogramPath = Me.txtbKeogramPath.Text
        _cfg.KeogramMask = Me.txtbKeogramMask.Text
        _cfg.KeogramThumbnailPath = Me.txtbKeogramThumbnailPath.Text
        _cfg.KeogramThumbnailMask = Me.txtbKeogramThumbnailMask.Text

        _cfg.DayKeogramPath = Me.txtbDayKeogramPath.Text
        _cfg.DayKeogramMask = Me.txtbDayKeogramMask.Text
        _cfg.DayKeogramThumbnailPath = Me.txtbDayKeogramThumbnailPath.Text
        _cfg.DayKeogramThumbnailMask = Me.txtbDayKeogramThumbnailMask.Text

        _cfg.KeolapsePath = Me.txtbKeolapsePath.Text
        _cfg.KeolapseMask = Me.txtbKeolapseMask.Text
        _cfg.KeolapseThumbnailPath = Me.txtbKeolapseThumbnailPath.Text
        _cfg.KeolapseThumbnailMask = Me.txtbKeolapseThumbnailMask.Text

        _cfg.StartrailPath = Me.txtbStartrailPath.Text
        _cfg.StartrailMask = Me.txtbStartrailMask.Text
        _cfg.StartrailThumbnailPath = Me.txtbStartrailThumbnailPath.Text
        _cfg.StartrailThumbnailMask = Me.txtbStartrailThumbnailMask.Text

        _cfg.StartraillapsePath = Me.txtbStartraillapsePath.Text
        _cfg.StartraillapseMask = Me.txtbStartraillapseMask.Text
        _cfg.StartraillapseThumbnailPath = Me.txtbStartraillapseThumbnailPath.Text
        _cfg.StartraillapseThumbnailMask = Me.txtbStartraillapseThumbnailMask.Text

        _cfg.RecapPath = Me.txtbRecapPath.Text
        _cfg.RecapImageMask = Me.txtbRecapImageMask.Text
        _cfg.RecapVideoMask = Me.txtbRecapVideoMask.Text
        _cfg.RecapMetadataMask = Me.txtbRecapMetadataMask.Text

        _cfg.MeteorPath = Me.txtbMeteorPath.Text
        _cfg.MeteorMask = Me.txtbMeteorMask.Text
        _cfg.MeteorThumbnailPath = Me.txtbMeteorThumbnailPath.Text
        _cfg.MeteorThumbnailMask = Me.txtbMeteorThumbnailMask.Text
        _cfg.MeteorMarkedMask = Me.txtbMeteorMarkedMask.Text
        _cfg.MeteorMarkedThumbnailMask = Me.txtbMeteorMarkedThumbnailMask.Text
        _cfg.MeteorMetadataMask = Me.txtbMeteorMetadataMask.Text

        _cfg.ShowEmptyFolders = Me.chkbShowEmptyFolders.Checked
        _cfg.DeleteEmptyFolders = Me.chkbDeleteEmptyFolders.Checked

        _cfg.ThumbnailWidth = Integer.Parse(Me.txtbThumbnailWidth.Text)
        _cfg.ThumbnailHeight = Integer.Parse(Me.txtbThumbnailHeight.Text)
        _cfg.ThumbnailCreate = Me.chkbThumbnailCreate.Checked

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub txtbDatetimeFormat_TextChanged(sender As Object, e As EventArgs) Handles txtbDatetimeFormat.TextChanged
        lblDateTimeFormatTest.Text = Date.Now.ToString(txtbDatetimeFormat.Text)
    End Sub

    Private Sub txtbDateFormat_TextChanged(sender As Object, e As EventArgs) Handles txtbDateFormat.TextChanged
        lblDateFormatTest.Text = Date.Now.ToString(txtbDateFormat.Text)
    End Sub

    Private Sub btnInsertDateTime_Click(sender As Object, e As EventArgs)
        If ActiveControl IsNot Nothing AndAlso TypeOf ActiveControl Is TextBox AndAlso ActiveControl.Name IsNot "txtbDatetimeFormat" AndAlso ActiveControl.Name IsNot "txtbBasePath" AndAlso Not ActiveControl.Text.Contains("[DT]") Then
            Dim txtBox = DirectCast(ActiveControl, TextBox)
            Dim insertText = "[DT]"
            Dim selectionStart = txtBox.SelectionStart
            txtBox.Text = txtBox.Text.Insert(selectionStart, insertText)
            txtBox.SelectionStart = selectionStart + insertText.Length
        End If
    End Sub

    Private Sub btnInsertDate_Click(sender As Object, e As EventArgs)
        If ActiveControl IsNot Nothing AndAlso TypeOf ActiveControl Is TextBox AndAlso ActiveControl.Name IsNot "txtbDatetimeFormat" AndAlso ActiveControl.Name IsNot "txtbBasePath" AndAlso Not ActiveControl.Text.Contains("[D]") Then
            Dim txtBox = DirectCast(ActiveControl, TextBox)
            Dim insertText = "[D]"
            Dim selectionStart = txtBox.SelectionStart
            txtBox.Text = txtBox.Text.Insert(selectionStart, insertText)
            txtBox.SelectionStart = selectionStart + insertText.Length
        End If
    End Sub

    Private Sub btnSelectBaseFolder_MouseClick(sender As Object, e As MouseEventArgs) Handles btnSelectBaseFolder.MouseClick
        If Me.FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            Me.txtbBasePath.Text = Me.FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub chkbThumbnailCreate_CheckedChanged(sender As Object, e As EventArgs) Handles chkbThumbnailCreate.CheckedChanged
        Me.txtbThumbnailWidth.Enabled = chkbThumbnailCreate.Checked
        Me.txtbThumbnailHeight.Enabled = chkbThumbnailCreate.Checked
    End Sub
End Class

Public Class NoFocusButton
    Inherits Button

    Public Sub New()
        Me.SetStyle(ControlStyles.Selectable, False)
        Me.TabStop = False
    End Sub
    Protected Overrides Sub [Select](directed As Boolean, forward As Boolean)

    End Sub

    Protected Overrides Sub OnGotFocus(e As EventArgs)
        MyBase.OnGotFocus(e)
        Me.Parent.Select() ' returns focus to container
    End Sub

    Protected Overrides Sub OnEnter(e As EventArgs)
        ' Block Enter-based focus
    End Sub

End Class