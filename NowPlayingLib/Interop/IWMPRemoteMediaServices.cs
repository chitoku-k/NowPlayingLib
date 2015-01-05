using System;
using System.Runtime.InteropServices;

namespace NowPlayingLib.Interop
{
    /// <summary>
    /// The IWMPRemoteMediaServices interface includes methods that provide services to Windows Media Player from a program that hosts the Player control.
    /// </summary>
    [ComImport]
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("CBB92747-741F-44fe-AB5B-F1A48F3B2A59")]
    internal interface IWMPRemoteMediaServices
    {
        /// <summary>
        /// Called by Windows Media Player to determine whether a host program wants to run its embedded control remotely.
        /// </summary>
        [return: MarshalAs(UnmanagedType.BStr)]
        string GetServiceType();

        /// <summary>
        /// Called by Windows Media Player to retrieve the name of the program that is hosting the remoted control.
        /// </summary>
        [return: MarshalAs(UnmanagedType.BStr)]
        string GetApplicationName();

        /// <summary>
        /// Called by Windows Media Player to retrieve a name and interface pointer for an object that can be called from the script code within a skin.
        /// </summary>
        [return: MarshalAs(UnmanagedType.IDispatch)]
        object GetScriptableObject([MarshalAs(UnmanagedType.BStr)] out string name);

        /// <summary>
        /// Called by Windows Media Player to retrieve the location of a skin file to apply to the Player control.
        /// </summary>
        [return: MarshalAs(UnmanagedType.BStr)]
        string GetCustomUIMode();
    }
}
