Imports System.IO
Imports System.Windows.Forms

Module MainModule
    Public allSkyTree As New Tree(New RootNode())
    Public cfg As SettingsSnapshot

    <STAThread>
    Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        cfg = New SettingsSnapshot()
        cfg.LoadConfig()

        While Not IsConfigurationValid()
            MessageBox.Show(
                "Please configure the AllSky base folder before continuing.",
                "Configuration Required",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

            Using dlg As New frmConfig(cfg)
                If dlg.ShowDialog() <> DialogResult.OK Then
                    Return
                End If
                cfg.SaveConfig()
            End Using
            cfg.LoadConfig()
        End While

        cfg.CalculateDerivedSettings()

        'Load scores from the JSON file to ensure that any existing scores are available for use in the application.
        ScoreManager.LoadScores()
        ExploreFileSystem()
        ScoreManager.SaveScores() ' TBD if this is needed here or only when scores are changed

        If cfg.DeleteEmptyFolders Then
            RemoveEmptyFolders(cfg.BasePath)
        End If

        Dim mainForm As New Form1(cfg)
        Application.Run(mainForm)

    End Sub

    ''' <summary>
    ''' Validates the configuration settings to ensure that the base path is not null or empty and that it exists as a directory. Returns True if the configuration is valid; otherwise, returns False.
    ''' </summary>
    ''' <returns></returns>
    Private Function IsConfigurationValid() As Boolean
        If String.IsNullOrWhiteSpace(cfg.BasePath) Then Return False
        If Not IO.Directory.Exists(cfg.BasePath) Then Return False
        Return True
    End Function

    ''' <summary>
    ''' Validates if the given string is a valid date according to the configured date format.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    Private Function ValidDate(name As String) As Boolean
        Return Date.TryParseExact(name, cfg.DateFormat, Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, Nothing)
    End Function

    ''' <summary>
    ''' Extracts the date and time part from a given filename based on the configured meteor mask. It removes the prefix and suffix defined in the mask to isolate the date-time portion of the filename.
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    Private Function ExtractDateTimeFromFileName(fileName As String) As String
        Dim fileNameOnly As String = System.IO.Path.GetFileNameWithoutExtension(fileName)

        Dim DTstart As Integer = cfg.MeteorMask.LastIndexOf("[")
        Dim DTend As Integer = cfg.MeteorMask.LastIndexOf("]")

        Dim dateTimePart As String = ""

        If DTstart >= 0 AndAlso DTend > DTstart Then
            Dim DTmask As String = cfg.MeteorMask.Substring(DTstart, DTend - DTstart + 1)
            Dim prefix As String = cfg.MeteorMask.Substring(0, DTstart)
            Dim suffix As String = cfg.MeteorMask.Substring(DTend + 1)

            dateTimePart = fileNameOnly

            If fileNameOnly.StartsWith(prefix) Then
                dateTimePart = dateTimePart.Substring(prefix.Length, dateTimePart.Length - prefix.Length)
            End If
            If fileNameOnly.EndsWith(suffix) Then
                dateTimePart = dateTimePart.Substring(0, dateTimePart.Length - suffix.Length)
            End If
        End If

        Return dateTimePart

    End Function

    ''' <summary>
    ''' Validates if the given filename contains a valid date-time portion according to the configured datetime format. It extracts the date-time part from the filename and checks if it can be parsed into a DateTime object.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    Private Function ValidDateTime(name As String) As Boolean
        Dim dtPart As String = ExtractDateTimeFromFileName(name)
        Return DateTime.TryParseExact(dtPart, cfg.DatetimeFormat, Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, Nothing)
    End Function

    ''' <summary>
    ''' Creates a thumbnail image from the specified image file path with the given width and height. It loads the original image, creates a new bitmap for the thumbnail, and draws the original image onto the thumbnail using high-quality bicubic interpolation. The resulting thumbnail is returned as a Bitmap object.
    ''' </summary>
    ''' <param name="imagePath"></param>
    ''' <param name="width"></param>
    ''' <param name="height"></param>
    ''' <returns></returns>
    Private Function CreateThumbnail(imagePath As String, width As Integer, height As Integer) As Bitmap
        If Not IsNothing(imagePath) AndAlso IO.File.Exists(imagePath) Then
            Using originalImage As Image = Image.FromFile(imagePath)
                Dim thumbnail As New Bitmap(width, height)
                Using g As Graphics = Graphics.FromImage(thumbnail)
                    g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    g.DrawImage(originalImage, 0, 0, width, height)
                End Using
                Return thumbnail
            End Using
        End If
    End Function

    ''' <summary>
    ''' Combines two paths, ensuring that if the subPath is ".", it returns the basePath. If either path is null or empty, it returns Nothing.
    ''' </summary>
    ''' <param name="basePath"></param>
    ''' <param name="subPath"></param>
    ''' <returns></returns>
    Public Function CombinePaths(basePath As String, subPath As String) As String
        If String.IsNullOrEmpty(basePath) Or String.IsNullOrEmpty(subPath) Then
            Return ""
        End If
        If subPath = "." Then
            Return basePath
        End If
        Return System.IO.Path.Combine(basePath, subPath)
    End Function

    ''' <summary>
    ''' Explores the file system starting from the base path defined in the configuration. It retrieves all day folders, validates them, and constructs a tree structure representing the hierarchy of years, months, days, and associated video/image files. For each valid day folder, it checks for the existence of various media files (night timelapse, day timelapse, keogram, keolapse, startrail, startrail timelapse, recap) and adds corresponding nodes to the tree. It also searches for meteor files within each day folder and adds them to the tree if they exist. If no valid day folders are found, an error message is displayed.
    ''' </summary>
    Public Sub ExploreFileSystem()
        'get all day folders in the base path
        Dim dayfolders = System.IO.Directory.GetDirectories(cfg.BasePath).Select(Function(f) IO.Path.GetFileName(f)).ToList()
        'remove any folders that are not valid dates
        dayfolders.RemoveAll(Function(f) Not ValidDate(f))
        'if no day folders found, show message and exit
        If dayfolders.Count = 0 Then
            MessageBox.Show("No day folders found in the base path: " & cfg.BasePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        'initialise the tree with the root node
        allSkyTree = New Tree(New RootNode())

        'initialize a list to keep track of valid score IDs
        Dim validIds As List(Of String)
        validIds = New List(Of String)

        'browse through the day folders and create the tree structure
        For Each dayfolder In dayfolders
            Dim d As Date = Date.ParseExact(dayfolder, cfg.DateFormat, Globalization.CultureInfo.InvariantCulture)
            Dim year As String = d.Year.ToString
            Dim month As String = d.Month.ToString("D2")
            Dim day As String = d.Day.ToString("D2")

            allSkyTree.GetOrCreateYear(allSkyTree.Root, year)
            allSkyTree.GetOrCreateMonth(allSkyTree.GetOrCreateYear(allSkyTree.Root, year), month)
            Dim dayNode As vTreeNode = allSkyTree.GetOrCreateDay(allSkyTree.GetOrCreateMonth(allSkyTree.GetOrCreateYear(allSkyTree.Root, year), month), day)

            'now get all video/image files in the day folder
            Dim filePath As String = ""
            Dim fileThumbPath As String = ""

            Dim thumbPath As String = ""

#Region "Night Timelapse"
            'night timelapse
            filePath = CombinePaths(cfg.FullNightTimelapsePath.Replace("[D]", dayfolder), cfg.NightTimelapseMask.Replace("[D]", dayfolder))
            fileThumbPath = CombinePaths(cfg.FullNightTimelapseThumbnailPath.Replace("[D]", dayfolder), cfg.NightTimelapseThumbnailMask.Replace("[D]", dayfolder))
            If IO.File.Exists(filePath) Then
                If IO.File.Exists(fileThumbPath) Then
                    thumbPath = fileThumbPath
                Else
                    thumbPath = ""
                    'thumbnail does not exist, create it
                    'Dim thumbnail As Bitmap = CreateThumbnail(filePath, cfg.ThumbnailWidth, cfg.ThumbnailHeight)
                    'thumbnail.Save(cfg.FullNightTimelapseThumbnailPath)
                End If

                Dim nightTimelapseNode As New VideoImageNode() With {
                        .VideoImage = IO.Path.GetFileName(filePath),
                        .VideoImagePath = filePath,
                        .VideoImageThumbnailPath = thumbPath,
                        .Q = 0,
                        .Type = VideoImageType.NightTimelapse,
                        .ItemType = VideoImageType.NightTimelapse.ToString(),
                        .DateTime = d.ToString(cfg.DatetimeFormat)
                    }
                dayNode.AddChild(nightTimelapseNode)
                Dim scoreId As String = $"{year}-{month}-{day}-{nightTimelapseNode.Type}"
                If Not ScoreManager.Scores.Items.ContainsKey(scoreId) Then
                    ScoreManager.Scores.Items(scoreId) = New ScoreEntry With {.Score = ScoreType.Pass}
                End If
                validIds.Add(scoreId)
            End If
#End Region

#Region "Day Timelapse"
            'day timelapse
            filePath = CombinePaths(cfg.FullDayTimelapsePath.Replace("[D]", dayfolder), cfg.DayTimelapseMask.Replace("[D]", dayfolder))
            fileThumbPath = CombinePaths(cfg.FullDayTimelapseThumbnailPath.Replace("[D]", dayfolder), cfg.DayTimelapseThumbnailMask.Replace("[D]", dayfolder))
            If IO.File.Exists(filePath) Then
                If IO.File.Exists(fileThumbPath) Then
                    thumbPath = fileThumbPath
                Else
                    thumbPath = ""
                    'thumbnail does not exist, create it
                    'Dim thumbnail As Bitmap = CreateThumbnail(cfg.FullDayTimelapsePath, cfg.ThumbnailWidth, cfg.ThumbnailHeight)
                    'thumbnail.Save(cfg.FullDayTimelapseThumbnailPath)
                End If
                Dim dayTimelapseNode As New VideoImageNode() With {
                        .VideoImage = IO.Path.GetFileName(filePath),
                        .VideoImagePath = filePath,
                        .VideoImageThumbnailPath = thumbPath,
                        .Q = 0,
                        .Type = VideoImageType.DayTimelapse,
                        .ItemType = VideoImageType.DayTimelapse.ToString(),
                        .DateTime = d.ToString(cfg.DatetimeFormat)
                    }
                dayNode.AddChild(dayTimelapseNode)
                Dim scoreId As String = $"{year}-{month}-{day}-{dayTimelapseNode.Type}"
                If Not ScoreManager.Scores.Items.ContainsKey(scoreId) Then
                    ScoreManager.Scores.Items(scoreId) = New ScoreEntry With {.Score = ScoreType.Pass}
                End If
                validIds.Add(scoreId)
            End If
#End Region

#Region "Keogram"
            'keogram
            filePath = CombinePaths(cfg.FullKeogramPath.Replace("[D]", dayfolder), cfg.KeogramMask.Replace("[D]", dayfolder))
            fileThumbPath = CombinePaths(cfg.FullKeogramThumbnailPath.Replace("[D]", dayfolder), cfg.KeogramThumbnailMask.Replace("[D]", dayfolder))
            If IO.File.Exists(filePath) Then
                If IO.File.Exists(fileThumbPath) Then
                    thumbPath = fileThumbPath
                Else
                    If Not cfg.ThumbnailCreate Then
                        thumbPath = ""
                    Else
                        'thumbnail does Not exist, create it
                        Dim thumbnail As Bitmap = CreateThumbnail(cfg.FullKeogramPath, cfg.ThumbnailWidth, cfg.ThumbnailHeight)
                        thumbnail.Save(cfg.FullKeogramThumbnailPath)
                    End If
                End If
                Dim keogramNode As New VideoImageNode() With {
                        .VideoImage = IO.Path.GetFileName(filePath),
                        .VideoImagePath = filePath,
                        .VideoImageThumbnailPath = thumbPath,
                        .Q = 0,
                        .Type = VideoImageType.Keogram,
                        .ItemType = VideoImageType.Keogram.ToString(),
                        .DateTime = d.ToString(cfg.DatetimeFormat)
                    }
                dayNode.AddChild(keogramNode)
                Dim scoreId As String = $"{year}-{month}-{day}-{keogramNode.Type}"
                If Not ScoreManager.Scores.Items.ContainsKey(scoreId) Then
                    ScoreManager.Scores.Items(scoreId) = New ScoreEntry With {.Score = ScoreType.Pass}
                End If
                validIds.Add(scoreId)
            End If
#End Region

#Region "Day Keogram"
            'day keogram
            filePath = CombinePaths(cfg.FullDayKeogramPath.Replace("[D]", dayfolder), cfg.DayKeogramMask.Replace("[D]", dayfolder))
            fileThumbPath = CombinePaths(cfg.FullDayKeogramThumbnailPath.Replace("[D]", dayfolder), cfg.DayKeogramThumbnailMask.Replace("[D]", dayfolder))
            If IO.File.Exists(filePath) Then
                If IO.File.Exists(fileThumbPath) Then
                    thumbPath = fileThumbPath
                Else
                    If Not cfg.ThumbnailCreate Then
                        thumbPath = ""
                    Else
                        'thumbnail does Not exist, create it
                        Dim thumbnail As Bitmap = CreateThumbnail(cfg.FullDayKeogramPath, cfg.ThumbnailWidth, cfg.ThumbnailHeight)
                        thumbnail.Save(cfg.FullDayKeogramThumbnailPath)
                    End If
                End If
                Dim dayKeogramNode As New VideoImageNode() With {
                        .VideoImage = IO.Path.GetFileName(filePath),
                        .VideoImagePath = filePath,
                        .VideoImageThumbnailPath = thumbPath,
                        .Q = 0,
                        .Type = VideoImageType.DayKeogram,
                        .ItemType = VideoImageType.DayKeogram.ToString(),
                        .DateTime = d.ToString(cfg.DatetimeFormat)
                    }
                dayNode.AddChild(dayKeogramNode)
                Dim scoreId As String = $"{year}-{month}-{day}-{dayKeogramNode.Type}"
                If Not ScoreManager.Scores.Items.ContainsKey(scoreId) Then
                    ScoreManager.Scores.Items(scoreId) = New ScoreEntry With {.Score = ScoreType.Pass}
                End If
                validIds.Add(scoreId)
            End If
#End Region

#Region "Keolapse"
            'keolapse
            filePath = CombinePaths(cfg.FullKeolapsePath.Replace("[D]", dayfolder), cfg.KeolapseMask.Replace("[D]", dayfolder))
            fileThumbPath = CombinePaths(cfg.FullKeolapseThumbnailPath.Replace("[D]", dayfolder), cfg.KeolapseThumbnailMask.Replace("[D]", dayfolder))
            If IO.File.Exists(filePath) Then
                If IO.File.Exists(fileThumbPath) Then
                    thumbPath = fileThumbPath
                Else
                    thumbPath = ""
                    'thumbnail does not exist, create it
                    'Dim thumbnail As Bitmap = CreateThumbnail(cfg.FullKeolapsePath, cfg.ThumbnailWidth, cfg.ThumbnailHeight)
                    'thumbnail.Save(cfg.FullKeolapseThumbnailPath)
                End If
                Dim keolapseNode As New VideoImageNode() With {
                        .VideoImage = IO.Path.GetFileName(filePath),
                        .VideoImagePath = filePath,
                        .VideoImageThumbnailPath = thumbPath,
                        .Q = 0,
                        .Type = VideoImageType.Keolapse,
                        .ItemType = VideoImageType.Keolapse.ToString(),
                        .DateTime = d.ToString(cfg.DatetimeFormat)
                    }
                dayNode.AddChild(keolapseNode)
                Dim scoreId As String = $"{year}-{month}-{day}-{keolapseNode.Type}"
                If Not ScoreManager.Scores.Items.ContainsKey(scoreId) Then
                    ScoreManager.Scores.Items(scoreId) = New ScoreEntry With {.Score = ScoreType.Pass}
                End If
                validIds.Add(scoreId)
            End If
#End Region

#Region "Startrail"
            'startrail
            filePath = CombinePaths(cfg.FullStartrailPath.Replace("[D]", dayfolder), cfg.StartrailMask.Replace("[D]", dayfolder))
            fileThumbPath = CombinePaths(cfg.FullStartrailThumbnailPath.Replace("[D]", dayfolder), cfg.StartrailMask.Replace("[D]", dayfolder))
            If IO.File.Exists(filePath) Then
                If IO.File.Exists(fileThumbPath) Then
                    thumbPath = fileThumbPath
                Else
                    If Not cfg.ThumbnailCreate Then
                        thumbPath = ""
                    Else
                        'thumbnail does Not exist, create it
                        Dim thumbnail As Bitmap = CreateThumbnail(filePath, cfg.ThumbnailWidth, cfg.ThumbnailHeight)
                        If Not IO.Path.Exists(IO.Path.GetDirectoryName(fileThumbPath)) Then
                            Directory.CreateDirectory(IO.Path.GetDirectoryName(fileThumbPath))
                        End If
                        thumbnail.Save(fileThumbPath)
                        End If
                    End If
                Dim startrailNode As New VideoImageNode() With {
                        .VideoImage = IO.Path.GetFileName(filePath),
                        .VideoImagePath = filePath,
                        .VideoImageThumbnailPath = thumbPath,
                        .Q = 0,
                        .Type = VideoImageType.Startrail,
                        .ItemType = VideoImageType.Startrail.ToString(),
                        .DateTime = d.ToString(cfg.DatetimeFormat)
                    }
                dayNode.AddChild(startrailNode)
                Dim scoreId As String = $"{year}-{month}-{day}-{startrailNode.Type}"
                If Not ScoreManager.Scores.Items.ContainsKey(scoreId) Then
                    ScoreManager.Scores.Items(scoreId) = New ScoreEntry With {.Score = ScoreType.Pass}
                End If
                validIds.Add(scoreId)
            End If
#End Region

#Region "Startrail Timelapse"
            'startrail timelapse
            filePath = CombinePaths(cfg.FullStartraillapsePath.Replace("[D]", dayfolder), cfg.StartraillapseMask.Replace("[D]", dayfolder))
            fileThumbPath = CombinePaths(cfg.FullStartraillapseThumbnailPath.Replace("[D]", dayfolder), cfg.StartraillapseThumbnailMask.Replace("[D]", dayfolder))
            If IO.File.Exists(filePath) Then
                If IO.File.Exists(fileThumbPath) Then
                    thumbPath = fileThumbPath
                Else
                    thumbPath = ""
                    'thumbnail does not exist, create it
                    'Dim thumbnail As Bitmap = CreateThumbnail(cfg.FullStartrailTimelapsePath, cfg.ThumbnailWidth, cfg.ThumbnailHeight)
                    'thumbnail.Save(cfg.FullStartrailTimelapseThumbnailPath)
                End If
                Dim startrailTimelapseNode As New VideoImageNode() With {
                        .VideoImage = IO.Path.GetFileName(filePath),
                        .VideoImagePath = filePath,
                        .VideoImageThumbnailPath = thumbPath,
                        .Q = 0,
                        .Type = VideoImageType.StartrailTimelapse,
                        .ItemType = VideoImageType.StartrailTimelapse.ToString(),
                        .DateTime = d.ToString(cfg.DatetimeFormat)
                    }
                dayNode.AddChild(startrailTimelapseNode)
                Dim scoreId As String = $"{year}-{month}-{day}-{startrailTimelapseNode.Type}"
                If Not ScoreManager.Scores.Items.ContainsKey(scoreId) Then
                    ScoreManager.Scores.Items(scoreId) = New ScoreEntry With {.Score = ScoreType.Pass}
                End If
                validIds.Add(scoreId)
            End If
#End Region

#Region "Recap"
            'recap
            Dim imagePath As String = CombinePaths(cfg.FullRecapPath.Replace("[D]", dayfolder), cfg.RecapImageMask.Replace("[D]", dayfolder))
            Dim videoPath As String = CombinePaths(cfg.FullRecapPath.Replace("[D]", dayfolder), cfg.RecapVideoMask.Replace("[D]", dayfolder))
            Dim metaPath As String = CombinePaths(cfg.FullRecapPath.Replace("[D]", dayfolder), cfg.RecapMetadataMask.Replace("[D]", dayfolder))

            If IO.File.Exists(imagePath) AndAlso IO.File.Exists(videoPath) AndAlso IO.File.Exists(metaPath) Then
                Dim recapNode As New RecapNode() With {
                        .RecapImagePath = imagePath,
                        .RecapVideoPath = videoPath,
                        .RecapMetadataPath = metaPath,
                        .Q = 0,
                        .Type = VideoImageType.Recap,
                        .ItemType = VideoImageType.Recap.ToString(),
                        .DateTime = d.ToString(cfg.DatetimeFormat)
                    }

                Dim recapThumbnailFilename As String = cfg.RecapImageMask.Replace("[D]", dayfolder)
                recapThumbnailFilename = System.IO.Path.GetFileNameWithoutExtension(recapThumbnailFilename) & "_th" & System.IO.Path.GetExtension(recapThumbnailFilename)

                Dim FullRecapThumbPath As String = CombinePaths(cfg.FullRecapPath.Replace("[D]", dayfolder), recapThumbnailFilename
                                                                )
                If Not IO.File.Exists(FullRecapThumbPath) Then
                    'thumbnail does not exist, create it
                    Dim thumbnail As Bitmap = CreateThumbnail(imagePath, cfg.ThumbnailWidth, cfg.ThumbnailHeight)
                    thumbnail.Save(FullRecapThumbPath)
                End If
                recapThumbnailFilename = FullRecapThumbPath
                recapNode.RecapImageThumbnailPath = recapThumbnailFilename

                dayNode.AddChild(recapNode)
                Dim scoreId As String = $"{year}-{month}-{day}-{recapNode.Type}"
                If Not ScoreManager.Scores.Items.ContainsKey(scoreId) Then
                    ScoreManager.Scores.Items(scoreId) = New ScoreEntry With {.Score = ScoreType.Pass}
                End If
                validIds.Add(scoreId)
            End If
#End Region

#Region "Search for Meteors"
            ' search for meteors in the day folder

            If IO.Directory.Exists(cfg.FullMeteorPath.Replace("[D]", dayfolder)) Then
                'get all meteor files in the day folder
                Dim meteorFiles = System.IO.Directory.GetFiles(cfg.FullMeteorPath.Replace("[D]", dayfolder), cfg.MeteorMask.Replace("[DT]", dayfolder + "*")).ToList
                'remove any meteor files that do not have a valid datetime in the filename
                meteorFiles.RemoveAll(Function(f) Not ValidDateTime(f))
                Dim tomorrow As Date
                Date.TryParseExact(dayfolder, "yyyyMMdd", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, tomorrow)
                tomorrow = tomorrow.AddDays(1)
                Dim tomorrowStr As String = tomorrow.ToString("yyyyMMdd")
                Dim meteorFilesTomorrow = System.IO.Directory.GetFiles(cfg.FullMeteorPath.Replace("[D]", dayfolder), cfg.MeteorMask.Replace("[DT]", tomorrowStr + "*")).ToList
                meteorFilesTomorrow.RemoveAll(Function(f) Not ValidDateTime(f))
                meteorFiles.AddRange(meteorFilesTomorrow)

                'add meteor files to the tree
                For Each meteorFile In meteorFiles
                    'get the datetime from the filename and convert it to HHmmss format to store in the tree node name
                    Dim dtStr As String = ExtractDateTimeFromFileName(IO.Path.GetFileName(meteorFile))
                    Dim dt As DateTime = DateTime.ParseExact(dtStr, cfg.DatetimeFormat, Globalization.CultureInfo.InvariantCulture)
                    Dim time2store As String = dt.ToString("HHmmss")

                    'get the thumbnail path for the meteor file
                    Dim meteorThumbPath As String = CombinePaths(cfg.FullMeteorThumbnailPath.Replace("[D]", dayfolder), cfg.MeteorMask.Replace("[DT]", dtStr))

                    'get the marked meteor path for the marked meteor file
                    Dim meteorMarkedPath As String = CombinePaths(cfg.FullMeteorPath.Replace("[D]", dayfolder), cfg.MeteorMarkedMask.Replace("[DT]", dtStr))

                    'get the marked meteor thumbnail path for the marked meteor file    
                    Dim meteorMarkedThumbPath As String = CombinePaths(cfg.FullMeteorThumbnailPath.Replace("[D]", dayfolder), cfg.MeteorMarkedThumbnailMask.Replace("[DT]", dtStr))

                    'get the meteor metadata path for the meteor file   
                    Dim meteorMetadataPath As String = CombinePaths(cfg.FullMeteorPath.Replace("[D]", dayfolder), cfg.MeteorMetadataMask.Replace("[DT]", dtStr))

                    Dim meteorNode As New MeteorNode() With {
                            .Name = time2store,
                            .MeteorImagePath = meteorFile,
                            .MeteorImageThumbnailPath = meteorThumbPath,
                            .MeteorMarkedImagePath = meteorMarkedPath,
                            .MeteorMarkedImageThumbnailPath = meteorMarkedThumbPath,
                            .MeteorMetadataPath = meteorMetadataPath,
                            .Q = 0,
                            .Type = VideoImageType.Meteor,
                            .ItemType = VideoImageType.Meteor.ToString(),
                            .DateTime = dt.ToString(cfg.DatetimeFormat)
                        }
                    dayNode.AddChild(meteorNode)
                    Dim scoreId As String = $"{year}-{month}-{day}-{time2store}-{meteorNode.Type}"
                    If Not ScoreManager.Scores.Items.ContainsKey(scoreId) Then
                        ScoreManager.Scores.Items(scoreId) = New ScoreEntry With {.Score = ScoreType.Pass}
                    End If
                    validIds.Add(scoreId)
                Next
            End If
#End Region

        Next

        'remove any scores that are not in the validIds list
        For Each item In ScoreManager.Scores.Items.Keys.ToList()
            If Not validIds.Contains(item) Then
                ScoreManager.Scores.Items.Remove(item)
            End If
        Next
        ScoreManager.SaveScores() ' Save the updated scores after removing invalid entries
    End Sub

    ''' <summary>
    ''' Recursively removes empty folders starting from the specified path. It checks each subfolder and deletes it if it is empty. The function returns True if the folder was deleted (i.e., it was empty), and False otherwise. If an exception occurs (e.g., due to permissions or the folder being in use), it also returns False.
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Private Function RemoveEmptyFolders(path As String) As Boolean
        Dim isEmpty As Boolean = True

        Try
            ' Process subfolders first (bottom-up)
            For Each folder In Directory.GetDirectories(path)
                If Not RemoveEmptyFolders(folder) Then
                    isEmpty = False
                End If
            Next

            ' If any files exist, folder is not empty
            If Directory.GetFiles(path).Length > 0 Then
                isEmpty = False
            End If

            ' If empty, delete it
            If isEmpty Then
                Directory.Delete(path, False)
                Return True
            End If

        Catch ex As Exception
            ' Folder cannot be deleted (permissions, in use, etc.)
            Return False
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Finds a node in the tree structure based on the provided root node, date, and item type. It traverses the tree hierarchy from year to month to day, and finally searches for a child node that matches the specified item type. If a matching node is found, it is returned; otherwise, Nothing is returned.
    ''' </summary>
    ''' <param name="root"></param>
    ''' <param name="dt"></param>
    ''' <param name="itemType"></param>
    ''' <returns></returns>
    Public Function FindNode(root As vTreeNode, dts As String, typeName As String) As vTreeNode
        Dim dt As DateTime
        If typeName = "Meteor" Then
            dt = DateTime.ParseExact(dts, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture)
        Else
            dt = DateTime.ParseExact(dts, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)
        End If

        ' Year
        Dim yearNode = root.GetChild(Of YearNode)(Function(y) y.Year = dt.Year)
        If yearNode Is Nothing Then Return Nothing

        ' Month
        Dim monthNode = yearNode.GetChild(Of MonthNode)(Function(m) m.Month = dt.Month)
        If monthNode Is Nothing Then Return Nothing

        ' Day
        Dim dayNode = monthNode.GetChild(Of DayNode)(Function(d) d.Day = dt.Day)
        If dayNode Is Nothing Then Return Nothing

        ' Item
        ' Item: match by ItemType string
        Return dayNode.Children.FirstOrDefault(
            Function(c) String.Equals(c.Data.ItemType, typeName, StringComparison.OrdinalIgnoreCase)
    )

    End Function

End Module
