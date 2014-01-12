using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using doubanFMLib;
using System.IO.IsolatedStorage;
using Newtonsoft.Json;
using Microsoft.Phone.BackgroundAudio;

namespace doubanFM
{

    

    public partial class PlayerPage : PhoneApplicationPage
    {
        PlayerViewModel playerVM;
        BackgroundAudioPlayer player = BackgroundAudioPlayer.Instance;
        private bool isReset = false;
        public PlayerPage()
        {
            InitializeComponent();
            playerVM = new PlayerViewModel();
            DataContext = playerVM;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //base.OnNavigatingFrom(e);
            //if (isReset)
            //{
            //    isReset = false;
            //    e.Cancel = true;
            //}
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string action;
            string mode;

            if (e.NavigationMode == NavigationMode.New)
            {
                NavigationContext.QueryString.TryGetValue("action", out action);
                NavigationContext.QueryString.TryGetValue("origin", out mode);
                if (action == PlayerPageAction.New.ToString())
                {
                    Settings.Put<string>("mode", mode);
                    player.Stop(); //implemented as starting a new channel;
                    player.Play();

                }
                else
                {
                    //for the case where it is already playing/initiated from the same channel thats currently playing
                    if (player.PlayerState != PlayState.Paused)
                    {
                        player.Play();
                    }
                }
            }
            else
            {
                playerVM.UpdatePlayerState();
            }
            if(e.NavigationMode == NavigationMode.Reset)
            {
                isReset = true;
            }


        }

    }
}