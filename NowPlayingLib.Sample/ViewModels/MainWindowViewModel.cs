using Livet;
using Livet.Commands;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace NowPlayingLib.Sample.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        #region Artwork 変更通知プロパティ

        private BitmapImage _artwork;
        public BitmapImage Artwork
        {
            get { return _artwork; }
            set
            {
                _artwork = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region MediaItem 変更通知プロパティ

        private MediaItem _mediaItem;
        public MediaItem MediaItem
        {
            get { return _mediaItem; }
            set
            {
                _mediaItem = value;
                SetArtwork(value);
                RaisePropertyChanged();
            }
        }

        #endregion

        #region PlayerName 変更通知プロパティ

        private string _playerName;
        public string PlayerName
        {
            get { return _playerName; }
            set
            {
                _playerName = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region IsMenuShown 変更通知プロパティ

        private bool _isMenuShown;
        public bool IsMenuShown
        {
            get { return _isMenuShown; }
            set
            {
                _isMenuShown = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region ErrorMessage 変更通知プロパティ

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            { 
                _errorMessage = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        public MediaPlayerBase Player { get; set; }

        private void SetArtwork(MediaItem value)
        {
            if (value == null || value.Artworks.FirstOrDefault() == null)
            {
                this.Artwork = null;
                return;
            }

            try
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = value.Artworks.FirstOrDefault();
                image.EndInit();
                image.Freeze();
                this.Artwork = image;
            }
            catch
            {
                this.Artwork = null;
            }
        }

        public void SwitchMenu()
        {
            this.IsMenuShown = !this.IsMenuShown;
        }

        #region SelectPlayerCommand

        private ListenerCommand<string> _selectPlayerCommand;
        public ListenerCommand<string> SelectPlayerCommand
        {
            get { return _selectPlayerCommand ?? (_selectPlayerCommand = new ListenerCommand<string>(SelectPlayer)); }
        }

        #endregion

        public async void SelectPlayer(string parameter)
        {
            this.IsMenuShown = false;
            MediaPlayerBase player;

            try
            {
                this.PlayerName = "PLEASE WAIT...";
                this.ErrorMessage = null;
                this.MediaItem = null;

                switch (parameter)
                {
                    case "WINDOWS MEDIA PLAYER":
                        player = await Task.Run(() => new WindowsMediaPlayer());
                        break;
                    case "ITUNES":
                        player = await Task.Run(() => new iTunes());
                        break;
                    case "X-APPLICATION":
                        player = new XApplication();
                        break;
                    case "LISMO PORT":
                        player = new LismoPort();
                        break;
                    default:
                        return;
                }

                var p = player as INotifyPlayerStateChanged;
                if (p != null)
                {
                    p.CurrentMediaChanged += SetCurrentMedia;
                }
                SetCurrentMedia(player, new CurrentMediaChangedEventArgs(await player.GetCurrentMedia()));

                this.Player = player;
                this.PlayerName = parameter;
            }
            catch (Exception ex)
            {
                this.PlayerName = "ERROR";
                this.MediaItem = null;
                this.ErrorMessage = ex.GetType().FullName + "\n" + ex.Message;
            }
        }

        private void SetCurrentMedia(object sender, CurrentMediaChangedEventArgs e)
        {
            this.MediaItem = e.CurrentMedia;
        }
    }
}
