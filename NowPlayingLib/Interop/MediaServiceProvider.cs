using System.Runtime.InteropServices;

namespace NowPlayingLib.Interop
{
    /// <summary>
    /// Windows Media Player の <see cref="IWMPRemoteMediaServices"/> インターフェイスを実装します。
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    internal sealed class MediaServiceProvider : IWMPRemoteMediaServices
    {
        public string GetServiceType() => "Remote";

        public string GetApplicationName() =>  "NowPlayingLib";

        public object GetScriptableObject(out string name) => name = null;

        public string GetCustomUIMode() => null;
    }
}
