namespace NowPlayingLib.Interop
{
    /// <summary>
    /// The Windows Media Player Attribute Reference documents metadata attributes that are of interest to Windows Media Player SDK developers.
    /// </summary>
    public class AudioAttributes
    {
        /// <summary>
        /// The AcquisitionTime attribute is the date and time the item was added to the library.
        /// </summary>
        public const string AcquisitionTime = "AcquisitionTime";

        /// <summary>
        /// The AcquisitionTimeDay attribute is the day part of the date and time the item was added to the library.
        /// </summary>
        public const string AcquisitionTimeDay = "AcquisitionTimeDay";

        /// <summary>
        /// The AcquisitionTimeMonth attribute is the month part of the date and time the item was added to the library.
        /// </summary>
        public const string AcquisitionTimeMonth = "AcquisitionTimeMonth";

        /// <summary>
        /// The AcquisitionTimeYear attribute is the year part of the date and time the item was added to the library.
        /// </summary>
        public const string AcquisitionTimeYear = "AcquisitionTimeYear";

        /// <summary>
        /// The AcquisitionTimeYearMonth attribute is the year and month part of the date and time the item was added to the library.
        /// </summary>
        public const string AcquisitionTimeYearMonth = "AcquisitionTimeYearMonth";

        /// <summary>
        /// The AcquisitionTimeYearMonthDay attribute is the year, month, and day part of the date and time the item was added to the library.
        /// </summary>
        public const string AcquisitionTimeYearMonthDay = "AcquisitionTimeYearMonthDay";

        /// <summary>
        /// The AlbumID attribute is a unique identifier for the album.
        /// </summary>
        public const string AlbumId = "AlbumID";

        /// <summary>
        /// The AlbumIDAlbumArtist attribute is a unique identifier for the album.
        /// </summary>
        public const string AlbumIdAlbumArtist = "AlbumIDAlbumArtist";

        /// <summary>
        /// The AudioFormat attribute is a FourCC code that identifies the audio format of the item.
        /// </summary>
        public const string AudioFormat = "AudioFormat";

        /// <summary>
        /// The Author attribute is the name of a media artist or actor associated with the content.
        /// </summary>
        public const string Author = "Author";

        /// <summary>
        /// The AverageLevel attribute is a 16-bit amplitude value indicating the average volume level.
        /// </summary>
        public const string AverageLevel = "AverageLevel";

        /// <summary>
        /// The Bitrate attribute is the bit rate of the item, in bits per second.
        /// </summary>
        public const string Bitrate = "Bitrate";

        /// <summary>
        /// The BuyNow attribute is a PARAM value for use in commercial interactions.
        /// </summary>
        public const string BuyNow = "BuyNow";

        /// <summary>
        /// The BuyTickets attribute is a PARAM value for use in commercial interactions.
        /// </summary>
        public const string BuyTickets = "BuyTickets";

        /// <summary>
        /// This attribute is undocumented.
        /// </summary>
        public const string CanonicalFileType = "CanonicalFileType";

        /// <summary>
        /// The ContentDistributorDuration attribute is the playing duration of the item, in seconds.
        /// </summary>
        public const string ContentDistributorDuration = "ContentDistributorDuration";

        /// <summary>
        /// The Copyright attribute is the copyright message for the item.
        /// </summary>
        public const string Copyright = "Copyright";

        /// <summary>
        /// This attribute is undocumented.
        /// </summary>
        public const string DefaultDate = "DefaultDate";

        /// <summary>
        /// The DisplayArtist attribute is the name of the artist that is displayed for an item.
        /// </summary>
        public const string DisplayArtist = "DisplayArtist";

        /// <summary>
        /// The DRMIndividualizedVersion indicates the digital rights management (DRM) version that the digital media file requires.
        /// </summary>
        public const string DrmIndividualizedVersion = "DRMIndividualizedVersion";

        /// <summary>
        /// The DRMKeyID attribute identifies the media usage rights for content that is protected using digital rights management (DRM).
        /// </summary>
        public const string DrmKeyID = "DRMKeyID";

        /// <summary>
        /// The Duration attribute is the playing duration of the item, in seconds.
        /// </summary>
        public const string Duration = "Duration";

        /// <summary>
        /// The FileSize attribute is the size of the file in bytes.
        /// </summary>
        public const string FileSize = "FileSize";

        /// <summary>
        /// The FileType attribute is the three-letter file name extension, such as "wma" or "mp3".
        /// </summary>
        public const string FileType = "FileType";

        /// <summary>
        /// This attribute is undocumented.
        /// </summary>
        public const string HmeAlbumTitle = "HMEAlbumTitle";

        /// <summary>
        /// The Is_Protected attribute indicates whether the content is protected using digital rights management (DRM).
        /// </summary>
        public const string IsProtected = "Is_Protected";

        /// <summary>
        /// This attribute is undocumented.
        /// </summary>
        public const string IsTrusted = "Is_Trusted";

        /// <summary>
        /// This attribute is undocumented.
        /// </summary>
        public const string LinkedFileUrl = "LinkedFileURL";

        /// <summary>
        /// The MediaType attribute is the type of content in the item.
        /// </summary>
        public const string MediaType = "MediaType";

        /// <summary>
        /// The MoreInfo attribute is a PARAM value for use in commercial interactions.
        /// </summary>
        public const string MoreInfo = "MoreInfo";

        /// <summary>
        /// The PeakValue attribute is a 16-bit amplitude value indicating the peak volume level.
        /// </summary>
        public const string PeakValue = "PeakValue";

        /// <summary>
        /// The ProviderLogoURL attribute is the address of the logo of the provider of the attribute values.
        /// </summary>
        public const string ProviderLogoUrl = "ProviderLogoURL";

        /// <summary>
        /// The ProviderURL attribute is the address of the home page of the provider of the attribute values.
        /// </summary>
        public const string ProviderUrl = "ProviderURL";

        /// <summary>
        /// The RecordingTime attribute is the date and time of the original recording, for items where this date is different from the release date.
        /// </summary>
        public const string RecordingTime = "RecordingTime";

        /// <summary>
        /// The RecordingTimeDay attribute is the day part of the date of the original recording, for items where this date is different from the release date.
        /// </summary>
        public const string RecordingTimeDay = "RecordingTimeDay";

        /// <summary>
        /// The RecordingTimeMonth attribute is the month part of the date of the original recording, for items where this date is different from the release date.
        /// </summary>
        public const string RecordingTimeMonth = "RecordingTimeMonth";

        /// <summary>
        /// The RecordingTimeYear attribute is the year part of the date of the original recording, for items where this date is different from the release date.
        /// </summary>
        public const string RecordingTimeYear = "RecordingTimeYear";

        /// <summary>
        /// The RecordingTimeYearMonth attribute is the year and month part of the date of the original recording, for items where this date is different from the release date.
        /// </summary>
        public const string RecordingTimeYearMonth = "RecordingTimeYearMonth";

        /// <summary>
        /// The RecordingTimeYearMonthDay attribute is the date of the original recording (without the time of day), for items where this date is different from the release date.
        /// </summary>
        public const string RecordingTimeYearMonthDay = "RecordingTimeYearMonthDay";

        /// <summary>
        /// The ReleaseDate attribute is the date of the original release of the item.
        /// </summary>
        public const string ReleaseDate = "ReleaseDate";

        /// <summary>
        /// The ReleaseDateDay attribute is the day part of the date of the original release of the item.
        /// </summary>
        public const string ReleaseDateDay = "ReleaseDateDay";

        /// <summary>
        /// The ReleaseDateMonth attribute is the month part of the date of the original release of the item.
        /// </summary>
        public const string ReleaseDateMonth = "ReleaseDateMonth";

        /// <summary>
        /// The ReleaseDateYear attribute is the year part of the date of the original release of the item.
        /// </summary>
        public const string ReleaseDateYear = "ReleaseDateYear";

        /// <summary>
        /// The ReleaseDateYearMonth attribute is the year and month part of the date of the original release of the item.
        /// </summary>
        public const string ReleaseDateYearMonth = "ReleaseDateYearMonth";

        /// <summary>
        /// The ReleaseDateYearMonthDay attribute is the year, month, and day part of the date of the original release of the item.
        /// </summary>
        public const string ReleaseDateYearMonthDay = "ReleaseDateYearMonthDay";

        /// <summary>
        /// The RequestState attribute is the media information request state.
        /// </summary>
        public const string RequestState = "RequestState";

        /// <summary>
        /// The SourceURL attribute is the address of the item.
        /// </summary>
        public const string SourceUrl = "SourceURL";

        /// <summary>
        /// This attribute is undocumented.
        /// </summary>
        public const string Streams = "Streams";

        /// <summary>
        /// The SyncState attribute is a string representation of a 32-bit value that Windows Media Player uses when it synchronizes playlists with portable devices.
        /// </summary>
        public const string SyncState = "SyncState";

        /// <summary>
        /// This attribute is undocumented.
        /// </summary>
        public const string SyncState2 = "SyncState2";

        /// <summary>
        /// The Title attribute is the title of the content.
        /// </summary>
        public const string Title = "Title";

        /// <summary>
        /// The TrackingID attribute is 128-bit Globally Unique Identifier (GUID) that Windows Media Player uses to identify a media item.
        /// </summary>
        public const string TrackingId = "TrackingID";

        /// <summary>
        /// This attribute is undocumented.
        /// </summary>
        public const string Type = "service";

        /// <summary>
        /// The UserCustom1 attribute is arbitrary text.
        /// </summary>
        public const string UserCustom1 = "UserCustom1";

        /// <summary>
        /// The UserCustom2 attribute is user defined text.
        /// </summary>
        public const string UserCustom2 = "UserCustom2";

        /// <summary>
        /// The UserEffectiveRating attribute is the rating computed by Windows Media Player based on how often the item has been played.
        /// </summary>
        public const string UserEffectiveRating = "UserEffectiveRating";

        /// <summary>
        /// The UserLastPlayedTime attribute is the date and time the item was most recently played.
        /// </summary>
        public const string UserLastPlayedTime = "UserLastPlayedTime";

        /// <summary>
        /// The UserPlayCount attribute is the number of times the item has been played.
        /// </summary>
        public const string UserPlayCount = "UserPlayCount";

        /// <summary>
        /// The UserPlaycountAfternoon attribute is the number of times the item has been played between 12:00 and 17:00 local time.
        /// </summary>
        public const string UserPlaycountAfternoon = "UserPlaycountAfternoon";

        /// <summary>
        /// The UserPlaycountEvening attribute is the number of times the item has been played between 17:00 and 22:00 local time.
        /// </summary>
        public const string UserPlaycountEvening = "UserPlaycountEvening";

        /// <summary>
        /// The UserPlaycountMorning attribute is the number of times the item has been played between 06:00 and 12:00 local time.
        /// </summary>
        public const string UserPlaycountMorning = "UserPlaycountMorning";

        /// <summary>
        /// The UserPlaycountNight attribute is the number of times the item has been played between 22:00 and 06:00 local time.
        /// </summary>
        public const string UserPlaycountNight = "UserPlaycountNight";

        /// <summary>
        /// The UserPlaycountWeekday attribute is the number of times the item has been played on a Monday, Tuesday, Wednesday, Thursday, or Friday.
        /// </summary>
        public const string UserPlaycountWeekday = "UserPlaycountWeekday";

        /// <summary>
        /// The UserPlaycountWeekend attribute is the number of times the item has been played on a Saturday or Sunday.
        /// </summary>
        public const string UserPlaycountWeekend = "UserPlaycountWeekend";

        /// <summary>
        /// The UserRating attribute is the rating specified by the user in the library.
        /// </summary>
        public const string UserRating = "UserRating";

        /// <summary>
        /// The UserServiceRating attribute is reserved for future use.
        /// </summary>
        public const string UserServiceRating = "UserServiceRating";

        /// <summary>
        /// The WM/AlbumArtist attribute is the name of the primary artist for the album.
        /// </summary>
        public const string AlbumArtist = "WM/AlbumArtist";

        /// <summary>
        /// The WM/AlbumTitle attribute is the title of the album on which the content was originally released.
        /// </summary>
        public const string AlbumTitle = "WM/AlbumTitle";

        /// <summary>
        /// The WM/Category attribute is the category of the content.
        /// </summary>
        public const string Category = "WM/Category";

        /// <summary>
        /// The WM/Composer attribute is the name of the composer of the music.
        /// </summary>
        public const string Composer = "WM/Composer";

        /// <summary>
        /// The WM/Conductor attribute is the name of the conductor of the music.
        /// </summary>
        public const string Conductor = "WM/Conductor";

        /// <summary>
        /// The WM/ContentDistributor attribute is the name of the distributor of the item.
        /// </summary>
        public const string ContentDistributor = "WM/ContentDistributor";

        /// <summary>
        /// The WM/ContentGroupDescription attribute is the description of the content group, which is a collection of media items such as a CD boxed set.
        /// </summary>
        public const string ContentGroupDescription = "WM/ContentGroupDescription";

        /// <summary>
        /// The WM/EncodingTime attribute is the date and time at which the content was encoded.
        /// </summary>
        public const string EncodingTime = "WM/EncodingTime";

        /// <summary>
        /// The WM/Genre attribute is the genre of the content.
        /// </summary>
        public const string Genre = "WM/Genre";

        /// <summary>
        /// The WM/InitialKey attribute is the initial key of the music.
        /// </summary>
        public const string InitialKey = "WM/InitialKey";

        /// <summary>
        /// The WM/Language attribute is the language of the item.
        /// </summary>
        public const string Language = "WM/Language";

        /// <summary>
        /// The WM/Lyrics attribute is the lyrics of the content.
        /// </summary>
        public const string Lyrics = "WM/Lyrics";

        /// <summary>
        /// The WM/MCDI attribute is the music CD identifier of the CD from which the file or track was copied.
        /// </summary>
        public const string Mcdi = "WM/MCDI";

        /// <summary>
        /// The WM/MediaClassPrimaryID attribute is a GUID specifying one of the primary media classes: music, non-music audio, video, or other.
        /// </summary>
        public const string MediaClassPrimaryId = "WM/MediaClassPrimaryID";

        /// <summary>
        /// The WM/MediaClassSecondaryID attribute is a GUID specifying the secondary media class, which is a subclass of the primary media class.
        /// </summary>
        public const string MediaClassSecondaryId = "WM/MediaClassSecondaryID";

        /// <summary>
        /// The WM/Mood attribute is a category name for the mood of the content.
        /// </summary>
        public const string Mood = "WM/Mood";

        /// <summary>
        /// The WM/ParentalRating attribute is the parental rating of the content.
        /// </summary>
        public const string ParentalRating = "WM/ParentalRating";

        /// <summary>
        /// The WM/PartOfSet attribute is the part number and the total number of parts of the set to which the item belongs.
        /// </summary>
        public const string PartOfSet = "WM/PartOfSet";

        /// <summary>
        /// The WM/Period attribute is the period of the content.
        /// </summary>
        public const string Period = "WM/Period";

        /// <summary>
        /// The WM/Picture attribute contains a picture related to the content.
        /// </summary>
        public const string Picture = "WM/Picture";

        /// <summary>
        /// The WM/ProtectionType attribute is the type of protection used on the content.
        /// </summary>
        public const string ProtectionType = "WM/ProtectionType";

        /// <summary>
        /// The WM/Provider attribute is the name of the provider of the attribute values.
        /// </summary>
        public const string Provider = "WM/Provider";

        /// <summary>
        /// The WM/ProviderRating attribute is the rating of the item assigned by the provider of the attribute values.
        /// </summary>
        public const string ProviderRating = "WM/ProviderRating";

        /// <summary>
        /// The WM/ProviderStyle attribute is the style of the item assigned by the provider of the attribute values.
        /// </summary>
        public const string ProviderStyle = "WM/ProviderStyle";

        /// <summary>
        /// The WM/Publisher attribute is the name of the company that published the content.
        /// </summary>
        public const string Publisher = "WM/Publisher";

        /// <summary>
        /// The WM/SubscriptionContentID attribute is the subscription content identifier.
        /// </summary>
        public const string SubscriptionContentId = "WM/SubscriptionContentID";

        /// <summary>
        /// The WM/SubTitle attribute is the subtitle of the content.
        /// </summary>
        public const string SubTitle = "WM/SubTitle";

        /// <summary>
        /// This attribute is undocumented.
        /// </summary>
        public const string Track = "WM/Track";

        /// <summary>
        /// The WM/TrackNumber attribute is the track number of the item on the album on which it was originally released.
        /// </summary>
        public const string TrackNumber = "WM/TrackNumber";

        /// <summary>
        /// The WM/UniqueFileIdentifier attribute is a string that uniquely identifies the item.
        /// </summary>
        public const string UniqueFileIdentifier = "WM/UniqueFileIdentifier";

        /// <summary>
        /// The WM/WMCollectionGroupID attribute is a GUID identifying the group containing the collection to which the item belongs.
        /// </summary>
        public const string WMCollectionGroupId = "WM/WMCollectionGroupID";

        /// <summary>
        /// The WM/WMCollectionID attribute is a GUID identifying the collection to which the item belongs.
        /// </summary>
        public const string WMCollectionId = "WM/WMCollectionID";

        /// <summary>
        /// The WM/WMContentID attribute is a GUID identifying the content of the item.
        /// </summary>
        public const string WMContentId = "WM/WMContentID";

        /// <summary>
        /// The WM/Writer attribute is the name of the writer who wrote the words of the content.
        /// </summary>
        public const string Writer = "WM/Writer";

        /// <summary>
        /// The WM/Year attribute is the year the content was published.
        /// </summary>
        public const string Year = "WM/Year";

        /// <summary>
        /// This attribute is undocumented.
        /// </summary>
        public const string WMServerVersion = "WMServerVersion";
    }
}
