using System;

namespace NowPlayingLib.Win32
{
    [Flags]
    internal enum CACHE_ENTRY : uint
    {
        NORMAL = 0x00000001,
        STICKY = 0x00000004,
        EDITED = 0x00000008,
        TRACK_OFFLINE = 0x00000010,
        TRACK_ONLINE = 0x00000020,
        SPARSE = 0x00010000,
        COOKIE = 0x00100000,
        URLHISTORY = 0x00200000
    }
}
