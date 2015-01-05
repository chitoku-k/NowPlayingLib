using System;
using System.Runtime.InteropServices;

namespace NowPlayingLib.Interop
{
    /// <summary>
    /// Gives an embedded object information about resources provided by its container such as the location and extent of its display site, its moniker, and its user interface.
    /// </summary>
    [ComImport]
    [ComVisible(true)]
    [Guid("00000118-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IOleClientSite
    {
        /// <summary>
        /// Saves the embedded object associated with the client site.
        /// </summary>
        void SaveObject();
      
        /// <summary>
        /// <para>Retrieves a moniker for the object's client site.</para>
        /// <para>An object can force the assignment of its own or its container's moniker by specifying a value for dwAssign.</para>
        /// </summary>
        [return: MarshalAs(UnmanagedType.Interface)]
        object GetMoniker(uint dwAssign, uint dwWhichMoniker);

        /// <summary>
        /// Retrieves a pointer to the object's container.
        /// </summary>
        [return: MarshalAs(UnmanagedType.Interface)]
        object GetContainer();

        /// <summary>
        /// Asks a container to display its object to the user.
        /// </summary>
        void ShowObject();

        /// <summary>
        /// Notifies a container when an embedded object's window is about to become visible or invisible.
        /// </summary>
        void OnShowWindow(bool fShow);

        /// <summary>
        /// Asks a container to resize the display site for embedded objects.
        /// </summary>
        void RequestNewObjectLayout();
    }
}
