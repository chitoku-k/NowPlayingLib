using System;

namespace NowPlayingLib
{
    /// <summary>
    /// 音楽プレーヤーの状態が変更されたことをクライアントに通知します。
    /// </summary>
    public interface INotifyPlayerStateChanged
    {
        /// <summary>
        /// 音楽プレーヤーで再生中の曲が変更されたときに発生します。
        /// </summary>
        event CurrentMediaChangedEventHandler CurrentMediaChanged;

        /// <summary>
        /// 音楽プレーヤーが終了された時に発生します。
        /// </summary>
        event EventHandler Closed;
    }
}
