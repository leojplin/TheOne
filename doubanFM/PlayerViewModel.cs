using doubanFMLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using doubanFMAudioPlayer;
using Microsoft.Phone.BackgroundAudio;
using Newtonsoft.Json;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;

namespace doubanFM
{
    class PlayerViewModel : INotifyPropertyChanged
    {
        BackgroundAudioPlayer player = BackgroundAudioPlayer.Instance;


        private User currentUser;
        public User CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                onPropertyChanged();
                LikeCommand.RaiseCanExecuteChanged();
                TrashCommand.RaiseCanExecuteChanged();
            }
        }

        private Song currentSong;
        public Song CurrentSong
        {
            get { return currentSong; }
            set
            {
                currentSong = value;
                onPropertyChanged();
            }
        }

        internal Channel currentChannel;
        public Channel CurrentChannel
        {
            get { return currentChannel; }
            set
            {
                currentChannel = value;

                onPropertyChanged();

            }
        }

        private bool ready = false;
        public bool Ready
        {
            get { return ready; }
            set
            {
                ready = value;
                onPropertyChanged();
                PlayCommand.RaiseCanExecuteChanged();
                SkipCommand.RaiseCanExecuteChanged();
                LikeCommand.RaiseCanExecuteChanged();
                TrashCommand.RaiseCanExecuteChanged();
            }
        }

        private AudioTrack currentTrack;

        public AudioTrack CurrentTrack
        {
            get { return currentTrack; }
            set { currentTrack = value; }
        }

        private string playText = "\uE102";
        private string pauseText = "\uE103";
        private string playIcon = "\uE103";
        public string PlayIcon
        {
            get { return playIcon; }
            set
            {
                playIcon = value;
                onPropertyChanged();
            }
        }



        public RelayCommand PlayCommand { get; private set; }
        public RelayCommand SkipCommand { get; private set; }
        public RelayCommand LikeCommand { get; private set; }
        public RelayCommand TrashCommand { get; private set; }



        public string ChannelId { get; set; }

        public PlayerViewModel()
        {
            player.PlayStateChanged += Instance_PlayStateChanged;

            PlayCommand = new RelayCommand(() => Play(), () => Ready);
            SkipCommand = new RelayCommand(() => Skip(), () => Ready);
            LikeCommand = new RelayCommand(() => Like(), () => Ready && currentUser != null);
            TrashCommand = new RelayCommand(() => Trash(), () => Ready && currentUser != null);

            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains("currentChannel"))
            {
                CurrentChannel = settings["currentChannel"] as Channel;
            }
            if (settings.Contains("user"))
            {
                CurrentUser = settings["user"] as User;
            }
        }

        private void Trash()
        {
            Douban.ReportUserAction(CurrentUser, currentSong.sid, currentChannel.channel_id, Douban.trash);
            Skip();
        }

        private void Like()
        {
            Douban.ReportUserAction(CurrentUser, currentSong.sid, currentChannel.channel_id, Douban.like);
        }

        private void Skip()
        {
            BackgroundAudioPlayer.Instance.SkipNext();
        }


        private void Play()
        {
            if (player.PlayerState == PlayState.Playing)
            {
                player.Pause();
            }
            if (player.PlayerState == PlayState.Paused)
            {
                player.Play();
            }
        }

        public void UpdatePlayerState()
        {
            Instance_PlayStateChanged(player, new PlayStateChangedEventArgs(player.PlayerState));
            if(player.Track != null)
            {
                CurrentSong = JsonConvert.DeserializeObject<Song>(player.Track.Tag);
            }
        }

        void Instance_PlayStateChanged(object sender, EventArgs e)
        {
            var a = e as PlayStateChangedEventArgs;
            if (a.IntermediatePlayState == PlayState.TrackReady)
            {
                CurrentSong = JsonConvert.DeserializeObject<Song>(player.Track.Tag);
            }
            if (a.CurrentPlayState == PlayState.Paused ||
                a.CurrentPlayState == PlayState.Playing)
            {
                Ready = true;
            }
            else
            {
                Ready = false;
            }

            if(a.CurrentPlayState == PlayState.Paused)
            {
                PlayIcon = playText;
            }
            if(a.CurrentPlayState == PlayState.Playing)
            {
                PlayIcon = pauseText;
            }

        }


        public void onPropertyChanged([CallerMemberName] string s = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(s));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
