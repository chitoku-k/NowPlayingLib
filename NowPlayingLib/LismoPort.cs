using NowPlayingLib.Interop;
using SmpxKDDILib;
using SonyMediaPlayerXLib;
using System;

namespace NowPlayingLib
{
    /// <summary>
    /// LISMO Port (x-アプリ for LISMO) の機能を提供します。
    /// </summary>
    public class LismoPort : SonyMediaPlayerX
    {
        /// <summary>
        /// <see cref="NowPlayingLib.LismoPort"/> の新しいインスタンスを初期化します。
        /// </summary>
        public LismoPort() { }

        /// <summary>
        /// LISMO Port (x-アプリ for LISMO) の <see cref="SonyMediaPlayerXLib.ISmpxPlayControls"/> のインスタンスを取得します。
        /// </summary>
        /// <exception cref="System.TypeInitializationException"/>
        protected override ComWrapper<ISonyMediaPlayerX2> Player
        {
            get
            {
                try
                {
                    _player = _player ?? ComWrapper.Create<ISonyMediaPlayerX2>(new SmpxMediaPlayerKDDI());
                    IsDisposed = false;
                    return _player;
                }
                catch (TypeInitializationException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new TypeInitializationException(typeof(SmpxMediaPlayerKDDI).FullName, ex);
                }
            }
        }
    }
}
