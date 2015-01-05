﻿using NowPlayingLib.Interop;
using SonyMediaPlayerXLib;
using SonyVzCs;
using System;
using System.Threading.Tasks;

namespace NowPlayingLib
{
    /// <summary>
    /// SonyMediaPlayerX の基本実装を提供します。これは <c>abstract</c> クラスです。
    /// </summary>
    public abstract class SonyMediaPlayerX : MediaPlayerBase
    {
        /// <summary>
        /// SonyMediaPlayerX の <see cref="SonyMediaPlayerXLib.ISonyMediaPlayerX2"/> への COM ラッパーの静的フィールド。
        /// </summary>
        protected static ComWrapper<ISonyMediaPlayerX2> _player;
        private static ComWrapper<ISmpxPlayControls> _controls;

        /// <summary>
        /// SonyMediaPlayerX の <see cref="SonyMediaPlayerXLib.ISonyMediaPlayerX2"/> への COM ラッパーを取得します。
        /// </summary>
        /// <exception cref="System.TypeInitializationException"/>
        protected abstract ComWrapper<ISonyMediaPlayerX2> Player { get; }

        /// <summary>
        /// SonyMediaPlayerX の <see cref="SonyMediaPlayerXLib.ISmpxPlayControls"/> への COM ラッパーを取得します。
        /// </summary>
        /// <exception cref="System.TypeInitializationException"/>
        protected ComWrapper<ISmpxPlayControls> Controls
        {
            get { return _controls ?? (_controls = ComWrapper.Create(Player.Object.playControls)); }
        }

        /// <summary>
        /// 非同期操作として現在 SonyMediaPlayerX で再生している曲を取得します。
        /// </summary>
        /// <para>非同期操作を表すタスク オブジェクト。</para>
        /// <para>タスク オブジェクトの <c>Result</c> プロパティは、<see cref="NowPlayingLib.MediaItem"/> を返します。</para>
        public override Task<MediaItem> GetCurrentMedia()
        {
            if (this.PlayerState == PlayerState.Stopped)
            {
                return null;
            }

            using (var currentItem = ComWrapper.Create((ISmpxMediaDescriptor2)Controls.Object.currentItem))
            {
                return Task.FromResult(new MediaItem
                {
                    Album = currentItem.Object.packageTitle,
                    AlbumArtist = currentItem.Object.artist,
                    Artist = currentItem.Object.artist,
                    Duration = TimeSpan.FromSeconds(currentItem.Object.duration),
                    Genre = currentItem.Object.genre,
                    Name = currentItem.Object.title,
                    // 例外発生: System.Runtime.InteropServices.COMException
                    // FileInfo = GetFileInfo(currentItem.Object.filePath),
                });
            }
        }

        /// <summary>
        /// SonyMediaPlayerX に関連付けられたアンマネージ リソースを解放します。
        /// </summary>
        /// <param name="disposing">明示的な破棄要求元から呼び出された場合は true に設定します。それ以外の場合は false。</param>
        protected override void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                _controls.Dispose();
                _player.Dispose();
            }

            IsDisposed = true;
        }

        /// <summary>
        /// SonyMediaPlayerX の現在の再生状態を取得します。
        /// </summary>
        public override PlayerState PlayerState
        {
            get
            {
                switch ((VzPlayStateEnum)Controls.Object.playState - 1)
                {
                    case VzPlayStateEnum.vzPlayState_FastForward:
                    case VzPlayStateEnum.vzPlayState_PlayingFastForward:
                    case VzPlayStateEnum.vzPlayState_PlayingSlowForward:
                    case VzPlayStateEnum.vzPlayState_StepForward:
                        return PlayerState.FastForward;
                    case VzPlayStateEnum.vzPlayState_Paused:
                        return PlayerState.Paused;
                    case VzPlayStateEnum.vzPlayState_Playing:
                        return PlayerState.Playing;
                    case VzPlayStateEnum.vzPlayState_PlayingReverse:
                    case VzPlayStateEnum.vzPlayState_PlayingFastReverse:
                    case VzPlayStateEnum.vzPlayState_PlayingSlowReverse:
                    case VzPlayStateEnum.vzPlayState_StepReverse:
                        return PlayerState.PlayingReverse;
                    case VzPlayStateEnum.vzPlayState_Rewind:
                        return PlayerState.Rewind;
                    case VzPlayStateEnum.vzPlayState_Stopped:
                        return PlayerState.Stopped;
                    default:
                        return PlayerState.Unknown;
                }
            }
        }

        /// <summary>
        /// SonyMediaPlayerX の現在の再生位置を取得または設定します。
        /// </summary>
        public override TimeSpan CurrentPosition
        {
            get { return TimeSpan.FromMilliseconds(Controls.Object.currentPosition); }
            set { Controls.Object.currentPosition = (int)value.TotalMilliseconds; }
        }

        /// <summary>
        /// SonyMediaPlayerX で再生を実行します。
        /// </summary>
        public override void Play()
        {
            Controls.Object.play();
        }

        /// <summary>
        /// SonyMediaPlayerX で早送りを実行します。
        /// </summary>
        public override void FastForward()
        {
            Controls.Object.fastForward();
        }

        /// <summary>
        /// SonyMediaPlayerX で巻戻しを実行します。
        /// </summary>
        public override void Rewind()
        {
            Controls.Object.fastReverse();
        }

        /// <summary>
        /// SonyMediaPlayerX で停止を実行します。
        /// </summary>
        public override void Stop()
        {
            Controls.Object.stop();
        }

        /// <summary>
        /// SonyMediaPlayerX で一時停止を実行します。
        /// </summary>
        public override void Pause()
        {
            Controls.Object.pause();
        }

        /// <summary>
        /// SonyMediaPlayerX で次の曲の再生を実行します。
        /// </summary>
        public override void NextTrack()
        {
            Controls.Object.next();
        }

        /// <summary>
        /// SonyMediaPlayerX で前の曲の再生を実行します。
        /// </summary>
        public override void PreviousTrack()
        {
            Controls.Object.previous();
        }
    }
}