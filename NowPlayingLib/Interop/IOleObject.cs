using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace NowPlayingLib.Interop
{
    /// <summary>
    /// The IOleObject interface is the principal means by which an embedded object provides basic functionality to, and communicates with, its container.
    /// </summary>
    [ComImport]
    [ComVisible(true)]
    [Guid("00000112-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IOleObject
    {
        /// <summary>
        /// コンテナー中のクライアント サイトをコントロールに設定します。
        /// </summary>
        void SetClientSite(IOleClientSite pClientSite);

        /// <summary>
        /// コントロールのクライアント サイトを取得します。
        /// </summary>
        IOleClientSite GetClientSite();

        /// <summary>
        /// コンテナー アプリケーションとコンテナー ドキュメントの名前をコントロールに設定します。
        /// </summary>
        void SetHostNames(string szContainerApp, string szContainerObj);

        /// <summary>
        /// コントロールの状態を実行中から読み込み済みに変更します。
        /// </summary>
        void Close(OLECLOSE dwSaveOption);

        /// <summary>
        /// コントロールに名称 (モニカー) を設定します。 ATL の実装は、E_NOTIMPL を返します。
        /// </summary>
        void SetMoniker(uint dwWhichMoniker, object pmk);

        /// <summary>
        /// コントロールの名称 (モニカー) を取得します。 ATL の実装は、E_NOTIMPL を返します。
        /// </summary>
        object GetMoniker(uint dwAssign, uint dwWhichMoniker);

        /// <summary>
        /// 選択されたデータを使用してコントロールを初期化します。 ATL の実装は、E_NOTIMPL を返します。
        /// </summary>
        void InitFromData(object pDataObject, bool fCreation, uint dwReserved);

        /// <summary>
        /// クリップボードからデータを取得します。 ATL の実装は、E_NOTIMPL を返します。
        /// </summary>
        object GetClipboardData(uint dwReserved);

        /// <summary>
        /// 列挙されるアクションのいずれか 1 つを実行するようにコントロールに命令します。
        /// </summary>
        void DoVerb(uint iVerb, uint lpmsg, object pActiveSite, uint lindex, uint hwndParent, uint lprcPosRect);

        /// <summary>
        /// コントロールのアクションを列挙します。
        /// </summary>
        object EnumVerbs();

        /// <summary>
        /// コントロールを更新します。 ATL の実装は、S_OK を返します。
        /// </summary>
        void Update();

        /// <summary>
        /// コントロールが最新かどうかをチェックします。 ATL の実装は、S_OK を返します。
        /// </summary>
        int IsUpToDate();

        /// <summary>
        /// コントロールのクラス識別子を取得します。
        /// </summary>
        Guid GetUserClassID();

        /// <summary>
        /// コントロールのユーザー タイプ名を取得します。
        /// </summary>
        string GetUserType(uint dwFormOfType);

        /// <summary>
        /// コントロールの表示領域のサイズを設定します。
        /// </summary>
        void SetExtent(uint dwDrawAspect, object psizel);

        /// <summary>
        /// コントロールの表示領域のサイズを取得します。
        /// </summary>
        object GetExtent(uint dwDrawAspect);

        /// <summary>
        /// コントロールとの間にアドバイザリ コネクションを構築します。
        /// </summary>
        uint Advise(object pAdvSink);

        /// <summary>
        /// コントロールとの間のアドバイザリ コネクションを削除します。
        /// </summary>
        void Unadvise(uint dwConnection);

        /// <summary>
        /// コントロールのアドバイザリ コネクションを列挙します。
        /// </summary>
        object EnumAdvise();

        /// <summary>
        /// コントロールのステータスを取得します。
        /// </summary>
        uint GetMiscStatus(int dwAspect);

        /// <summary>
        /// 推奨する配色をコントロール アプリケーションに設定します。 ATL の実装は、E_NOTIMPL を返します。
        /// </summary>
        void SetColorScheme(object pLogpal);
    }
}
