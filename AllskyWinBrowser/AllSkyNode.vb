''' <summary>
''' This enumeration defines the different types of video and image files that can be found in the Allsky folder structure.  The values in this enumeration are used to determine what type of video or image file is being processed, and to determine which video or image to display when there are multiple videos or images for the same date and time.
''' </summary>
Public Enum VideoImageType
    NightTimelapse
    DayTimelapse
    Keogram
    DayKeogram
    Keolapse
    Startrail
    StartrailTimelapse
    Recap
    Meteor
End Enum

''' <summary>
''' This interface defines the properties that all nodes in the Allsky folder structure must implement.  The Name property is used to display the name of the node in the TreeView control, and the ItemType property is used to determine what type of node it is (e.g. Year, Month, Day, VideoImage, Recap, Meteor).
''' </summary>
Public Interface Inode
    ''' <summary>
    ''' This property is used to display the name of the node in the TreeView control.  It is implemented by each class that implements the Inode interface, and returns the appropriate name for that node (e.g. Year, Month, Day, VideoImage, Recap, Meteor).
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property Name As String
    ''' <summary>
    ''' This property is used to determine what type of node it is (e.g. Year, Month, Day, VideoImage, Recap, Meteor).  It is implemented by each class that implements the Inode interface, and returns the appropriate type for that node.
    ''' </summary>
    ''' <returns></returns>
    Property ItemType As String
End Interface

''' <summary>
''' This interface extends the Inode interface and adds a Q property, which is used to store the quality of the video or image.  The Q property is used to determine which video or image to display when there are multiple videos or images for the same date and time.
''' </summary>
Public Interface InodeQ
    ''' <summary>
    ''' This interface extends the Inode interface and adds a Q property, which is used to store the quality of the video or image.  The Q property is used to determine which video or image to display when there are multiple videos or images for the same date and time.
    ''' </summary>
    Inherits Inode
    ''' <summary>
    ''' This property is used to store the quality of the video or image.  It is implemented by each class that implements the InodeQ interface, and returns the appropriate quality for that node.  The Q property is used to determine which video or image to display when there are multiple videos or images for the same date and time.
    ''' </summary>
    ''' <returns></returns>
    Property Q As Integer
End Interface

''' <summary>
''' This class represents the root node of the Allsky folder structure.  It implements the Inode interface and has a RootName property, which is used to store the name of the root node (e.g. "Allsky").  The Name property returns the RootName property, and the ItemType property returns "Root".
''' </summary>
Public Class RootNode
    Implements Inode
    ''' <summary>
    ''' This property is used to store the name of the root node (e.g. "Allsky").  It is implemented by the RootNode class, and returns the appropriate name for that node.
    ''' </summary>
    ''' <returns></returns>
    Public Property RootName As String
    ''' <summary>
    ''' This property is used to determine what type of node it is (e.g. Year, Month, Day, VideoImage, Recap, Meteor).  It is implemented by the RootNode class, and returns "Root".
    ''' </summary>
    ''' <returns></returns>
    Public Property ItemType As String Implements Inode.ItemType
    ''' <summary>
    ''' This property is used to display the name of the node in the TreeView control.  It is implemented by the RootNode class, and returns "Allsky".
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Name As String Implements Inode.Name
        Get
            Return "Allsky"
        End Get
    End Property
End Class

''' <summary>
''' This class represents a year node in the Allsky folder structure.  It implements the Inode interface and has a Year property, which is used to store the year of the node (e.g. "2023").  The Name property returns the Year property, and the ItemType property returns "Year".
''' </summary>
Public Class YearNode
    Implements Inode
    ''' <summary>
    ''' This property is used to store the year of the node (e.g. "2023").  It is implemented by the YearNode class, and returns the appropriate year for that node.
    ''' </summary>
    ''' <returns></returns>
    Public Property Year As String
    ''' <summary>
    ''' This property is used to determine what type of node it is (e.g. Year, Month, Day, VideoImage, Recap, Meteor).  It is implemented by the YearNode class, and returns "Year".
    ''' </summary>
    ''' <returns></returns>
    Property ItemType As String Implements Inode.ItemType
    ''' <summary>
    ''' This property is used to display the name of the node in the TreeView control.  It is implemented by the YearNode class, and returns the Year property.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Name As String Implements Inode.Name
        Get
            Return Year
        End Get
    End Property
End Class

''' <summary>
''' This class represents a month node in the Allsky folder structure.  It implements the Inode interface and has a Month property, which is used to store the month of the node (e.g. "01" for January).  The Name property returns the Month property, and the ItemType property returns "Month".
''' </summary>
Public Class MonthNode
    Implements Inode
    ''' <summary>
    ''' This property is used to store the month of the node (e.g. "01" for January).  It is implemented by the MonthNode class, and returns the appropriate month for that node.
    ''' </summary>
    ''' <returns></returns>
    Public Property Month As String
    ''' <summary>
    ''' This property is used to determine what type of node it is (e.g. Year, Month, Day, VideoImage, Recap, Meteor).  It is implemented by the MonthNode class, and returns "Month".
    ''' </summary>
    ''' <returns></returns>
    Public Property ItemType As String Implements Inode.ItemType
    ''' <summary>
    ''' This property is used to display the name of the node in the TreeView control.  It is implemented by the MonthNode class, and returns the Month property.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Name As String Implements Inode.Name
        Get
            Return Month
        End Get
    End Property
End Class

''' <summary>
''' This class represents a day node in the Allsky folder structure.  It implements the Inode interface and has a Day property, which is used to store the day of the node (e.g. "01" for the first day of the month).  The Name property returns the Day property, and the ItemType property returns "Day".
''' </summary>
Public Class DayNode
    Implements Inode
    ''' <summary>
    ''' This property is used to store the day of the node (e.g. "01" for the first day of the month).  It is implemented by the DayNode class, and returns the appropriate day for that node.
    ''' </summary>
    ''' <returns></returns>
    Public Property Day As String
    ''' <summary>
    ''' This property is used to determine what type of node it is (e.g. Year, Month, Day, VideoImage, Recap, Meteor).  It is implemented by the DayNode class, and returns "Day".
    ''' </summary>
    ''' <returns></returns>
    Public Property ItemType As String Implements Inode.ItemType
    ''' <summary>
    ''' This property is used to display the name of the node in the TreeView control.  It is implemented by the DayNode class, and returns the Day property.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Name As String Implements Inode.Name
        Get
            Return Day
        End Get
    End Property
End Class

''' <summary>
''' This class represents a video or image node in the Allsky folder structure.  It implements the InodeQ interface and has a VideoImage property, which is used to store the name of the video or image file (e.g. "2023-01-01_00-00-00.mp4").  The Name property returns the VideoImage property, and the ItemType property returns "VideoImage".  The Q property is used to store the quality of the video or image, and is used to determine which video or image to display when there are multiple videos or images for the same date and time.
''' </summary>
Public Class VideoImageNode
    Implements InodeQ
    ''' <summary>
    ''' This property is used to store the name of the video or image file (e.g. "2023-01-01_00-00-00.mp4").  It is implemented by the VideoImageNode class, and returns the appropriate name for that node.
    ''' </summary>
    ''' <returns></returns>
    Public Property VideoImage As String
    ''' <summary>
    ''' This property is used to determine what type of node it is (e.g. Year, Month, Day, VideoImage, Recap, Meteor).  It is implemented by the VideoImageNode class, and returns "VideoImage".
    ''' </summary>
    ''' <returns></returns>
    Public Property Type As VideoImageType
    Private _q As Integer
    Private _ty As String
    Private _dt As String
    ''' <summary>
    ''' This property is used to display the name of the node in the TreeView control.  It is implemented by the VideoImageNode class, and returns the VideoImage property.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Name As String Implements Inode.Name
        Get
            Return VideoImage
        End Get
    End Property

    ''' <summary>
    ''' This property is used to determine what type of node it is (e.g. Year, Month, Day, VideoImage, Recap, Meteor).  It is implemented by the VideoImageNode class, and returns "VideoImage".
    ''' </summary>
    ''' <returns></returns>
    Public Property ItemType As String Implements Inode.ItemType
        Get
            Return _ty
        End Get
        Set(value As String)
            _ty = value
        End Set
    End Property

    ''' <summary>
    ''' This property is used to store the quality of the video or image.  It is implemented by the VideoImageNode class, and returns the appropriate quality for that node.  The Q property is used to determine which video or image to display when there are multiple videos or images for the same date and time.
    ''' </summary>
    ''' <returns></returns>
    Public Property Q As Integer Implements InodeQ.Q
        Get
            Return _q
        End Get
        Set(value As Integer)
            _q = value
        End Set
    End Property

    ''' <summary>
    ''' This property is used to store the date and time of the video or image.  It is implemented by the VideoImageNode class, and returns the appropriate date and time for that node.
    ''' </summary>
    ''' <returns></returns>
    Public Property DateTime As String
        Get
            Return _dt
        End Get
        Set(value As String)
            _dt = value
        End Set
    End Property

    ''' <summary>
    ''' This property is used to store the path to the video or image file.  It is implemented by the VideoImageNode class, and returns the appropriate path for that node.
    ''' </summary>
    ''' <returns></returns>
    Public Property VideoImagePath As String
    ''' <summary>
    ''' This property is used to store the path to the thumbnail image for the video or image file.  It is implemented by the VideoImageNode class, and returns the appropriate path for that node.
    ''' </summary>
    ''' <returns></returns>
    Public Property VideoImageThumbnailPath As String
    ''' <summary>
    ''' This property is used to determine if the thumbnail image for the video or image file exists.  It is implemented by the VideoImageNode class, and returns True if the VideoImageThumbnailPath property is not null or empty, and False otherwise.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property VideoImageThumbnailExists As Boolean
        Get
            Return Not String.IsNullOrEmpty(VideoImageThumbnailPath)
        End Get
    End Property

    ''' <summary>
    ''' This property is used to determine if the video or image file is a video file.  It is implemented by the VideoImageNode class, and returns True if the VideoImage property ends with ".mp4", ".avi", or ".mov", and False otherwise.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IsVideo As Boolean
        Get
            Return VideoImage.ToLower().EndsWith(".mp4") OrElse VideoImage.ToLower().EndsWith(".avi") OrElse VideoImage.ToLower().EndsWith(".mov")
        End Get
    End Property

    ''' <summary>
    ''' This property is used to determine if the video or image file is an image file.  It is implemented by the VideoImageNode class, and returns True if the VideoImage property ends with ".jpg", ".jpeg", or ".png", and False otherwise.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IsImage As Boolean
        Get
            Return VideoImage.ToLower().EndsWith(".jpg") OrElse VideoImage.ToLower().EndsWith(".jpeg") OrElse VideoImage.ToLower().EndsWith(".png")
        End Get
    End Property



End Class

''' <summary>
''' This class represents a recap node in the Allsky folder structure.  It implements the InodeQ interface and has properties for the recap image, video, and metadata paths.  The Name property returns "Recap", and the ItemType property returns "Recap".  The Q property is used to store the quality of the recap image or video, and is used to determine which recap image or video to display when there are multiple recap images or videos for the same date and time.
''' </summary>
Public Class RecapNode
    Implements InodeQ

    Private _q As Integer
    Private _ty As String
    Private _dt As String
    ''' <summary>
    ''' This property is used to determine what type of node it is (e.g. Year, Month, Day, VideoImage, Recap, Meteor).  It is implemented by the RecapNode class, and returns "Recap".
    ''' </summary>
    ''' <returns></returns>
    Public Property Type As VideoImageType
    ''' <summary>
    ''' This property is used to store the path to the recap image file.  It is implemented by the RecapNode class, and returns the appropriate path for that node.
    ''' </summary>
    ''' <returns></returns>
    Public Property RecapImagePath As String
    ''' <summary>
    ''' This property is used to store the path to the recap video file.  It is implemented by the RecapNode class, and returns the appropriate path for that node.
    ''' </summary>
    ''' <returns></returns>
    Public Property RecapVideoPath As String
    ''' <summary>
    ''' This property is used to store the path to the recap metadata file.  It is implemented by the RecapNode class, and returns the appropriate path for that node.
    ''' </summary>
    ''' <returns></returns>
    Public Property RecapMetadataPath As String
    'these next two are used for the recap image and video thumbnails, which are generated by thia program and stored in the Allsky web folder.  They are not part of the Allsky folder structure.
    'name will add just '-th' to the end of the recap image and video names, and will be stored in the 'recap' folder
    Public Property RecapImageThumbnailPath As String

    ''' <summary>
    ''' This property is used to determine what type of node it is (e.g. Year, Month, Day, VideoImage, Recap, Meteor).  It is implemented by the RecapNode class, and returns "Recap".
    ''' </summary>
    ''' <returns></returns>
    Public Property ItemType As String Implements Inode.ItemType
        Get
            Return _ty
        End Get
        Set(value As String)
            _ty = value
        End Set
    End Property

    ''' <summary>
    ''' This property is used to store the date and time of the recap image or video.  It is implemented by the RecapNode class, and returns the appropriate date and time for that node.
    ''' </summary>
    ''' <returns></returns>
    Public Property DateTime As String
        Get
            Return _dt
        End Get
        Set(value As String)
            _dt = value
        End Set
    End Property

    ''' <summary>
    ''' This property is used to store the quality of the recap image or video.  It is implemented by the RecapNode class, and returns the appropriate quality for that node.  The Q property is used to determine which recap image or video to display when there are multiple recap images or videos for the same date and time.
    ''' </summary>
    ''' <returns></returns>
    Public Property Q As Integer Implements InodeQ.Q
        Get
            Return _q
        End Get
        Set(value As Integer)
            _q = value
        End Set
    End Property

    ''' <summary>
    ''' This property is used to display the name of the node in the TreeView control.  It is implemented by the RecapNode class, and returns "Recap".
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Name As String Implements Inode.Name
        Get
            Return "Recap"
        End Get
    End Property

    ''' <summary>
    ''' This property is used to determine if the recap image thumbnail exists.  It is implemented by the RecapNode class, and returns True if the RecapImageThumbnailPath property is not null or empty, and False otherwise.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RecapImageThumbnailExists As Boolean
        Get
            Return Not String.IsNullOrEmpty(RecapImageThumbnailPath)
        End Get
    End Property

End Class

''' <summary>
''' This class represents a meteor node in the Allsky folder structure.  It implements the InodeQ interface and has properties for the meteor image, marked image, and metadata paths.  The Name property returns the MeteorTime property, and the ItemType property returns "Meteor".  The Q property is used to store the quality of the meteor image or video, and is used to determine which meteor image or video to display when there are multiple meteor images or videos for the same date and time.
''' </summary>
Public Class MeteorNode
    Implements InodeQ
    ''' <summary>
    ''' This property is used to store the time of the meteor event.  It is implemented by the MeteorNode class, and returns the appropriate time for that node.
    ''' </summary>
    ''' <returns></returns>
    Public Property MeteorTime As String
    Private _q As Integer
    ''' <summary>
    ''' This property is used to store the path to the meteor image file.  It is implemented by the MeteorNode class, and returns the appropriate path for that node.
    ''' </summary>
    ''' <returns></returns>
    Public Property MeteorImagePath As String
    ''' <summary>
    ''' This property is used to store the path to the meteor image thumbnail file.  It is implemented by the MeteorNode class, and returns the appropriate path for that node.
    ''' </summary>
    ''' <returns></returns>
    Public Property MeteorImageThumbnailPath As String
    ''' <summary>
    ''' This property is used to store the path to the meteor marked image file.  It is implemented by the MeteorNode class, and returns the appropriate path for that node.
    ''' </summary>
    ''' <returns></returns>
    Public Property MeteorMarkedImagePath As String
    ''' <summary>
    ''' This property is used to store the path to the meteor marked image thumbnail file.  It is implemented by the MeteorNode class, and returns the appropriate path for that node.
    ''' </summary>
    ''' <returns></returns>
    Public Property MeteorMarkedImageThumbnailPath As String
    ''' <summary>
    ''' This property is used to store the path to the meteor metadata file.  It is implemented by the MeteorNode class, and returns the appropriate path for that node.
    ''' </summary>
    ''' <returns></returns>
    Public Property MeteorMetadataPath As String

    Private _ty As VideoImageType

    Private _itype As String
    Private _dt As String

    ''' <summary>
    ''' This property is used to display the name of the node in the TreeView control.  It is implemented by the MeteorNode class, and returns the MeteorTime property.
    ''' </summary>
    ''' <returns></returns>
    Public Property Name As String Implements Inode.Name
        Get
            Return MeteorTime
        End Get
        Set(value As String)
            MeteorTime = value
        End Set
    End Property

    ''' <summary>
    ''' This property is used to determine what type of node it is (e.g. Year, Month, Day, VideoImage, Recap, Meteor).  It is implemented by the MeteorNode class, and returns "Meteor".
    ''' </summary>
    ''' <returns></returns>
    Property ItemType As String Implements Inode.ItemType
        Get
            Return _itype
        End Get
        Set(value As String)
            _itype = value
        End Set
    End Property

    ''' <summary>
    ''' This property is used to store the quality of the meteor image or video.  It is implemented by the MeteorNode class, and returns the appropriate quality for that node.  The Q property is used to determine which meteor image or video to display when there are multiple meteor images or videos for the same date and time.
    ''' </summary>
    ''' <returns></returns>
    Public Property Q As Integer Implements InodeQ.Q
        Get
            Return _q
        End Get
        Set(value As Integer)
            _q = value
        End Set
    End Property

    ''' <summary>
    ''' This property is used to store the date and time of the meteor event.  It is implemented by the MeteorNode class, and returns the appropriate date and time for that node.
    ''' </summary>
    ''' <returns></returns>
    Public Property DateTime As String
        Get
            Return _dt
        End Get
        Set(value As String)
            _dt = value
        End Set
    End Property

    ''' <summary>
    ''' This property is used to determine what type of node it is (e.g. Year, Month, Day, VideoImage, Recap, Meteor).  It is implemented by the MeteorNode class, and returns "Meteor".
    ''' </summary>
    ''' <returns></returns>
    Public Property Type As VideoImageType
        Get
            Return _ty
        End Get
        Set(value As VideoImageType)
            _ty = value
        End Set
    End Property

    ''' <summary>
    ''' This property is used to determine if the meteor image thumbnail exists.  It is implemented by the MeteorNode class, and returns True if the MeteorImageThumbnailPath property is not null or empty, and False otherwise.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MeteorImageThumbnailExists As Boolean
        Get
            Return Not String.IsNullOrEmpty(MeteorImageThumbnailPath)
        End Get
    End Property

    ''' <summary>
    ''' This property is used to determine if the meteor marked image thumbnail exists.  It is implemented by the MeteorNode class, and returns True if the MeteorMarkedImageThumbnailPath property is not null or empty, and False otherwise.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MeteorMarkedImageThumbnailExists As Boolean
        Get
            Return Not String.IsNullOrEmpty(MeteorMarkedImageThumbnailPath)
        End Get
    End Property
End Class