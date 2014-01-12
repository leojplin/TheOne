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

        public MainPage()
        {
            InitializeComponent();
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ////var c = await Douban.GetChannels() as IList<Channel>;
            //var settings = IsolatedStorageSettings.ApplicationSettings;
            //var c = await Douban.Authenticate("peipei.520@hotmail.com", "5201314");
            //if (settings.Contains("user"))
            //{
            //    settings["user"] = c;
            //}
            //else
            //{
            //    settings.Add("user", c);
            //}
            //var cc = await Douban.GetChannels();
            //var chs = new List<Channel>();
            //foreach (var v in cc)
            //{
            //    if (v.channel_id != "0")
            //    {
            //        chs.Add(await Douban.GetChannel(c, v.channel_id));
            //    }
            //}

            //var ss=  JsonConvert.SerializeObject(chs);
            //Debug.WriteLine(ss);
            //StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            //var file = await local.CreateFileAsync("data", CreationCollisionOption.ReplaceExisting);
            //var stream = await file.OpenStreamForWriteAsync();
            //await stream.WriteAsync(Encoding.UTF8.GetBytes(ss.ToCharArray()), 0, Encoding.UTF8.GetByteCount(ss.ToCharArray()));




            NavigationService.Navigate(new Uri("/PlayerPage.xaml", UriKind.Relative));
        }

        private void myPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}