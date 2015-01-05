namespace NowPlayingLib.Interop
{
    /// <summary>
    /// Indicates whether an object should be saved before closing.
    /// </summary>
    internal enum OLECLOSE
    {
        /// <summary>
        /// The object should be saved if it is dirty.
        /// </summary>
        SAVEIFDIRTY = 0,
        /// <summary>
        /// The object should not be saved, even if it is dirty. This flag is typically used when an object is being deleted.
        /// </summary>
        NOSAVE = 1,
        /// <summary>
        /// <para>If the object is dirty, the IOleObject::Close implementation should display a dialog box to let the end user determine whether to save the object.</para> 
        /// <para>However, if the object is in the running state but its user interface is invisible, the end user should not be prompted, and the close should be handled as if OLECLOSE_SAVEIFDIRTY had been specified.</para>
        /// </summary>
        PROMPTSAVE = 2
    }
}
