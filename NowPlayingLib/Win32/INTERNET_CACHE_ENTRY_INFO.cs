using System;
using System.Runtime.InteropServices;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

namespace NowPlayingLib.Win32
{
    /// <summary>
    /// Contains information about an entry in the Internet cache.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct INTERNET_CACHE_ENTRY_INFO
    {
        /// <summary>
        /// <para>Size of this structure, in bytes.</para>
        /// <para>This value can be used to help determine the version of the cache system.</para>
        /// </summary>
        public uint StructSize;

        /// <summary>
        /// String that contains the URL name.
        /// </summary>
        public string SourceUrlName;

        /// <summary>
        /// String that contains the local file name. 
        /// </summary>
        public string LocalFileName;

        /// <summary>
        /// A bitmask indicating the type of cache entry and its properties.
        /// </summary>
        public CACHE_ENTRY CacheEntryType;

        /// <summary>
        /// Current number of WinINEet callers using the cache entry.
        /// </summary>
        public uint UseCount;

        /// <summary>
        /// Number of times the cache entry was retrieved.
        /// </summary>
        public uint HitRate;

        /// <summary>
        /// Low-order portion of the file size, in bytes.
        /// </summary>
        public uint SizeLow;

        /// <summary>
        /// High-order portion of the file size, in bytes.
        /// </summary>
        public uint SizeHigh;

        /// <summary>
        /// FILETIME structure that contains the last modified time of this URL.
        /// </summary>
        public FILETIME LastModifiedTime;

        /// <summary>
        /// FILETIME structure that contains the expiration time of this file.
        /// </summary>
        public FILETIME ExpireTime;

        /// <summary>
        /// FILETIME structure that contains the last accessed time.
        /// </summary>
        public FILETIME LastAccessTime;

        /// <summary>
        /// FILETIME structure that contains the last time the cache was synchronized.
        /// </summary>
        public FILETIME LastSyncTime;

        /// <summary>
        /// Pointer to a buffer that contains the header information.
        /// </summary>
        public string HeaderInfo;

        /// <summary>
        /// Size of the lpHeaderInfo buffer.
        /// </summary>
        public uint HeaderInfoSize;

        /// <summary>
        /// String that contains the file name extension used to retrieve the data as a file.
        /// </summary>
        public string FileExtension;

        /// <summary>
        /// Exemption time from the last accessed time.
        /// </summary>
        public uint ExemptDelta;
    }
}

