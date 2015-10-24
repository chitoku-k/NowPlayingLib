using System;
using System.Runtime.InteropServices;

namespace NowPlayingLib.Interop
{
    /// <summary>
    /// Windows Media Player をホストします。
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    internal sealed class RemoteHost : IServiceProvider, IOleClientSite
    {
        public IntPtr QueryService(ref Guid guidService, ref Guid riid) => Marshal.GetComInterfaceForObject(new MediaServiceProvider(), typeof(IWMPRemoteMediaServices));

        public object GetContainer() => HRESULT.E_NOTIMPL;

        public object GetMoniker(uint dwAssign, uint dwWhichMoniker) => HRESULT.E_NOTIMPL;

        public void OnShowWindow(bool fShow) { }

        public void RequestNewObjectLayout() { }

        public void SaveObject() { }

        public void ShowObject() { }
    }
}
