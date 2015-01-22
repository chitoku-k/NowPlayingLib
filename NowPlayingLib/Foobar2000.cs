using Foobar2000;
using Foobar2000Helper;
using NowPlayingLib.Helpers;
using NowPlayingLib.Interop;
using System;
using System.Threading.Tasks;

namespace NowPlayingLib
{
    /// <summary>
    /// foobar2000 の機能を提供します。
    /// </summary>
    public class Foobar2000 : MediaPlayerBase, INotifyPlayerStateChanged
    {
        /// <summary>
        /// foobar2000 が使用するプロセス名。
        /// </summary>
        public static readonly string ProcessName = "foobar2000";

        private static ComWrapper<Application07> _player;
        private static ComWrapper<ApplicationHelper07> _helper;
        private static ComWrapper<Playback07> _playback;

        /// <summary>
        /// foobar2000 の Foobar2000.Application07 への COM ラッパーを取得します。
        /// </summary>
        protected ComWrapper<Application07> Player
        {
            get
            {
                try
                {
                    if (_player == null)
                    {
                        if (Helper.Object.Running)
                        {
                            _player = ComWrapper.Create((Application07)Helper.Object.Server);
                        }
                        else
                        {
                            _player = ComWrapper.Create(new Application07());
                        }
                    }
                    IsDisposed = false;
                    return _player;
                }
                catch (TypeInitializationException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new TypeInitializationException(typeof(Application07).FullName, ex);
                }
            }
        }

        /// <summary>
        /// foobar2000 の Foobar2000Helper.ApplicationHelper07 への COM ラッパーを取得します。
        /// </summary>
        protected ComWrapper<ApplicationHelper07> Helper
        {
            get { return _helper ?? (_helper = ComWrapper.Create(new ApplicationHelper07())); }
        }

        /// <summary>
        /// foobar2000 の Foobar2000.Playback07 への COM ラッパーを取得します。
        /// </summary>
        protected ComWrapper<Playback07> Playback
        {
            get { return _playback ?? (_playback = ComWrapper.Create(Player.Object.Playback)); }
        }

        /// <summary>
        /// <see cref="NowPlayingLib.Foobar2000"/> の新しいインスタンスを作成し、foobar2000 を初期化します。
        /// </summary>
        public Foobar2000()
        {
            Helper.Object.ServerStateChanged += OnServerStateChanged;
            Playback.Object.TrackChanged += OnCurrentMediaChanged;
        }

        /// <summary>
        /// <see cref="NowPlayingLib.Foobar2000.CurrentMediaChanged"/> イベントを発生させます。
        /// </summary>
        /// <param name="bLocationChanged">再生位置が変更されたかを示す値。</param>
        protected void OnCurrentMediaChanged(bool bLocationChanged)
        {
            if (CurrentMediaChanged != null)
            {
                CurrentMediaChanged(this, new CurrentMediaChangedEventArgs(GetCurrentMedia().Result));
            }
        }

        private void OnServerStateChanged(bool bRunning)
        {
            if (_player != null && !bRunning)
            {
                OnClosed();
            }
        }

        /// <summary>
        /// <see cref="NowPlayingLib.Foobar2000.Closed"/> イベントを発生させます。
        /// </summary>
        protected void OnClosed()
        {
            Dispose();
            if (Closed != null)
            {
                Closed(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 非同期操作として現在 foobar2000 で再生している曲を取得します。
        /// </summary>
        /// <returns>
        /// <para>非同期操作を表すタスク オブジェクト。</para>
        /// <para>タスク オブジェクトの <c>Result</c> プロパティは、<see cref="NowPlayingLib.MediaItem"/> を返します。</para>
        /// </returns>
        public override Task<MediaItem> GetCurrentMedia()
        {
            if (!Playback.Object.IsPlaying)
            {
                return Task.FromResult<MediaItem>(null);
            }

            return Task.FromResult(new MediaItem
            {
                Album = Playback.Object.FormatTitle("%album%"),
                AlbumArtist = Playback.Object.FormatTitle("%album artist%"),
                Artist = Playback.Object.FormatTitle("%artist%"),
                BitRate = Playback.Object.FormatTitle("%bitrate%").ToInt32() * 1000,
                Composer = Playback.Object.FormatTitle("$meta(composer)"),
                Duration = TimeSpan.FromSeconds(Playback.Object.Length),
                FileInfo = GetFileInfo(Playback.Object.FormatTitle("%path%")),
                Genre = Playback.Object.FormatTitle("$meta(genre)"),
                Name = Playback.Object.FormatTitle("%title%"),
                TrackNumber = Playback.Object.FormatTitle("%track number%").ToInt32(),
                Year = Playback.Object.FormatTitle("$meta(year)").ToInt32()
            });
        }

        /// <summary>
        /// foobar2000 に関連付けられたアンマネージ リソースを解放します。
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
                if (_playback != null)
                {
                    _playback.Dispose();
                }
                if (_helper != null)
                {
                    _helper.Dispose();
                }
                if (_player != null)
                {
                    _player.Dispose();
                }
            }

            _playback = null;
            _helper = null;
            _player = null;
            IsDisposed = true;
        }

        /// <summary>
        /// foobar2000 の現在の再生状態を取得します。
        /// </summary>
        public override PlayerState PlayerState
        {
            get
            {
                if (!Playback.Object.IsPlaying)
                {
                    return PlayerState.Stopped;
                }
                else if (Playback.Object.IsPaused)
                {
                    return PlayerState.Paused;
                }
                else
                {
                    return PlayerState.Playing;
                }
            }
        }

        /// <summary>
        /// foobar2000 の現在の再生位置を取得または設定します。
        /// </summary>
        public override TimeSpan CurrentPosition
        {
            get { return TimeSpan.FromSeconds(Playback.Object.Position); }
            set
            {
                if (Playback.Object.CanSeek)
                {
                    Playback.Object.Seek(value.TotalSeconds);
                }
            }
        }

        /// <summary>
        /// foobar2000 で再生を実行します。
        /// </summary>
        public override void Play()
        {
            Playback.Object.Start(false);
        }

        /// <summary>
        /// foobar2000 で早送りを実行します。この実装は常に <see cref="System.NotSupportedException"/> をスローします。
        /// </summary>
        /// <exception cref="System.NotSupportedException"/>
        public override void FastForward()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// foobar2000 で巻戻しを実行します。この実装は常に <see cref="System.NotSupportedException"/> をスローします。
        /// </summary>
        /// <exception cref="System.NotSupportedException"/>
        public override void Rewind()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// foobar2000 で停止を実行します。
        /// </summary>
        public override void Stop()
        {
            Playback.Object.Stop();
        }

        /// <summary>
        /// foobar2000 で一時停止を実行します。
        /// </summary>
        public override void Pause()
        {
            Playback.Object.Pause();
        }

        /// <summary>
        /// foobar2000 で次の曲の再生を実行します。
        /// </summary>
        public override void NextTrack()
        {
            Playback.Object.Next();
        }

        /// <summary>
        /// foobar2000 で前の曲の再生を実行します。
        /// </summary>
        public override void PreviousTrack()
        {
            Playback.Object.Previous();
        }

        /// <summary>
        /// foobar2000 で再生中の曲が変更されたときに発生します。
        /// </summary>
        public event CurrentMediaChangedEventHandler CurrentMediaChanged;

        /// <summary>
        /// foobar2000 が終了された時に発生します。
        /// </summary>
        public event EventHandler Closed;
    }
}
