using NowPlayingLib.Interop;
using SmpxSonyLib;
using SonyMediaPlayerXLib;
using System;

namespace NowPlayingLib
{
    /// <summary>
    /// x-アプリ の機能を提供します。
    /// </summary>
    public class XApplication : SonyMediaPlayerX
    {
        /// <summary>
        /// <see cref="NowPlayingLib.XApplication"/> の新しいインスタンスを初期化します。
        /// </summary>
        public XApplication() { }

        /// <summary>
        /// x-アプリ の <see cref="SonyMediaPlayerXLib.ISonyMediaPlayerX2"/> のインスタンスを取得します。
        /// </summary>
        /// <exception cref="System.TypeInitializationException"/>
        protected override ComWrapper<ISonyMediaPlayerX2> Player
        {
            get
            {
                try
                {
                    _player = _player ?? ComWrapper.Create<ISonyMediaPlayerX2>(new SmpxMediaPlayerSony());
                    IsDisposed = false;
                    return _player;
                }
                catch (TypeInitializationException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new TypeInitializationException(typeof(SmpxMediaPlayerSony).FullName, ex);
                }
            }
        }
    }
}
