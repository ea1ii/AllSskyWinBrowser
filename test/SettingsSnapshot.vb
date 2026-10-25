Public Class SettingsSnapshot
    ' reading settings from config file

    Public Property DateFormat As String
    Public Property DatetimeFormat As String

    Public Property BasePath As String

    Public Property DayFolderMask As String

    Public Property DayTimelapsePath As String
    Public Property DayTimelapseMask As String
    Public Property DayTimeLapseThumbnailPath As String
    Public Property DayTimelapseThumbnailMask As String

    Public Property NightTimelapsePath As String
    Public Property NightTimelapseMask As String
    Public Property NightTimelapseThumbnailPath As String
    Public Property NightTimelapseThumbnailMask As String

    Public Property KeogramPath As String
    Public Property KeogramMask As String
    Public Property KeogramThumbnailPath As String
    Public Property KeogramThumbnailMask As String

    Public Property KeolapsePath As String
    Public Property KeolapseMask As String
    Public Property KeolapseThumbnailPath As String
    Public Property KeolapseThumbnailMask As String

    Public Property StartrailPath As String
    Public Property StartrailMask As String
    Public Property StartrailThumbnailPath As String
    Public Property StartrailThumbnailMask As String

    Public Property StartraillapsePath As String
    Public Property StartraillapseMask As String
    Public Property StartraillapseThumbnailPath As String
    Public Property StartraillapseThumbnailMask As String

    Public Property RecapPath As String
    Public Property RecapImageMask As String
    Public Property RecapVideoMask As String
    Public Property RecapMetadataMask As String

    Public Property MeteorPath As String
    Public Property MeteorMask As String
    Public Property MeteorThumbnailPath As String
    Public Property MeteorThumbnailMask As String
    Public Property MeteorMarkedMask As String
    Public Property MeteorMarkedThumbnailMask As String
    Public Property MeteorMetadataMask As String

    Public Property ShowEmptyFolders As Boolean
    Public Property DeleteEmptyFolders As Boolean

    Public Property ThumbnailWidth As Integer
    Public Property ThumbnailHeight As Integer
    Public Property ThumbnailCreate As Boolean

    'calculated settings

    Public Property FullDayFolderPath As String

    Public Property FullDayTimelapsePath As String
    Public Property FullDayTimelapseThumbnailPath As String

    Public Property FullNightTimelapsePath As String
    Public Property FullNightTimelapseThumbnailPath As String

    Public Property FullKeogramPath As String
    Public Property FullKeogramThumbnailPath As String

    Public Property FullKeolapsePath As String
    Public Property FullKeolapseThumbnailPath As String

    Public Property FullStartrailPath As String
    Public Property FullStartrailThumbnailPath As String

    Public Property FullStartraillapsePath As String
    Public Property FullStartraillapseThumbnailPath As String

    Public Property FullRecapPath As String

    Public Property FullMeteorPath As String
    Public Property FullMeteorThumbnailPath As String



    ''' <summary>
    ''' Creates a snapshot of the current settings from My.Settings and returns it as a SettingsSnapshot object.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSettingsSnapshot() As SettingsSnapshot
        Return New SettingsSnapshot With {
            .DateFormat = My.Settings.DateFormat,
            .DatetimeFormat = My.Settings.DatetimeFormat,
            .BasePath = My.Settings.BasePath,
            .DayFolderMask = My.Settings.DayFolderMask,
            .DayTimelapsePath = My.Settings.DayTimelapsePath,
            .DayTimelapseMask = My.Settings.DayTimelapseMask,
            .DayTimeLapseThumbnailPath = My.Settings.DayTimelapseThumbnailPath,
            .DayTimelapseThumbnailMask = My.Settings.DayTimelapseThumbnailMask,
            .NightTimelapsePath = My.Settings.NightTimelapsePath,
            .NightTimelapseMask = My.Settings.NightTimelapseMask,
            .NightTimelapseThumbnailPath = My.Settings.NightTimelapseThumbnailPath,
            .NightTimelapseThumbnailMask = My.Settings.NightTimelapseThumbnailMask,
            .KeogramPath = My.Settings.KeogramPath,
            .KeogramMask = My.Settings.KeogramMask,
            .KeogramThumbnailPath = My.Settings.KeogramThumbnailPath,
            .KeogramThumbnailMask = My.Settings.KeogramThumbnailMask,
            .KeolapsePath = My.Settings.KeolapsePath,
            .KeolapseMask = My.Settings.KeolapseMask,
            .KeolapseThumbnailPath = My.Settings.KeolapseThumbnailPath,
            .KeolapseThumbnailMask = My.Settings.KeolapseThumbnailMask,
            .StartrailPath = My.Settings.StartrailPath,
            .StartrailMask = My.Settings.StartrailMask,
            .StartrailThumbnailPath = My.Settings.StartrailThumbnailPath,
            .StartrailThumbnailMask = My.Settings.StartrailThumbnailMask,
            .StartraillapsePath = My.Settings.StartraillapsePath,
            .StartraillapseMask = My.Settings.StartraillapseMask,
            .StartraillapseThumbnailPath = My.Settings.StartraillapseThumbnailPath,
            .StartraillapseThumbnailMask = My.Settings.StartraillapseThumbnailMask,
            .RecapPath = My.Settings.RecapPath,
            .RecapImageMask = My.Settings.RecapImageMask,
            .RecapVideoMask = My.Settings.RecapVideoMask,
            .RecapMetadataMask = My.Settings.RecapMetadataMask,
            .MeteorPath = My.Settings.MeteorPath,
            .MeteorMask = My.Settings.MeteorMask,
            .ThumbnailWidth = My.Settings.ThumbnailWidth,
            .ThumbnailHeight = My.Settings.ThumbnailHeight,
            .ThumbnailCreate = My.Settings.ThumbnailCreate
        }
    End Function

    ''' <summary>
    ''' Loads the configuration settings from My.Settings into the properties of this SettingsSnapshot instance.
    ''' </summary>
    Public Sub LoadConfig()
        ' Load configuration settings here

        DateFormat = My.Settings.DateFormat
        DatetimeFormat = My.Settings.DatetimeFormat

        BasePath = My.Settings.BasePath
        DayFolderMask = My.Settings.DayFolderMask

        DayTimelapsePath = My.Settings.DayTimelapsePath
        DayTimelapseMask = My.Settings.DayTimelapseMask
        DayTimeLapseThumbnailPath = My.Settings.DayTimelapseThumbnailPath
        DayTimelapseThumbnailMask = My.Settings.DayTimelapseThumbnailMask

        NightTimelapsePath = My.Settings.NightTimelapsePath
        NightTimelapseMask = My.Settings.NightTimelapseMask
        NightTimelapseThumbnailPath = My.Settings.NightTimelapseThumbnailPath
        NightTimelapseThumbnailMask = My.Settings.NightTimelapseThumbnailMask

        KeogramPath = My.Settings.KeogramPath
        KeogramMask = My.Settings.KeogramMask
        KeogramThumbnailPath = My.Settings.KeogramThumbnailPath
        KeogramThumbnailMask = My.Settings.KeogramThumbnailMask

        KeolapsePath = My.Settings.KeolapsePath
        KeolapseMask = My.Settings.KeolapseMask
        KeolapseThumbnailPath = My.Settings.KeolapseThumbnailPath
        KeolapseThumbnailMask = My.Settings.KeolapseThumbnailMask

        StartrailPath = My.Settings.StartrailPath
        StartrailMask = My.Settings.StartrailMask
        StartrailThumbnailPath = My.Settings.StartrailThumbnailPath
        StartrailThumbnailMask = My.Settings.StartrailThumbnailMask

        StartraillapsePath = My.Settings.StartraillapsePath
        StartraillapseMask = My.Settings.StartraillapseMask
        StartraillapseThumbnailPath = My.Settings.StartraillapseThumbnailPath
        StartraillapseThumbnailMask = My.Settings.StartraillapseThumbnailMask

        RecapPath = My.Settings.RecapPath
        RecapImageMask = My.Settings.RecapImageMask
        RecapVideoMask = My.Settings.RecapVideoMask
        RecapMetadataMask = My.Settings.RecapMetadataMask

        MeteorPath = My.Settings.MeteorPath
        MeteorMask = My.Settings.MeteorMask
        MeteorThumbnailPath = My.Settings.MeteorThumbnailPath
        MeteorThumbnailMask = My.Settings.MeteorThumbnailMask
        MeteorMarkedMask = My.Settings.MeteorMarkedMask
        MeteorMarkedThumbnailMask = My.Settings.MeteorMarkedThumbnailMask
        MeteorMetadataMask = My.Settings.MeteorMetadataMask

        ShowEmptyFolders = My.Settings.ShowEmptyFolders
        DeleteEmptyFolders = My.Settings.DeleteEmptyFolders

        ThumbnailWidth = My.Settings.ThumbnailWidth
        ThumbnailHeight = My.Settings.ThumbnailHeight
        ThumbnailCreate = My.Settings.ThumbnailCreate

    End Sub

    ''' <summary>
    ''' Saves the current configuration settings from this SettingsSnapshot instance into My.Settings.
    ''' </summary>
    Public Sub SaveConfig()
        ' Save configuration settings here
        With My.Settings
            .DateFormat = DateFormat
            .DatetimeFormat = DatetimeFormat

            .BasePath = BasePath
            .DayFolderMask = DayFolderMask

            .DayTimelapsePath = DayTimelapsePath
            .DayTimelapseMask = DayTimelapseMask
            .DayTimelapseThumbnailPath = DayTimeLapseThumbnailPath
            .DayTimelapseThumbnailMask = DayTimelapseThumbnailMask

            .NightTimelapsePath = NightTimelapsePath
            .NightTimelapseMask = NightTimelapseMask
            .NightTimelapseThumbnailPath = NightTimelapseThumbnailPath
            .NightTimelapseThumbnailMask = NightTimelapseThumbnailMask

            .KeogramPath = KeogramPath
            .KeogramMask = KeogramMask
            .KeogramThumbnailPath = KeogramThumbnailPath
            .KeogramThumbnailMask = KeogramThumbnailMask

            .KeolapsePath = KeolapsePath
            .KeolapseMask = KeolapseMask
            .KeolapseThumbnailPath = KeolapseThumbnailPath
            .KeolapseThumbnailMask = KeolapseThumbnailMask

            .StartrailPath = StartrailPath
            .StartrailMask = StartrailMask
            .StartrailThumbnailPath = StartrailThumbnailPath
            .StartrailThumbnailMask = StartrailThumbnailMask

            .StartraillapsePath = StartraillapsePath
            .StartraillapseMask = StartraillapseMask
            .StartraillapseThumbnailPath = StartraillapseThumbnailPath
            .StartraillapseThumbnailMask = StartraillapseThumbnailMask

            .RecapPath = RecapPath
            .RecapImageMask = RecapImageMask
            .RecapVideoMask = RecapVideoMask
            .RecapMetadataMask = RecapMetadataMask

            .MeteorPath = MeteorPath
            .MeteorMask = MeteorMask
            .MeteorThumbnailPath = MeteorThumbnailPath
            .MeteorThumbnailMask = MeteorThumbnailMask
            .MeteorMarkedMask = MeteorMarkedMask
            .MeteorMarkedThumbnailMask = MeteorMarkedThumbnailMask
            .MeteorMetadataMask = MeteorMetadataMask

            .ShowEmptyFolders = ShowEmptyFolders
            .DeleteEmptyFolders = DeleteEmptyFolders

            .ThumbnailWidth = ThumbnailWidth
            .ThumbnailHeight = ThumbnailHeight
            .ThumbnailCreate = ThumbnailCreate
        End With

        My.Settings.Save()
    End Sub

    ''' <summary>
    ''' Calculates derived settings based on the loaded configuration, such as full paths for various media types, and stores them in the corresponding properties of this SettingsSnapshot instance.
    ''' </summary>
    Public Sub CalculateDerivedSettings()
        ' Calculate derived settings based on the loaded configuration
        If Not String.IsNullOrEmpty(BasePath) And System.IO.Path.Exists(BasePath) Then
            FullDayFolderPath = CombinePaths(BasePath, DayFolderMask)
            FullDayTimelapsePath = CombinePaths(FullDayFolderPath, DayTimelapsePath)
            FullDayTimelapseThumbnailPath = CombinePaths(FullDayFolderPath, DayTimeLapseThumbnailPath)
            FullNightTimelapsePath = CombinePaths(FullDayFolderPath, NightTimelapsePath)
            FullNightTimelapseThumbnailPath = CombinePaths(FullDayFolderPath, NightTimelapseThumbnailPath)
            FullKeogramPath = CombinePaths(FullDayFolderPath, KeogramPath)
            FullKeogramThumbnailPath = CombinePaths(FullDayFolderPath, KeogramThumbnailPath)
            FullKeolapsePath = CombinePaths(FullDayFolderPath, KeolapsePath)
            FullKeolapseThumbnailPath = CombinePaths(FullDayFolderPath, KeolapseThumbnailPath)
            FullStartrailPath = CombinePaths(FullDayFolderPath, StartrailPath)
            FullStartrailThumbnailPath = CombinePaths(FullDayFolderPath, StartrailThumbnailPath)
            FullStartraillapsePath = CombinePaths(FullDayFolderPath, StartraillapsePath)
            FullStartraillapseThumbnailPath = CombinePaths(FullDayFolderPath, StartraillapseThumbnailPath)
            FullRecapPath = CombinePaths(FullDayFolderPath, RecapPath)
            FullMeteorPath = CombinePaths(FullDayFolderPath, MeteorPath)
            FullMeteorThumbnailPath = CombinePaths(FullDayFolderPath, MeteorThumbnailPath)
        End If
    End Sub

End Class
