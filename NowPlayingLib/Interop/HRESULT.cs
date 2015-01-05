namespace NowPlayingLib.Interop
{
    /// <summary>
    /// HRESULT を定義します。
    /// </summary>
    internal enum HRESULT
    {
        /// <summary>
        /// S_OK
        /// </summary>
        S_OK = 0,
        /// <summary>
        /// E_NOINTERFACE
        /// </summary>
        E_NOINTERFACE = unchecked((int)0x80004002),
        /// <summary>
        /// E_NOTIMPL
        /// </summary>
        E_NOTIMPL = unchecked((int)0x80004001)
    }
}
