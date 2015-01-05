using Microsoft.MediaPlayer.Interop;
using NowPlayingLib.Helpers;
using NowPlayingLib.Interop;
using NowPlayingLib.Win32;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WindowsMediaPlayerInterop = Microsoft.MediaPlayer.Interop.WindowsMediaPlayer;

namespace NowPlayingLib
{
    /// <summary>
    /// Windows Media Player の機能を提供します。
    /// </summary>
    public class WindowsMediaPlayer : MediaPlayerBase, INotifyPlayerStateChanged
    {
        private static ComWrapper<WindowsMediaPlayerInterop> _player;
        private static ComWrapper<IWMPControls> _controls;

        /// <summary>
        /// Windows Media Player の <see cref="Microsoft.MediaPlayer.Interop.WindowsMediaPlayer"/> への COM ラッパーを取得します。
        /// </summary>
        /// <exception cref="System.TypeInitializationException"/>
        protected ComWrapper<WindowsMediaPlayerInterop> Player
        {
            get
            {
                if (_player == null)
                {
                    try
                    {
                        _player = ComWrapper.Create(new WindowsMediaPlayerInterop());
                        ((IOleObject)_player.Object).SetClientSite(new RemoteHost());
                    }
                    catch (TypeInitializationException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        throw new TypeInitializationException(typeof(WindowsMediaPlayerInterop).FullName, ex);
                    }
                }
                IsDisposed = false;
                return _player;
            }
        }

        /// <summary>
        /// Windows Media Player の <see cref="Microsoft.MediaPlayer.Interop.IWMPControls"/> への COM ラッパーを取得します。
        /// </summary>
        protected ComWrapper<IWMPControls> Controls
        {
            get { return _controls ?? (_controls = ComWrapper.Create(Player.Object.controls)); }
        }

        /// <summary>
        /// <see cref="NowPlayingLib.WindowsMediaPlayer"/> の新しいインスタンスを作成し、Windows Media Player を初期化します。
        /// </summary>
        /// <exception cref="System.TypeInitializationException"/>
        public WindowsMediaPlayer() 
        {
            if (_player == null)
            {
                Player.Object.CurrentItemChange += OnCurrentMediaChanged;
                Player.Object.PlayerDockedStateChange += OnClosed;
            }
        }

        /// <summary>
        /// <see cref="NowPlayingLib.WindowsMediaPlayer.CurrentMediaChanged"/> イベントを発生させます。
        /// </summary>
        /// <param name="pdispMedia">再生中の曲。</param>
        protected async void OnCurrentMediaChanged(object pdispMedia)
        {
            var media = pdispMedia as IWMPMedia3;

            // 曲情報が利用可能になるまで待機
            while (Player.Object.openState != WMPOpenState.wmposMediaOpen)
            {
                media = null;
                await Task.Delay(100);
            }

            if (CurrentMediaChanged != null)
            {
                CurrentMediaChanged(this, new CurrentMediaChangedEventArgs(await GetCurrentMedia(media ?? Player.Object.currentMedia)));
            }
        }

        /// <summary>
        /// <see cref="NowPlayingLib.WindowsMediaPlayer.Closed"/> イベントを発生させます。
        /// </summary>
        protected void OnClosed()
        {
            Dispose();
            if (Closed != null)
            {
                Closed(this, EventArgs.Empty);
            }
        }

        private async Task<Stream> GetArtwork(IWMPMetadataPicture artwork)
        {
            var cache = new INTERNET_CACHE_ENTRY_INFO();
            using (ComWrapper.Create(artwork))
            {
                try
                {
                    cache = NativeMethods.GetUrlCacheEntryInfo(artwork.URL);
                    return await ReadFile(cache.LocalFileName);
                }
                finally
                {
                    NativeMethods.DeleteUrlCacheEntry(cache.SourceUrlName);
                }
            }
        }

        private Task<Stream[]> GetArtworks(MetadataPictureCollection artworks)
        {
            return Task.WhenAll(artworks.Select(GetArtwork));
        }

        private async Task<MediaItem> GetCurrentMedia(IWMPMedia currentMedia)
        {
            if (currentMedia == null)
            {
                return null;
            }

            using (ComWrapper.Create(currentMedia))
            {
                var media = new MediaItem
                {
                    Album = currentMedia.getItemInfo(AudioAttributes.AlbumTitle),
                    AlbumArtist = currentMedia.getItemInfo(AudioAttributes.AlbumArtist),
                    Artist = currentMedia.getItemInfo(AudioAttributes.Author),
                    BitRate = currentMedia.getItemInfo(AudioAttributes.Bitrate).ToInt32(),
                    Category = currentMedia.getItemInfo(AudioAttributes.Category),
                    Composer = currentMedia.getItemInfo(AudioAttributes.Composer),
                    DateAdded = currentMedia.getItemInfo(AudioAttributes.AcquisitionTime).ToDateTime(),
                    Duration = TimeSpan.FromSeconds(currentMedia.getItemInfo(AudioAttributes.Duration).ToDouble()),
                    FileInfo = GetFileInfo(currentMedia.getItemInfo(AudioAttributes.SourceUrl)),
                    Genre = currentMedia.getItemInfo(AudioAttributes.Genre),
                    Kind = currentMedia.getItemInfo(AudioAttributes.FileType),
                    Lyrics = currentMedia.getItemInfo(AudioAttributes.Lyrics),
                    Name = currentMedia.getItemInfo(AudioAttributes.Title),
                    PlayedCount = currentMedia.getItemInfo(AudioAttributes.UserPlayCount).ToInt32(),
                    PlayedDate = currentMedia.getItemInfo(AudioAttributes.UserLastPlayedTime).ToDateTime(),
                    ReleaseDate = currentMedia.getItemInfo(AudioAttributes.ReleaseDate).ToDateTime(),
                    TrackNumber = currentMedia.getItemInfo(AudioAttributes.TrackNumber).ToInt32(),
                    Year = currentMedia.getItemInfo(AudioAttributes.Year).ToInt32()
                };
                media.AlbumArtist = string.IsNullOrEmpty(media.AlbumArtist) ? media.Artist : media.AlbumArtist;
                media.Artworks = await GetArtworks(new MetadataPictureCollection(currentMedia));
                return media;
            }
        }

        /// <summary>
        /// 非同期操作として現在 Windows Media Player で再生している曲を取得します。
        /// </summary>
        /// <para>非同期操作を表すタスク オブジェクト。</para>
        /// <para>タスク オブジェクトの <c>Result</c> プロパティは、<see cref="NowPlayingLib.MediaItem"/> を返します。</para>
        public override Task<MediaItem> GetCurrentMedia()
        {
            return GetCurrentMedia(Player.Object.currentMedia);
        }

        /// <summary>
        /// Windows Media Player に関連付けられたアンマネージ リソースを解放します。
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
                if (_player != null)
                {
                    _player.Object.CurrentItemChange -= OnCurrentMediaChanged;
                    _player.Object.PlayerDockedStateChange -= OnClosed;
                    ((IOleObject)_player.Object).Close(OLECLOSE.NOSAVE);
                    _player.Dispose();
                }
            }

            _controls = null;
            _player = null;
            IsDisposed = true;
        }

        /// <summary>
        /// Windows Media Player の現在の再生状態を取得します。
        /// </summary>
        public override PlayerState PlayerState
        {
            get
            {
                switch (Player.Object.playState)
                {
                    case WMPPlayState.wmppsPaused:
                        return PlayerState.Paused;
                    case WMPPlayState.wmppsPlaying:
                        return PlayerState.Playing;
                    case WMPPlayState.wmppsScanForward:
                        return PlayerState.FastForward;
                    case WMPPlayState.wmppsScanReverse:
                        return PlayerState.Rewind;
                    case WMPPlayState.wmppsStopped:
                        return PlayerState.Stopped;
                    default:
                        return PlayerState.Unknown;
                }
            }
        }

        /// <summary>
        /// Windows Media Player の現在の再生位置を取得または設定します。
        /// </summary>
        public override TimeSpan CurrentPosition
        {
            get { return TimeSpan.FromSeconds(Controls.Object.currentPosition); }
            set { Controls.Object.currentPosition = value.TotalSeconds; }
        }

        /// <summary>
        /// Windows Media Player で再生を実行します。
        /// </summary>
        public override void Play()
        {
            Controls.Object.play();
        }

        /// <summary>
        /// Windows Media Player で早送りを実行します。
        /// </summary>
        public override void FastForward()
        {
            Controls.Object.fastForward();
        }

        /// <summary>
        /// Windows Media Player で巻戻しを実行します。
        /// </summary>
        public override void Rewind()
        {
            Controls.Object.fastReverse();
        }

        /// <summary>
        /// Windows Media Player で停止を実行します。
        /// </summary>
        public override void Stop()
        {
            Controls.Object.stop();
        }

        /// <summary>
        /// Windows Media Player で一時停止を実行します。
        /// </summary>
        public override void Pause()
        {
            Controls.Object.pause();
        }

        /// <summary>
        /// Windows Media Player で次の曲の再生を実行します。
        /// </summary>
        public override void NextTrack()
        {
            Controls.Object.next();
        }

        /// <summary>
        /// Windows Media Player で前の曲の再生を実行します。
        /// </summary>
        public override void PreviousTrack()
        {
            Controls.Object.previous();
        }

        /// <summary>
        /// Windows Media Player で再生中の曲が変更されたときに発生します。
        /// </summary>
        public event CurrentMediaChangedEventHandler CurrentMediaChanged;

        /// <summary>
        /// Windows Media Player が終了されたときに発生します。
        /// </summary>
        public event EventHandler Closed;
    }
}
