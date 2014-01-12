using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using doubanFM.Resources;
using Microsoft.Phone.BackgroundAudio;
using doubanFMLib;
using System.IO.IsolatedStorage;
using doubanFMAudioPlayer;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using System.Windows.Shapes;
using Windows.Storage;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace doubanFM
{
    public partial class MainPage : PhoneApplicationPage
    {

        BackgroundAudioPlayer player = BackgroundAudioPlayer.Instance;
        SearchResultVM searchVM = new SearchResultVM();
        RecentChannelsVM recentVm = new RecentChannelsVM();

        public MainPage()
        {
            InitializeComponent();
            Search.DataContext = searchVM;
            RecentChannelsPivot.DataContext = recentVm;
            
        }

        private void PhoneTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var s = (sender as PhoneTextBox).Text;
            if(!string.IsNullOrEmpty(s))
            {
                searchVM.search(s);
            }
        }

        

        private void PhoneTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                Search.Focus();
            }
        }

        private void myPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var p = sender as Pivot;
            if(p.SelectedIndex == 0)
            {
                recentVm.Refresh();
            }
        }

    }
}