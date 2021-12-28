using iTunesLib;
using NowPlayingLib.Interop;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NowPlayingLib
{
    /// <summary>
    /// iTunes の機能を提供します。
    /// </summary>
    public class iTunes : MediaPlayerBase, INotifyPlayerStateChanged
    {
        /// <summary>
        /// iTunes が使用するプロセス名。
        /// </summary>
        public static readonly string ProcessName = "iTunes";

        private static ComWrapper<iTunesApp> _player;

        /// <summary>
        /// iTunes の <see cref="iTunesLib.iTunesApp"/> への COM ラッパーを取得します。
        /// </summary>
        /// <exception cref="System.TypeInitializationException"/>
        protected ComWrapper<iTunesApp> Player
        {
            get 
            {
                try
                {
                    _player = _player ?? ComWrapper.Create(new iTunesApp());
                    IsDisposed = false;
                    return _player;
                }
                catch (TypeInitializationException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new TypeInitializationException(typeof(iTunesApp).FullName, ex);
                }
            }
        }

        /// <summary>
        /// <see cref="NowPlayingLib.iTunes"/> の新しいインスタンスを作成し、iTunes を初期化します。
        /// </summary>
        /// <exception cref="System.TypeInitializationException"/>
        public iTunes() 
        {
            if (_player == null)
            {
                Player.Object.OnPlayerPlayEvent += OnCurrentMediaChanged;
                Player.Object.OnAboutToPromptUserToQuitEvent += OnClosed;
            }
        }

        /// <summary>
        /// <see cref="NowPlayingLib.iTunes.CurrentMediaChanged"/> イベントを発生させます。
        /// </summary>
        /// <param name="iTrack">再生中の曲。</param>
        protected async void OnCurrentMediaChanged(object iTrack)
        {
            var track = iTrack as IITTrack;
            if (track != null)
            {
                CurrentMediaChanged?.Invoke(this, new CurrentMediaChangedEventArgs(await GetCurrentMedia(track)));
            }
        }

        /// <summary>
        /// <see cref="NowPlayingLib.iTunes.Closed"/> イベントを発生させます。
        /// </summary>
        protected void OnClosed()
        {
            Dispose();
            Closed?.Invoke(this, EventArgs.Empty);
        }

        private async Task<Stream> GetArtwork(IITArtwork artwork)
        {
            using (ComWrapper.Create(artwork))
            {
                string path = Path.GetTempFileName();
                try
                {
                    artwork.SaveArtworkToFile(path);
                    return await ReadFile(path);
                }
                finally
                {
                    File.Delete(path);
                }
            }
        }

        private Task<Stream[]> GetArtworks(IITArtworkCollection artworks) => Task.WhenAll(artworks.OfType<IITArtwork>().Select(GetArtwork));

        private async Task<MediaItem> GetCurrentMedia(IITTrack currentTrack)
        {
            if (currentTrack == null)
            {
                return null;
            }

            using (ComWrapper.Create(currentTrack))
            using (var artworks = ComWrapper.Create(currentTrack.Artwork))
            {
                var media = new MediaItem
                {
                    Album = currentTrack.Album ?? "",
                    AlbumArtist = currentTrack.Artist ?? "",
                    Artist = currentTrack.Artist ?? "",
                    BitRate = currentTrack.BitRate * 1000,
                    Category = ((dynamic)currentTrack).Category ?? "",
                    Composer = currentTrack.Composer ?? "",
                    DateAdded = currentTrack.DateAdded,
                    Duration = TimeSpan.FromSeconds(currentTrack.Duration),
                    FileInfo = GetFileInfo(((dynamic)currentTrack).Location),
                    Genre = currentTrack.Genre ?? "",
                    Kind = currentTrack.KindAsString ?? "",
                    Lyrics = ((dynamic)currentTrack).Lyrics ?? "",
                    Name = currentTrack.Name ?? "",
                    PlayedCount = currentTrack.PlayedCount,
                    PlayedDate = currentTrack.PlayedDate,
                    TrackNumber = currentTrack.TrackNumber,
                    Year = currentTrack.Year
                };
                media.AlbumArtist = ((dynamic)currentTrack).AlbumArtist ?? media.Artist ?? "";
                media.Artworks = new Collection<Stream>(await GetArtworks(artworks.Object).ConfigureAwait(false));
                return media;
            }
        }

        /// <summary>
        /// 非同期操作として現在 iTunes で再生している曲を取得します。
        /// </summary>
        /// <returns>
        /// <para>非同期操作を表すタスク オブジェクト。</para>
        /// <para>タスク オブジェクトの <c>Result</c> プロパティは、<see cref="NowPlayingLib.MediaItem"/> を返します。</para>
        /// </returns>
        public override Task<MediaItem> GetCurrentMedia() => GetCurrentMedia(Player.Object.CurrentTrack);

        /// <summary>
        /// iTunes に関連付けられたアンマネージ リソースを解放します。
        /// </summary>
        /// <param name="disposing">明示的な破棄要求元から呼び出された場合は true に設定します。それ以外の場合は false。</param>
        protected override void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (_player != null && disposing)
            {
                _player.Object.OnPlayerPlayEvent -= OnCurrentMediaChanged;
                _player.Object.OnAboutToPromptUserToQuitEvent -= OnClosed;
                _player.Dispose();
            }

            _player = null;
            IsDisposed = true;
        }

        /// <summary>
        /// iTunes の現在の再生状態を取得します。
        /// </summary>
        public override PlayerState PlayerState
        {
            get
            {
                switch (Player.Object.PlayerState)
                {
                    case ITPlayerState.ITPlayerStateFastForward:
                        return PlayerState.FastForward;
                    case ITPlayerState.ITPlayerStatePlaying:
                        return PlayerState.Playing;
                    case ITPlayerState.ITPlayerStateRewind:
                        return PlayerState.Rewind;
                    case ITPlayerState.ITPlayerStateStopped:
                        return PlayerState.Stopped;
                    default:
                        return PlayerState.Unknown;
                }
            }
        }

        /// <summary>
        /// iTunes の現在の再生位置を取得または設定します。
        /// </summary>
        public override TimeSpan CurrentPosition
        {
            get 
            {
                if (this.PlayerState != PlayerState.Stopped)
                {
                    return TimeSpan.FromSeconds(Player.Object.PlayerPosition);
                }
                else
                {
                    return TimeSpan.Zero;
                }
            }
            set
            {
                if (this.PlayerState != PlayerState.Stopped)
                {
                    Player.Object.PlayerPosition = (int)value.TotalSeconds;
                }
            }
        }

        /// <summary>
        /// iTunes で再生を実行します。
        /// </summary>
        public override void Play() => Player.Object.Play();

        /// <summary>
        /// iTunes で早送りを実行します。
        /// </summary>
        public override void FastForward() => Player.Object.FastForward();

        /// <summary>
        /// iTunes で巻戻しを実行します。
        /// </summary>
        public override void Rewind() => Player.Object.Rewind();

        /// <summary>
        /// iTunes で停止を実行します。
        /// </summary>
        public override void Stop() => Player.Object.Stop();

        /// <summary>
        /// iTunes で一時停止を実行します。
        /// </summary>
        public override void Pause() => Player.Object.Pause();

        /// <summary>
        /// iTunes で次の曲の再生を実行します。
        /// </summary>
        public override void NextTrack() => Player.Object.NextTrack();

        /// <summary>
        /// iTunes で前の曲の再生を実行します。
        /// </summary>
        public override void PreviousTrack() => Player.Object.PreviousTrack();

        /// <summary>
        /// iTunes で再生中の曲が変更されたときに発生します。
        /// </summary>
        public event CurrentMediaChangedEventHandler CurrentMediaChanged;

        /// <summary>
        /// iTunes が終了された時に発生します。
        /// </summary>
        public event EventHandler Closed;
    }
}
