Imports System.Reflection

Partial Class Form1
    Inherits Form

    Private _cfg As SettingsSnapshot

    ' This constructor is used when you want to create the form without passing a SettingsSnapshot object.
    Public Sub New()
        InitializeComponent()
    End Sub

    ' This constructor is used when you want to pass a SettingsSnapshot object to the form.
    Public Sub New(cfg As SettingsSnapshot)
        Me.New()
        _cfg = cfg

    End Sub

    ' This method is called when the form is loaded. It initializes the form and sets up any necessary configurations.
    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click
        Application.Exit()
    End Sub

    ' This method is called when the "Setup" button is clicked. It opens a configuration dialog and saves the settings if the user clicks OK.
    Private Sub tsbSetup_Click(sender As Object, e As EventArgs) Handles tsbSetup.Click
        Dim ConfigDialog As New frmConfig(_cfg)

        If ConfigDialog.ShowDialog() = DialogResult.OK Then
            _cfg.CalculateDerivedSettings()
            _cfg.SaveConfig()
        End If
    End Sub

    ''' <summary>
    ''' Populates the header of the grid with labels for each column, including "Date", "Nightlapse", "Daylapse", "Keogram", "Keolapse", "Startrail", and "Starlapse". Each label is centered and docked to fill its respective cell in the TableLayoutPanel.
    ''' </summary>
    Private Sub PopulateGridHeader()

        With tlpHeader.Controls
            .Add(New Label With {.Text = "Date", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleCenter}, 0, 0)
            .Add(New Label With {.Text = "Nightlapse", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleCenter}, 1, 0)
            .Add(New Label With {.Text = "Daylapse", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleCenter}, 2, 0)
            .Add(New Label With {.Text = "Keogram", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleCenter}, 3, 0)
            .Add(New Label With {.Text = "Keolapse", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleCenter}, 4, 0)
            .Add(New Label With {.Text = "Startrail", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleCenter}, 5, 0)
            .Add(New Label With {.Text = "Starlapse", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleCenter}, 6, 0)
            .Add(New Label With {.Text = "Recap", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleCenter}, 7, 0)
            .Add(New Label With {.Text = "Meteors", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleCenter}, 8, 0)
        End With

    End Sub

    ''' <summary>
    ''' Populates the TreeView control with nodes representing the Allsky tree structure. It creates a root node for "Allsky" and adds child nodes for each year, month, and day, setting the Tag property of each node to the corresponding data object. The TreeView is updated in a batch operation to improve performance, and all nodes are expanded after population.
    ''' </summary>
    Private Sub PopulateTreeView()
        TreeView1.BeginUpdate()
        TreeView1.Nodes.Clear()

        Dim rootNode As New TreeNode("Allsky")
        rootNode.Tag = allSkyTree.Root.Data
        TreeView1.Nodes.Add(rootNode)

        For Each yearNode In allSkyTree.Root.Children
            Dim tvYear = rootNode.Nodes.Add(yearNode.Data.Name)
            tvYear.Tag = yearNode.Data

            For Each monthNode In yearNode.Children
                Dim tvMonth = tvYear.Nodes.Add(monthNode.Data.Name)
                tvMonth.Tag = monthNode.Data

                For Each dayNode In monthNode.Children
                    Dim tvDay = tvMonth.Nodes.Add(dayNode.Data.Name)
                    tvDay.Tag = dayNode.Data

                Next
            Next
        Next
        TreeView1.EndUpdate()
        TreeView1.ExpandAll()
        TreeView1.SelectedNode = rootNode

    End Sub

    ''' <summary>
    ''' Clears all rows from the specified TableLayoutPanel, removing all controls and resetting the row count to zero. It suspends layout updates during the operation to improve performance and resumes layout updates afterward. The method also clears any existing row styles to ensure a clean state for future population of the grid.
    ''' </summary>
    ''' <param name="tlp"></param>
    Public Sub ClearAllRows(tlp As TableLayoutPanel)
        ' Remove all controls
        tlp.SuspendLayout()

        For i As Integer = tlp.Controls.Count - 1 To 0 Step -1
            Dim c = tlp.Controls(i)
            tlp.Controls.RemoveAt(i)
            c.Dispose()
        Next

        ' Clear row styles
        tlp.RowStyles.Clear()

        ' Reset row count
        tlp.RowCount = 0

        tlp.ResumeLayout()
        tlp.Invalidate()
    End Sub

    ''' <summary>
    ''' Populates the grid with data from the Allsky tree structure. It iterates through each year, month, and day node, retrieves the relevant video/image data for each day, and adds a row to the grid with the corresponding information. The progress bar is updated as rows are added to provide feedback on the population process.
    ''' </summary>
    Private Sub PopulateGrid()
        'TableLayoutPanel1.Enabled = False
        'Panel4.Enabled = False
        ClearAllRows(TableLayoutPanel1)

        TableLayoutPanel1.SuspendLayout()

        ToolStripProgressBar1.Value = 0
        For Each yearNode In allSkyTree.Root.Children
            For Each monthNode In yearNode.Children
                For Each daylyNode In monthNode.Children
                    Dim dateString = $"{yearNode.Data.Name}-{monthNode.Data.Name}-{daylyNode.Data.Name}"

                    ' Retrieve the video/image data for each type (NightTimelapse, DayTimelapse, Keogram, Keolapse, Startrail, Starlapse) and determine if the thumbnail exists or not. If it exists, use the thumbnail path; otherwise, indicate "Yes" for available but no thumbnail, or "No" if not available.

                    ' Night Timelapse
                    Dim nightTimelapse As String
                    Dim nightNode = daylyNode.GetChild(Of VideoImageNode)(Function(n) n.Type = VideoImageType.NightTimelapse)
                    Dim nightTimelapseFull As String = Nothing
                    If nightNode IsNot Nothing Then
                        Dim data = DirectCast(nightNode.Data, VideoImageNode)
                        If data.VideoImageThumbnailExists Then
                            nightTimelapse = data.VideoImageThumbnailPath
                        Else
                            nightTimelapse = "Yes"
                        End If
                        nightTimelapseFull = data.VideoImagePath
                    Else
                        nightTimelapse = "No"
                    End If

                    ' Day Timelapse
                    Dim dayTimelapse As String
                    Dim dayNode = daylyNode.GetChild(Of VideoImageNode)(Function(n) n.Type = VideoImageType.DayTimelapse)
                    Dim dayTimelapseFull As String = Nothing
                    If dayNode IsNot Nothing Then
                        Dim data = DirectCast(dayNode.Data, VideoImageNode)
                        If data.VideoImageThumbnailExists Then
                            dayTimelapse = data.VideoImageThumbnailPath
                        Else
                            dayTimelapse = "Yes"
                        End If
                        dayTimelapseFull = data.VideoImagePath
                    Else
                        dayTimelapse = "No"
                    End If

                    ' Keogram
                    Dim keogram As String
                    Dim keogramNode = daylyNode.GetChild(Of VideoImageNode)(Function(n) n.Type = VideoImageType.Keogram)
                    Dim keogramFull As String = Nothing
                    If keogramNode IsNot Nothing Then
                        Dim data = DirectCast(keogramNode.Data, VideoImageNode)
                        If data.VideoImageThumbnailExists Then
                            keogram = data.VideoImageThumbnailPath
                        Else
                            keogram = "Yes"
                        End If
                        keogramFull = data.VideoImagePath
                    Else
                        keogram = "No"
                    End If

                    ' Keolapse
                    Dim keolapse As String
                    Dim keolapseNode = daylyNode.GetChild(Of VideoImageNode)(Function(n) n.Type = VideoImageType.Keolapse)
                    Dim keolapseFull As String = Nothing
                    If keolapseNode IsNot Nothing Then
                        Dim data = DirectCast(keolapseNode.Data, VideoImageNode)
                        If data.VideoImageThumbnailExists Then
                            keolapse = data.VideoImageThumbnailPath
                        Else
                            keolapse = "Yes"
                        End If
                        keolapseFull = data.VideoImagePath
                    Else
                        keolapse = "No"
                    End If

                    ' Startrail
                    Dim startrail As String
                    Dim startrailNode = daylyNode.GetChild(Of VideoImageNode)(Function(n) n.Type = VideoImageType.Startrail)
                    Dim startrailFull As String = Nothing
                    If startrailNode IsNot Nothing Then
                        Dim data = DirectCast(startrailNode.Data, VideoImageNode)
                        If data.VideoImageThumbnailExists Then
                            startrail = data.VideoImageThumbnailPath
                        Else
                            startrail = "Yes"
                        End If
                        startrailFull = data.VideoImagePath
                    Else
                        startrail = "No"
                    End If

                    ' Starlapse
                    Dim starlapse As String
                    Dim starlapseNode = daylyNode.GetChild(Of VideoImageNode)(Function(n) n.Type = VideoImageType.StartrailTimelapse)
                    Dim starlapseFull As String = Nothing
                    If starlapseNode IsNot Nothing Then
                        Dim data = DirectCast(starlapseNode.Data, VideoImageNode)
                        If data.VideoImageThumbnailExists Then
                            starlapse = data.VideoImageThumbnailPath
                        Else
                            starlapse = "Yes"
                        End If
                        starlapseFull = data.VideoImagePath
                    Else
                        starlapse = "No"
                    End If

                    ' Recap
                    Dim recapNode = daylyNode.GetChild(Of RecapNode)(Function(n) n.Type = VideoImageType.Recap)
                    Dim recap As String = Nothing
                    Dim recapImageFull As String = Nothing
                    Dim recapVideoFull As String = Nothing
                    Dim recapMetadataFull As String = Nothing
                    If recapNode IsNot Nothing Then
                        Dim data = DirectCast(recapNode.Data, RecapNode)
                        If data.RecapImageThumbnailExists Then
                            recap = data.RecapImagePath
                        Else
                            recap = "Yes"
                        End If
                        recapImageFull = data.RecapImagePath
                        recapVideoFull = data.RecapVideoPath
                        recapMetadataFull = data.RecapMetadataPath
                    Else
                        recap = "No"
                    End If

                    AddGridRow(dateString, nightNode, dayNode, keogramNode, keolapseNode, startrailNode, starlapseNode, recapNode, daylyNode)

                    UpdateProgress(ToolStripProgressBar1.Value + 1)
                Next
            Next
        Next

        'Panel4.Enabled = True
        'TableLayoutPanel1.Enabled = True
        TableLayoutPanel1.ResumeLayout()
        tstlblDaysCount.Text = $"Days: {TableLayoutPanel1.RowCount - 1}"

        UpdateScoresCount()
    End Sub

    ''' <summary>
    ''' Handles the double-click event on an icon in the grid. It retrieves the file path associated with the clicked icon and opens the corresponding viewer (ImageViewer for .jpg files and frmVideoPlayer for .mp4 files) to display the content.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IconDoubleClick(sender As Object, e As EventArgs)
        Dim tag = TryCast(sender.tag, PicTag)
        If tag Is Nothing Then Exit Sub

        Dim pathForViewer As String = tag.Path

        If pathForViewer IsNot Nothing AndAlso IO.File.Exists(pathForViewer) AndAlso pathForViewer.EndsWith(".jpg") Then
            Dim viewer As New ImageViewer(pathForViewer)
            viewer.ShowDialog()
        End If

        If pathForViewer IsNot Nothing AndAlso IO.File.Exists(pathForViewer) AndAlso pathForViewer.EndsWith(".mp4") Then
            Dim viewer As New frmVideoPlayer(pathForViewer)
            viewer.ShowDialog()
        End If

    End Sub

    ''' <summary>
    '''     Handles the double-click event on an icon in the grid while holding the Control key. It retrieves the file path associated with the clicked icon, modifies it to point to the corresponding video file (by replacing ".jpg" with ".mp4"), and opens the frmVideoPlayer to play the video if it exists.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IconCtrlDoubleClick(sender As Object, e As EventArgs)
        Dim tag = TryCast(sender.tag, PicTag)
        If tag Is Nothing Then Exit Sub

        Dim pathForViewer As String = tag.Path

        If pathForViewer IsNot Nothing Then
            If Control.ModifierKeys = Keys.Control Then
                pathForViewer = pathForViewer.Replace(".jpg", ".mp4")
                If IO.File.Exists(pathForViewer) Then
                    Dim viewer As New frmVideoPlayer(pathForViewer)
                    viewer.ShowDialog()
                End If
            Else
                If IO.File.Exists(pathForViewer) AndAlso pathForViewer.EndsWith(".jpg") Then
                    Dim viewer As New ImageViewer(pathForViewer)
                    viewer.ShowDialog()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the double-click event on a metadata icon in the grid. It retrieves the file path associated with the clicked icon and opens the frmMetadataViewer to display the metadata content if the file exists.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MetadataIconDoubleClick(sender As Object, e As EventArgs)
        Dim pathForViewer As String = sender.tag.ToString
        If pathForViewer IsNot Nothing AndAlso IO.File.Exists(pathForViewer) Then
            Dim viewer As New frmMetadata(pathForViewer)
            viewer.ShowDialog()
        End If
    End Sub

    Private previewForm As Form = Nothing

    ''' <summary>
    ''' Displays a floating preview of the specified image with a tooltip showing the provided timestamp. It creates a new form to host the image and positions it near the cursor. If a preview is already being displayed, it closes the existing preview before showing the new one.
    ''' </summary>
    ''' <param name="img"></param>
    ''' <param name="timestamp"></param>
    ''' </summary>
    ''' <param name="img"></param>
    ''' <param name="timestamp"></param>
    ''' <returns></returns>
    Private Function CreatePreviewForm(img As Image, timestamp As String) As Form
        Dim zFactor As Double = 0.8
        Dim f As New Form With {
            .FormBorderStyle = FormBorderStyle.FixedToolWindow,
            .StartPosition = FormStartPosition.Manual,
            .BackColor = Color.Black,
            .Opacity = 0.95,
            .ShowInTaskbar = False,
            .TopMost = True,
            .ControlBox = False,
            .Size = New Size(img.Width * zFactor, img.Height * zFactor),
            .Location = Cursor.Position + New Point(10, 10),
            .Text = timestamp.Insert(2, ":").Insert(5, ":")
        }

        Dim pb As New PictureBox With {
            .Image = img,
            .SizeMode = PictureBoxSizeMode.Zoom,
            .Dock = DockStyle.Fill
        }

        f.Controls.Add(pb)
        Return f
    End Function

    ''' <summary>
    ''' Handles the mouse hover event on a meteor icon in the grid. It retrieves the associated MeteorNode from the label's Tag property, loads the appropriate meteor image (marked or unmarked based on whether the Control key is pressed), and displays a floating preview form with the image and timestamp. If a preview is already being displayed, it closes the existing preview before showing the new one.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MeteorIcon_MouseEnter(sender As Object, e As EventArgs)
        Dim tag = TryCast(sender.Tag, MeteorTag)
        If tag Is Nothing Then Exit Sub
        Dim itemId As String = tag.ItemId

        Dim lbl = DirectCast(sender, Label)
        Dim meta = DirectCast(tag.MeteorNode, MeteorNode)

        ' Close any existing preview
        ClosePreview()

        Dim img As Image
        If Control.ModifierKeys = Keys.Control Then
            'img = Image.FromFile(meta.MeteorMarkedImageThumbnailPath)
            Using fs As New System.IO.FileStream(meta.MeteorMarkedImageThumbnailPath, IO.FileMode.Open, IO.FileAccess.Read)
                img = Image.FromStream(fs)
                img = New Bitmap(img)
            End Using
        Else
            'img = Image.FromFile(meta.MeteorImageThumbnailPath))Then
            Using fs As New System.IO.FileStream(meta.MeteorImageThumbnailPath, IO.FileMode.Open, IO.FileAccess.Read)
                img = Image.FromStream(fs)
                img = New Bitmap(img)
            End Using
        End If

        previewForm = CreatePreviewForm(img, meta.MeteorTime)
        previewForm.Show()
    End Sub


    ''' <summary>
    ''' Closes the currently displayed preview form, if any. This method checks if the previewForm variable is not Nothing, and if so, it closes the form and sets the variable to Nothing to indicate that no preview is currently being displayed.
    ''' </summary>
    Private Sub ClosePreview()
        If previewForm IsNot Nothing Then
            previewForm.Close()
            previewForm = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Handles the mouse hover event on a meteor icon in the grid. It retrieves the associated MeteorNode from the label's Tag property, loads the meteor image, and displays a floating preview form with the image and timestamp. If a preview is already being displayed, it closes the existing preview before showing the new one.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MeteorIcon_MouseLeave(sender As Object, e As EventArgs)
        ClosePreview()
    End Sub


    ''' <summary>
    ''' Handles the double-click event on a meteor icon in the grid. It retrieves the associated MeteorNode from the label's Tag property and opens the appropriate viewer based on whether the Control or Shift key is pressed. If Control is pressed, it opens an ImageViewer for the marked image; if Shift is pressed, it opens a frmMetadata viewer for the metadata; otherwise, it opens an ImageViewer for the original meteor image.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MeteorIcon_DoubleClick(sender As Object, e As EventArgs)
        Dim tag = TryCast(sender.Tag, MeteorTag)
        If tag Is Nothing Then Exit Sub
        Dim itemId As String = tag.ItemId

        Dim meta = DirectCast(tag.MeteorNode, MeteorNode)

        If Control.ModifierKeys = Keys.Control Then
            Dim viewer As New ImageViewer(meta.MeteorMarkedImagePath, meta.MeteorMetadataPath)
            viewer.ShowDialog()
        ElseIf Control.ModifierKeys = Keys.Shift Then
            Dim viewer As New frmMetadata(meta.MeteorMetadataPath)
            viewer.ShowDialog()
        Else
            Dim viewer As New ImageViewer(meta.MeteorImagePath, meta.MeteorMetadataPath)
            viewer.ShowDialog()
        End If
    End Sub

    ''' <summary>
    ''' Creates a circular label to represent a meteor icon in the grid. The label displays the index of the meteor, has a specified size and appearance, and is associated with the provided MeteorNode metadata. Event handlers for mouse hover and double-click events are attached to the label to allow for interaction with the meteor data.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <param name="meta"></param>
    ''' <returns></returns>
    Private Function CreateMeteorIcon(index As Integer, meta As MeteorNode, datestr As String) As Label
        Dim lbl As New Label With {
        .Text = index.ToString("00"),
        .AutoSize = True,
        .BorderStyle = BorderStyle.FixedSingle,
        .TextAlign = ContentAlignment.MiddleCenter,
        .BackColor = Color.LightCyan,
        .ForeColor = Color.Black,
        .Font = New Font("Segoe UI", 8, FontStyle.Bold),
        .Margin = New Padding(2),
        .Tag = New MeteorTag With {
            .MeteorNode = meta,
            .ItemId = $"{datestr}-{meta.MeteorTime}-{meta.Type}"
            }
        }
        Dim score = ScoreManager.GetScore(lbl.Tag.ItemId)
        Dim scoreStr As String = New String("·"c, score)
        lbl.Text &= " " & scoreStr

        AddHandler lbl.MouseClick, AddressOf MeteorIcon_MouseClick
        AddHandler lbl.MouseEnter, AddressOf MeteorIcon_MouseEnter
        AddHandler lbl.MouseLeave, AddressOf MeteorIcon_MouseLeave
        AddHandler lbl.DoubleClick, AddressOf MeteorIcon_DoubleClick

        Return lbl
    End Function

    ''' <summary>
    ''' Handles the mouse click event on a meteor icon in the grid. It retrieves the associated MeteorTag from the label's Tag property and updates the score for the corresponding meteor based on the mouse button clicked and any modifier keys pressed (Shift or Control). The score is updated in the ScoreManager, and the label's text is refreshed to reflect the new score.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub MeteorIcon_MouseClick(sender As Object, e As MouseEventArgs)
        Dim tag = TryCast(sender.Tag, MeteorTag)
        If tag Is Nothing Then Exit Sub
        Dim itemId As String = tag.ItemId
        If String.IsNullOrEmpty(itemId) Then Exit Sub
        ' Get current score
        Dim currentScore As ScoreType = ScoreManager.GetScore(itemId)
        Dim newScore As ScoreType = currentScore

        If e.Button = MouseButtons.Right Then
            If Control.ModifierKeys = Keys.Shift Then
                newScore = currentScore + 1
            End If
            If Control.ModifierKeys = Keys.Control Then
                newScore = currentScore - 1
            End If
            ' Update score in ScoreManager
            newScore = Math.Max(0, newScore) ' Ensure score doesn't go below 0
            newScore = Math.Min(ScoreType.Cool, newScore) ' Ensure score doesn't go above 3

            If newScore <> currentScore Then
                ScoreManager.SetScore(itemId, newScore)
                ' Refresh the PictureBox to trigger repaint and show updated score
                Dim txt As String = DirectCast(sender, Label).Text.Substring(0, 2)
                txt &= " " & New String("·"c, newScore)
                DirectCast(sender, Label).Text = txt
            End If
        End If
    End Sub

    ''' <summary>
    ''' Adds a new row to the TableLayoutPanel with the specified date and associated video/image nodes for each column. It creates PictureBox controls for each type of video/image (Nightlapse, Daylapse, Keogram, Keolapse, Startrail, Starlapse) and sets their images based on the provided nodes. If a node is not available, it displays an "Empty" image. The method also adds a label for the date and a panel for meteors associated with the day.    
    ''' </summary>
    ''' <param name="dateString"></param>
    ''' <param name="nightTimeLapseNode"></param>
    ''' <param name="dayTimelapseNode"></param>
    ''' <param name="keogramNode"></param>
    ''' <param name="keolapseNode"></param>
    ''' <param name="startrailNode"></param>
    ''' <param name="starlapseNode"></param>
    ''' <param name="recapNode"></param>
    ''' <param name="day">day node containing meteor children</param> 
    Private Sub AddGridRow(dateString As String, nightTimeLapseNode As vTreeNode, dayTimelapseNode As vTreeNode, keogramNode As vTreeNode, keolapseNode As vTreeNode, startrailNode As vTreeNode, starlapseNode As vTreeNode, recapNode As vTreeNode, day As vTreeNode)
        Dim rowIndex = TableLayoutPanel1.RowCount
        TableLayoutPanel1.RowCount += 1
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.AutoSize))
        Dim pic As PictureBox
        Dim metaPic As PictureBox
        With TableLayoutPanel1.Controls
            .Add(New Label With {.Text = DateTime.Parse(dateString).ToString("yyyy-MMM-d"), .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleCenter,
                 .Tag = New RowMeta With {
                 .Year = dateString.Split("-")(0),
                 .Month = dateString.Split("-")(1),
                 .Day = dateString.Split("-")(2)
                 }
            }, 0, rowIndex)

            ' Add PictureBoxes for each column (Nightlapse, Daylapse, Keogram, Keolapse, Startrail, Starlapse) with appropriate images and event handlers

            ' Nightlapse
            pic = New PictureBox With {.Dock = DockStyle.Fill, .SizeMode = PictureBoxSizeMode.CenterImage}
            AddHandler pic.Paint, AddressOf PictureBox_PaintScoreOverlay
            If nightTimeLapseNode IsNot Nothing Then
                Dim data = DirectCast(nightTimeLapseNode.Data, VideoImageNode)
                pic.SizeMode = PictureBoxSizeMode.Zoom
                If data.VideoImageThumbnailExists Then
                    'pic.Image = Image.FromFile(data.VideoImageThumbnailPath)
                    Using fs As New System.IO.FileStream(data.VideoImageThumbnailPath, IO.FileMode.Open, IO.FileAccess.Read)
                        Dim img As Image = Image.FromStream(fs)
                        pic.Image = New Bitmap(img)
                    End Using
                Else
                    pic.Image = ImageList1.Images("Video")
                End If
                pic.Tag = New PicTag With {
                    .Path = data.VideoImagePath,
                    .ItemId = $"{dateString}-{data.Type}"
                    }
            Else
                pic.Image = ImageList1.Images("Empty")
            End If
            AddHandler pic.DoubleClick, AddressOf IconDoubleClick
            AddHandler pic.MouseDown, AddressOf PictureBox_ClickScore
            .Add(pic, 1, rowIndex)

            ' Daylapse
            pic = New PictureBox With {.Dock = DockStyle.Fill, .SizeMode = PictureBoxSizeMode.CenterImage}
            AddHandler pic.Paint, AddressOf PictureBox_PaintScoreOverlay
            If dayTimelapseNode IsNot Nothing Then
                Dim data = DirectCast(dayTimelapseNode.Data, VideoImageNode)
                pic.SizeMode = PictureBoxSizeMode.Zoom
                If data.VideoImageThumbnailExists Then
                    'pic.Image = Image.FromFile(data.VideoImageThumbnailPath)
                    Using fs As New System.IO.FileStream(data.VideoImageThumbnailPath, IO.FileMode.Open, IO.FileAccess.Read)
                        Dim img As Image = Image.FromStream(fs)
                        pic.Image = New Bitmap(img)
                    End Using
                Else
                    pic.Image = ImageList1.Images("Video")
                End If
                pic.Tag = New PicTag With {
                    .Path = data.VideoImagePath,
                    .ItemId = $"{dateString}-{data.Type}"
                    }
            Else
                pic.Image = ImageList1.Images("Empty")
            End If
            AddHandler pic.DoubleClick, AddressOf IconDoubleClick
            AddHandler pic.MouseDown, AddressOf PictureBox_ClickScore
            .Add(pic, 2, rowIndex)

            ' Keogram
            pic = New PictureBox With {.Dock = DockStyle.Fill, .SizeMode = PictureBoxSizeMode.CenterImage}
            AddHandler pic.Paint, AddressOf PictureBox_PaintScoreOverlay
            If keogramNode IsNot Nothing Then
                Dim data = DirectCast(keogramNode.Data, VideoImageNode)
                pic.SizeMode = PictureBoxSizeMode.Zoom
                If data.VideoImageThumbnailExists Then
                    'pic.Image = Image.FromFile(data.VideoImageThumbnailPath)
                    Using fs As New System.IO.FileStream(data.VideoImageThumbnailPath, IO.FileMode.Open, IO.FileAccess.Read)
                        Dim img As Image = Image.FromStream(fs)
                        pic.Image = New Bitmap(img)
                    End Using
                Else
                    pic.Image = ImageList1.Images("Image")
                End If
                pic.Tag = New PicTag With {
                    .Path = data.VideoImagePath,
                    .ItemId = $"{dateString}-{data.Type}"
                    }
            Else
                pic.Image = ImageList1.Images("Empty")
            End If
            AddHandler pic.DoubleClick, AddressOf IconDoubleClick
            AddHandler pic.MouseDown, AddressOf PictureBox_ClickScore
            .Add(pic, 3, rowIndex)

            ' Keolapse
            pic = New PictureBox With {.Dock = DockStyle.Fill, .SizeMode = PictureBoxSizeMode.CenterImage}
            AddHandler pic.Paint, AddressOf PictureBox_PaintScoreOverlay
            If keolapseNode IsNot Nothing Then
                Dim data = DirectCast(keolapseNode.Data, VideoImageNode)
                pic.SizeMode = PictureBoxSizeMode.Zoom
                If data.VideoImageThumbnailExists Then
                    'pic.Image = Image.FromFile(data.VideoImageThumbnailPath)
                    Using fs As New System.IO.FileStream(data.VideoImageThumbnailPath, IO.FileMode.Open, IO.FileAccess.Read)
                        Dim img As Image = Image.FromStream(fs)
                        pic.Image = New Bitmap(img)
                    End Using
                Else
                    pic.Image = ImageList1.Images("Video")
                End If
                pic.Tag = New PicTag With {
                    .Path = data.VideoImagePath,
                    .ItemId = $"{dateString}-{data.Type}"
                    }
            Else
                pic.Image = ImageList1.Images("Empty")
            End If
            AddHandler pic.DoubleClick, AddressOf IconDoubleClick
            AddHandler pic.MouseDown, AddressOf PictureBox_ClickScore
            .Add(pic, 4, rowIndex)

            ' Startrail
            pic = New PictureBox With {.Dock = DockStyle.Fill, .SizeMode = PictureBoxSizeMode.CenterImage}
            AddHandler pic.Paint, AddressOf PictureBox_PaintScoreOverlay
            If startrailNode IsNot Nothing Then
                Dim data = DirectCast(startrailNode.Data, VideoImageNode)
                pic.SizeMode = PictureBoxSizeMode.Zoom
                If data.VideoImageThumbnailExists Then
                    'pic.Image = Image.FromFile(data.VideoImageThumbnailPath)
                    Using fs As New System.IO.FileStream(data.VideoImageThumbnailPath, IO.FileMode.Open, IO.FileAccess.Read)
                        Dim img As Image = Image.FromStream(fs)
                        pic.Image = New Bitmap(img)
                    End Using
                Else
                    pic.Image = ImageList1.Images("Image")
                End If
                pic.Tag = New PicTag With {
                    .Path = data.VideoImagePath,
                    .ItemId = $"{dateString}-{data.Type}"
                    }
            Else
                pic.Image = ImageList1.Images("Empty")
            End If
            AddHandler pic.DoubleClick, AddressOf IconDoubleClick
            AddHandler pic.MouseDown, AddressOf PictureBox_ClickScore
            .Add(pic, 5, rowIndex)

            ' Starlapse
            pic = New PictureBox With {.Dock = DockStyle.Fill, .SizeMode = PictureBoxSizeMode.CenterImage}
            AddHandler pic.Paint, AddressOf PictureBox_PaintScoreOverlay
            If starlapseNode IsNot Nothing Then
                Dim data = DirectCast(starlapseNode.Data, VideoImageNode)
                pic.SizeMode = PictureBoxSizeMode.Zoom
                If data.VideoImageThumbnailExists Then
                    'pic.Image = Image.FromFile(data.VideoImageThumbnailPath)
                    Using fs As New System.IO.FileStream(data.VideoImageThumbnailPath, IO.FileMode.Open, IO.FileAccess.Read)
                        Dim img As Image = Image.FromStream(fs)
                        pic.Image = New Bitmap(img)
                    End Using
                Else
                    pic.Image = ImageList1.Images("Video")
                End If
                pic.Tag = New PicTag With {
                    .Path = data.VideoImagePath,
                    .ItemId = $"{dateString}-{data.Type}"
                    }
            Else
                pic.Image = ImageList1.Images("Empty")
            End If
            AddHandler pic.DoubleClick, AddressOf IconDoubleClick
            AddHandler pic.MouseDown, AddressOf PictureBox_ClickScore
            .Add(pic, 6, rowIndex)

            ' Recap
            Dim tblPan As New TableLayoutPanel With {.Dock = DockStyle.Fill, .AutoSize = True, .RowCount = 1, .ColumnCount = 2}
            tblPan.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))
            tblPan.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
            tblPan.ColumnStyles(1).SizeType = SizeType.AutoSize

            pic = New PictureBox With {.Dock = DockStyle.Fill, .SizeMode = PictureBoxSizeMode.CenterImage}
            AddHandler pic.Paint, AddressOf PictureBox_PaintScoreOverlay
            If recapNode IsNot Nothing Then
                Dim data = DirectCast(recapNode.Data, RecapNode)
                If data.RecapImageThumbnailExists Then
                    pic.SizeMode = PictureBoxSizeMode.Zoom
                    'pic.Image = Image.FromFile(data.RecapImageThumbnailPath)
                    Using fs As New System.IO.FileStream(data.RecapImageThumbnailPath, IO.FileMode.Open, IO.FileAccess.Read)
                        Dim img As Image = Image.FromStream(fs)
                        pic.Image = New Bitmap(img)
                    End Using
                Else
                    pic.Image = ImageList1.Images("Image")
                End If
                pic.Tag = New PicTag With {
                    .Path = data.RecapImagePath,
                    .ItemId = $"{dateString}-{data.Type}"
                    }
                Dim tip As New ToolTip
                tip.SetToolTip(pic, "Double-Click to View image" & vbCrLf & "Ctrl+Double Click to Play video")
                AddHandler pic.DoubleClick, AddressOf IconCtrlDoubleClick
                AddHandler pic.MouseDown, AddressOf PictureBox_ClickScore
                tblPan.Controls.Add(pic, 0, 0)

                'icon for recap metadata
                If data.RecapMetadataPath IsNot Nothing AndAlso IO.File.Exists(data.RecapMetadataPath) Then
                    metaPic = New PictureBox With {.SizeMode = PictureBoxSizeMode.CenterImage, .Anchor = AnchorStyles.None}
                    metaPic.Image = ImageList1.Images("Doc")
                    metaPic.Size = ImageList1.Images("Doc").Size
                    metaPic.Anchor = AnchorStyles.None
                    metaPic.Tag = data.RecapMetadataPath
                    AddHandler metaPic.DoubleClick, AddressOf MetadataIconDoubleClick
                    tblPan.Controls.Add(metaPic, 1, 0)
                End If
            Else
                pic.Image = ImageList1.Images("Empty")
            End If
            .Add(tblPan, 7, rowIndex)

            ' Meteors
            Dim panel As New FlowLayoutPanel With {
                .FlowDirection = FlowDirection.LeftToRight,
                .WrapContents = True,
                .AutoSize = True,
                .AutoSizeMode = AutoSizeMode.GrowAndShrink,
                .Margin = New Padding(0),
                .Padding = New Padding(0)
            }
            Dim idx = 0
            For Each node In day.Children.Where(Function(n) TypeOf n.Data Is MeteorNode).ToList()
                idx += 1
                Dim mn As MeteorNode = DirectCast(node.Data, MeteorNode)
                Dim lbl = CreateMeteorIcon(idx, mn, dateString)
                panel.Controls.Add(lbl)
            Next

            .Add(panel, 8, rowIndex)

        End With
    End Sub

    ''' <summary>
    ''' Represents metadata for a row in the TableLayoutPanel, including the year, month, and day associated with that row. This class is used to store and retrieve date information for filtering purposes when interacting with the grid.
    ''' </summary>
    Public Class RowMeta
        Public Property Year As String
        Public Property Month As String
        Public Property Day As String
    End Class

    ''' <summary>
    ''' Updates the progress bar value in a thread-safe manner. If the method is called from a different thread than the UI thread, it uses Invoke to marshal the call to the UI thread. Otherwise, it directly updates the progress bar value.
    ''' </summary>
    ''' <param name="value"></param>
    Private Sub UpdateProgress(value As String)
        If InvokeRequired Then
            Invoke(New Action(Of Integer)(AddressOf UpdateProgress), value)
        Else
            ToolStripProgressBar1.Value = value
        End If
    End Sub

    ''' <summary>
    ''' Handles the Shown event of the form. It populates the grid header, builds the tree view, and populates the grid with data from the Allsky tree structure. The method updates the status label and progress bar to provide feedback on the population process.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        PopulateGridHeader()
        ToolStripStatusLabel1.Text = "Building Tree"
        Me.Refresh()
        'Await Task.Run(Sub() PopulateTreeView())
        PopulateTreeView()
        ToolStripStatusLabel1.Text = "Building Grid"
        Me.Refresh()
        ToolStripProgressBar1.Maximum = allSkyTree.Traverse().Count(Function(n) TypeOf n.Data Is DayNode)
        'Await Task.Run(Sub() PopulateGrid())
        PopulateGrid()
        ToolStripStatusLabel1.Text = ""
        ToolStripProgressBar1.Visible = False
    End Sub

    ''' <summary>
    ''' Sets the visibility of all controls in a specified row of the TableLayoutPanel. It iterates through each column in the specified row and sets the Visible property of each control to the specified value (true or false).
    ''' </summary>
    ''' <param name="rowIndex"></param>
    ''' <param name="visible"></param>
    Private Sub SetRowVisible(rowIndex As Integer, visible As Boolean)
        For col = 0 To TableLayoutPanel1.ColumnCount - 1
            Dim c = TableLayoutPanel1.GetControlFromPosition(col, rowIndex)
            If c IsNot Nothing Then c.Visible = visible
        Next
    End Sub

    ''' <summary>
    ''' Sets all rows in the TableLayoutPanel to be visible. It iterates through each row and calls the SetRowVisible method to make each row visible.
    ''' </summary>
    Private Sub ShowAllRows()
        TableLayoutPanel1.SuspendLayout()

        For row = 0 To TableLayoutPanel1.RowCount - 1
            SetRowVisible(row, True)
        Next
        tstlblDaysCount.Text = $"Days: {TableLayoutPanel1.RowCount - 1}"

        TableLayoutPanel1.ResumeLayout()
    End Sub

    ''' <summary>
    ''' Filters the grid by year, showing only the rows that match the specified year. It iterates through each row in the TableLayoutPanel and checks the metadata associated with each row to determine if it should be visible.
    ''' </summary>
    ''' <param name="year"></param>
    Private Sub FilterGridByYear(year As Integer)
        Dim cnt As Integer = 0
        TableLayoutPanel1.SuspendLayout()
        For row = 0 To TableLayoutPanel1.RowCount - 1
            Dim c = TableLayoutPanel1.GetControlFromPosition(0, row)
            If c Is Nothing Then Continue For
            Dim meta = TryCast(c.Tag, RowMeta)

            If meta IsNot Nothing Then
                Dim visible = (meta.Year = year)
                SetRowVisible(row, visible)
                If visible Then cnt += 1
            End If
        Next
        TableLayoutPanel1.ResumeLayout()
        tstlblDaysCount.Text = $"Days: {cnt}"
    End Sub

    ''' <summary>
    ''' Filters the grid by month, showing only the rows that match the specified year and month. It iterates through each row in the TableLayoutPanel and checks the metadata associated with each row to determine if it should be visible.
    ''' </summary>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    Private Sub FilterGridByMonth(year As Integer, month As Integer)
        Dim cnt As Integer = 0
        TableLayoutPanel1.SuspendLayout()
        For row = 0 To TableLayoutPanel1.RowCount - 1
            Dim c = TableLayoutPanel1.GetControlFromPosition(0, row)
            If c Is Nothing Then Continue For
            Dim meta = TryCast(c.Tag, RowMeta)

            If meta IsNot Nothing Then
                Dim visible = (meta.Year = year AndAlso meta.Month = month)
                SetRowVisible(row, visible)
                If visible Then cnt += 1
            End If
        Next
        TableLayoutPanel1.ResumeLayout()
        tstlblDaysCount.Text = $"Days: {cnt}"
    End Sub

    ''' <summary>
    ''' Filters the grid by day, showing only the rows that match the specified year, month, and day. It iterates through each row in the TableLayoutPanel and checks the metadata associated with each row to determine if it should be visible.
    ''' </summary>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <param name="day"></param>
    Private Sub FilterGridByDay(year As Integer, month As Integer, day As Integer)
        Dim cnt As Integer = 0
        TableLayoutPanel1.SuspendLayout()
        For row = 0 To TableLayoutPanel1.RowCount - 1
            Dim c = TableLayoutPanel1.GetControlFromPosition(0, row)
            If c Is Nothing Then Continue For
            Dim meta = TryCast(c.Tag, RowMeta)

            If meta IsNot Nothing Then
                Dim visible = (meta.Year = year AndAlso meta.Month = month AndAlso meta.Day = day)
                SetRowVisible(row, visible)
                If visible Then cnt += 1
            End If
        Next
        TableLayoutPanel1.ResumeLayout()
        tstlblDaysCount.Text = $"Days: {cnt}"
    End Sub

    ''' <summary>
    ''' Handles the AfterSelect event of the TreeView control. It determines which node was selected (YearNode, MonthNode, or DayNode) and updates the active filter label accordingly. It also calls the appropriate filtering method to show only the relevant rows in the grid based on the selected node.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        Dim node = e.Node   ' your YearNode / MonthNode / DayNode

        If TypeOf node.Tag Is RootNode Then
            Me.tslblActiveFilter.Text = "Active Filter: All"
            ShowAllRows()
            Exit Sub
        End If

        If TypeOf node.Tag Is YearNode Then
            Me.tslblActiveFilter.Text = $"Active Filter: Year {node.Tag.Year}"
            FilterGridByYear(node.Tag.Year)

        ElseIf TypeOf node.Tag Is MonthNode Then
            Me.tslblActiveFilter.Text = $"Active Filter: Year {node.Parent.Tag.Year}, Month {node.Tag.Month}"
            FilterGridByMonth(node.Parent.Tag.Year, node.Tag.Month)

        ElseIf TypeOf node.Tag Is DayNode Then
            Me.tslblActiveFilter.Text = $"Active Filter: Year {node.Parent.Parent.Tag.Year}, Month {node.Parent.Tag.Month}, Day {node.Tag.Day}"
            FilterGridByDay(node.Parent.Parent.Tag.Year,
                            node.Parent.Tag.Month,
                            node.Tag.Day)
        End If
    End Sub

    ''' <summary>
    ''' Handles the click event of the ToolStripButton1 control. It populates the grid header, builds the tree view, and populates the grid with data from the Allsky tree structure. The method updates the status label and progress bar to provide feedback on the population process.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click

        ToolStripStatusLabel1.Text = "Building Tree"
        Me.Refresh()
        'Await Task.Run(Sub() PopulateTreeView())
        PopulateTreeView()
        ToolStripStatusLabel1.Text = "Building Grid"
        Me.Refresh()
        ToolStripProgressBar1.Maximum = allSkyTree.Traverse().Count(Function(n) TypeOf n.Data Is DayNode)
        'Await Task.Run(Sub() PopulateGrid())
        PopulateGrid()
        ToolStripStatusLabel1.Text = ""
        ToolStripProgressBar1.Visible = False
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ScoreManager.SaveScores()
    End Sub

    ''' <summary>
    ''' Handles the Paint event of a PictureBox control in the grid. It retrieves the item ID from the PictureBox's Tag property, gets the corresponding score from the ScoreManager, and draws a score overlay on the PictureBox using the DrawScoreOverlay method. The overlay displays the score as a string in a bold, gold-colored font.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PictureBox_PaintScoreOverlay(sender As Object, e As PaintEventArgs)
        Dim tag = TryCast(sender.Tag, PicTag)
        If tag Is Nothing Then Exit Sub

        Dim pic = DirectCast(sender, PictureBox)

        ' Each PictureBox.Tag contains the item path (you already set this)
        Dim itemId As String = tag.ItemId

        If String.IsNullOrEmpty(itemId) Then Exit Sub

        ' Get score from ScoreManager
        Dim score As Integer = ScoreManager.GetScore(itemId)

        ' Draw overlay
        DrawScoreOverlay(e.Graphics, pic.ClientRectangle, score)
    End Sub

    ''' <summary>
    ''' Draws a score overlay on the specified graphics object within the given cell bounds. The overlay displays the score as a string in a bold, gold-colored font. If the score is less than or equal to zero, the method exits without drawing anything.    
    ''' </summary>
    ''' <param name="g"></param>
    ''' <param name="cellBounds"></param>
    ''' <param name="score"></param>
    Private Sub DrawScoreOverlay(g As Graphics, cellBounds As Rectangle, score As Integer)
        If score < 0 Then Exit Sub

        Dim stars As String = New String("★"c, score)

        Using font As New Font("Segoe UI", 10, FontStyle.Bold)
            Dim size = g.MeasureString(stars, font)
            Dim x = cellBounds.Right - size.Width - 4
            Dim y = cellBounds.Top + 4


            ' Outline (black)
            Using outlineBrush As New SolidBrush(Color.Black)
                g.DrawString(stars, font, outlineBrush, x + 1, y + 1)
            End Using

            ' Foreground (gold)
            Using fgBrush As New SolidBrush(Color.Gold)
                g.DrawString(stars, font, fgBrush, x, y)
            End Using
        End Using

    End Sub

    ''' <summary>
    '''  Handles the mouse click event on a PictureBox control in the grid. It retrieves the item ID from the PictureBox's Tag property and gets the current score from the ScoreManager. If the right mouse button is clicked, it checks for modifier keys (Shift or Control) to determine whether to increment or decrement the score. The new score is then updated in the ScoreManager, and the PictureBox is invalidated to trigger a repaint and show the updated score overlay.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PictureBox_ClickScore(sender As Object, e As MouseEventArgs)
        Dim tag = TryCast(sender.Tag, PicTag)
        If tag Is Nothing Then Exit Sub
        Dim itemId As String = tag.ItemId
        If String.IsNullOrEmpty(itemId) Then Exit Sub
        ' Get current score
        Dim currentScore As ScoreType = ScoreManager.GetScore(itemId)
        Dim newScore As ScoreType = currentScore

        If e.Button = MouseButtons.Right Then
            If Control.ModifierKeys = Keys.Shift Then
                newScore = currentScore + 1
            End If
            If Control.ModifierKeys = Keys.Control Then
                newScore = currentScore - 1
            End If
            ' Update score in ScoreManager
            newScore = Math.Max(0, newScore) ' Ensure score doesn't go below 0
            newScore = Math.Min(ScoreType.Cool, newScore) ' Ensure score doesn't go above 3

            If newScore <> currentScore Then
                ScoreManager.SetScore(itemId, newScore)
                ' Refresh the PictureBox to trigger repaint and show updated score
                DirectCast(sender, PictureBox).Invalidate()
                ScoreManager.SaveScores()
                UpdateScoresCount()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Updates the score counts displayed in the status strip. It retrieves the counts for each score type (Crap, Good, Nice, Cool) from the ScoreManager and updates the corresponding ToolStripStatusLabel controls with the formatted count strings.
    ''' </summary>
    Private Sub UpdateScoresCount()
        tsslblCountCrap.Text = $"Crap: {ScoreManager.CountScore(ScoreType.Crap)}"
        tsslblCountPass.Text = $"Good: {ScoreManager.CountScore(ScoreType.Pass)}"
        tsslblCountNice.Text = $"Nice: {ScoreManager.CountScore(ScoreType.Nice)}"
        tsslblCountCool.Text = $"Cool: {ScoreManager.CountScore(ScoreType.Cool)}"
    End Sub

    Private Sub tsbPurge_Click(sender As Object, e As EventArgs) Handles tsbPurge.Click
        PurgeItems(ScoreType.Crap)
    End Sub

    ''' <summary>
    ''' Purges items with the specified score type from the ScoreManager and deletes associated files. It retrieves the list of items to purge based on the score type, prompts the user for confirmation, and if confirmed, iterates through each item to delete associated files (meteor images, marked images, metadata, thumbnails) and removes the score from the ScoreManager. After purging, it refreshes the grid and updates the score counts in the status strip.
    ''' </summary>
    ''' <param name="scoreType"></param>
    Private Sub PurgeItems(scoreType As ScoreType)
        Dim itemsToPurge = ScoreManager.GetItemsByScore(scoreType)

        If itemsToPurge.Count = 0 Then
            MessageBox.Show($"No items with score '{scoreType}' to purge.", "Purge Items", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim result = MessageBox.Show($"Are you sure you want to purge {itemsToPurge.Count} items with score '{scoreType}'? This action cannot be undone.", "Confirm Purge", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result = DialogResult.Yes Then
            TableLayoutPanel1.SuspendLayout()
            ClearAllRows(TableLayoutPanel1)

            For Each itemId In itemsToPurge
                Dim suffixStart As Integer = itemId.LastIndexOf("-")
                If suffixStart <> -1 Then
                    Dim suffix As String = itemId.Substring(suffixStart + 1)
                    Dim baseName As String = itemId.Substring(0, suffixStart)
                    Dim dt As DateTime
                    Dim dateTimeString As String
                    'DateTime.TryParse(baseName, dt)
                    If suffix = "Meteor" Then
                        dt = DateTime.ParseExact(baseName, "yyyy-MM-dd-HHmmss", System.Globalization.CultureInfo.InvariantCulture)
                        dateTimeString = dt.ToString(_cfg.DatetimeFormat) '"yyyyMMddHHmmss")
                        Dim node As vTreeNode = FindNode(allSkyTree.Root, dateTimeString, suffix)
                        Dim meteorNode As MeteorNode = node.Data
                        If meteorNode IsNot Nothing Then
                            If IO.File.Exists(meteorNode.MeteorImageThumbnailPath) Then
                                Try
                                    IO.File.Delete(meteorNode.MeteorImageThumbnailPath)
                                Catch ex As Exception
                                    MsgBox($"Error: Could not delete {meteorNode.MeteorImageThumbnailPath}", MsgBoxStyle.Exclamation & MsgBoxStyle.OkOnly)
                                End Try
                            End If

                            If IO.File.Exists(meteorNode.MeteorImagePath) Then
                                Try
                                    IO.File.Delete(meteorNode.MeteorImagePath)
                                Catch ex As Exception
                                    MsgBox($"Error: Could not delete {meteorNode.MeteorImagePath}", MsgBoxStyle.Exclamation & MsgBoxStyle.OkOnly)
                                End Try
                            End If

                            If IO.File.Exists(meteorNode.MeteorMarkedImageThumbnailPath) Then
                                Try
                                    IO.File.Delete(meteorNode.MeteorMarkedImageThumbnailPath)
                                Catch ex As Exception
                                    MsgBox($"Error: Could not delete {meteorNode.MeteorMarkedImageThumbnailPath}", MsgBoxStyle.Exclamation & MsgBoxStyle.OkOnly)
                                End Try
                            End If

                            If IO.File.Exists(meteorNode.MeteorMarkedImagePath) Then
                                Try
                                    IO.File.Delete(meteorNode.MeteorMarkedImagePath)
                                Catch ex As Exception
                                    MsgBox($"Error: Could not delete {meteorNode.MeteorMarkedImagePath}", MsgBoxStyle.Exclamation & MsgBoxStyle.OkOnly)
                                End Try
                            End If

                            If IO.File.Exists(meteorNode.MeteorMetadataPath) Then
                                Try
                                    IO.File.Delete(meteorNode.MeteorMetadataPath)
                                Catch ex As Exception
                                    MsgBox($"Error: Could not delete {meteorNode.MeteorMetadataPath}", MsgBoxStyle.Exclamation & MsgBoxStyle.OkOnly)
                                End Try
                            End If

                        End If
                    Else
                        dt = DateTime.ParseExact(baseName, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)
                        dateTimeString = dt.ToString(_cfg.DateFormat) '"yyyyMMdd")
                        Dim node As vTreeNode = FindNode(allSkyTree.Root, dateTimeString, suffix)
                        If suffix = "Recap" Then
                            Dim nodeRecap As RecapNode = node.Data
                        Else
                            Dim nodeVideoImage As VideoImageNode = node.Data
                            If nodeVideoImage IsNot Nothing Then
                                If IO.File.Exists(nodeVideoImage.VideoImageThumbnailPath) Then
                                    Try
                                        IO.File.Delete(nodeVideoImage.VideoImageThumbnailPath)
                                    Catch ex As Exception
                                        MsgBox($"Error: Could not delete {nodeVideoImage.VideoImageThumbnailPath}", MsgBoxStyle.Exclamation & MsgBoxStyle.OkOnly)
                                    End Try
                                End If

                                If IO.File.Exists(nodeVideoImage.VideoImagePath) Then
                                    Try
                                        IO.File.Delete(nodeVideoImage.VideoImagePath)
                                    Catch ex As Exception
                                        MsgBox($"Error: Could not delete {nodeVideoImage.VideoImagePath}", MsgBoxStyle.Exclamation & MsgBoxStyle.OkOnly)
                                    End Try
                                End If
                            End If
                        End If
                    End If

                End If
                ScoreManager.RemoveScore(itemId)
            Next
            TableLayoutPanel1.ResumeLayout()

            ScoreManager.SaveScores()
            'Refresh the grid And update scores count
            ExploreFileSystem()
            PopulateTreeView()
            PopulateGrid()
            UpdateScoresCount()
            MessageBox.Show($"{itemsToPurge.Count} items with score '{scoreType}' have been purged.", "Purge Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

End Class

Public Class PicTag
    Public Property Path As String
    Public Property ItemId As String
End Class

Public Class MeteorTag
    Public Property MeteorNode As MeteorNode
    Public Property ItemId As String
End Class

