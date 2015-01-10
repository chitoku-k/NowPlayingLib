using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

namespace NowPlayingLib.Win32
{
    internal class NativeMethods
    {
        [DllImport("wininet.dll", SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetUrlCacheEntryInfo([MarshalAs(UnmanagedType.LPStr)] string lpszUrlName, IntPtr lpCacheEntryInfo, out uint lpdwCacheEntryInfoBufferSize);

        [DllImport("wininet.dll", SetLastError = true, EntryPoint = "DeleteUrlCacheEntry", BestFitMapping = false, ThrowOnUnmappableChar = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteUrlCacheEntryFunc([MarshalAs(UnmanagedType.LPStr)] string lpszUrlName);

        /// <summary>
        /// Retrieves information about a cache entry.
        /// </summary>
        /// <param name="url">String that contains the name of the cache entry.</param>
        /// <exception cref="System.IO.FileNotFoundException"/>
        /// <exception cref="System.ComponentModel.Win32Exception"/>
        /// <returns>INTERNET_CACHE_ENTRY_INFO structure that receives information about the cache entry.</returns>
        internal static INTERNET_CACHE_ENTRY_INFO GetUrlCacheEntryInfo(string url)
        {
            const int ERROR_FILE_NOT_FOUND = 0x00000002;

            var buffer = IntPtr.Zero;
            uint structSize;
            GetUrlCacheEntryInfo(url, buffer, out structSize);
            if (Marshal.GetLastWin32Error() == ERROR_FILE_NOT_FOUND)
            {
                throw new FileNotFoundException();
            }
            
            try
            {
                buffer = Marshal.AllocHGlobal((int)structSize);
                if (structSize > 0 && GetUrlCacheEntryInfo(url, buffer, out structSize))
                {
                    return (INTERNET_CACHE_ENTRY_INFO)Marshal.PtrToStructure(buffer, typeof(INTERNET_CACHE_ENTRY_INFO));
                }
                else
                {
                    throw new Win32Exception();
                }
            }
            finally
            {
                if (buffer != null && buffer.ToInt32() > 0)
                {
                    Marshal.FreeHGlobal(buffer);
                }
            }
        }

        /// <summary>
        /// Removes the file associated with the source name from the cache, if the file exists.
        /// </summary>
        /// <exception cref="System.ComponentModel.Win32Exception"/>
        /// <param name="url">String that contains the name of the source that corresponds to the cache entry.</param>
        internal static void DeleteUrlCacheEntry(string url)
        {
            if (!DeleteUrlCacheEntryFunc(url))
            {
                throw new Win32Exception();
            }
        }
    }
}
