using System;
using System.Runtime.InteropServices;

namespace NowPlayingLib.Interop
{
    /// <summary>
    /// サービス オブジェクト、つまり、他のオブジェクトにカスタム サポートを提供するオブジェクトを取得するための機構を定義します。
    /// </summary>
    [ComImport]
    [GuidAttribute("6d5140c1-7436-11ce-8034-00aa006009fa")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IServiceProvider
    {
        /// <summary>
        /// <see cref="IServiceProvider"/> の実装によって公開されるサービスのファクトリとして実行します。
        /// </summary>
        /// <param name="guidService">要求されたサービスの一意の識別子。</param>
        /// <param name="riid">呼び出し元がサービスに受信するインターフェイスの一意の識別子。</param>
        /// <returns>要求されたサービスのインターフェイスのアドレスを指定する整数を返します。</returns>
        IntPtr QueryService(ref Guid guidService, ref Guid riid);
    }
}
