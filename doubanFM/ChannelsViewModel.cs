using doubanFMLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace doubanFM
{
    public class ChannelsViewModel : INotifyPropertyChanged
    {

        private List<Channel> channels;

        public List<Channel> Channels
        {
            get { return channels; }
            set
            {
                channels = value;
                OnPropertyChanged();
            }
        }


        public ChannelsViewModel()
        {
            LoadChannels();
        }

        private async void LoadChannels()
        {
            var res = App.GetResourceStream(new Uri("ChannelsJson.txt", UriKind.Relative));
            var txt = new StreamReader(res.Stream).ReadToEnd();
            Channels = await JsonConvert.DeserializeObjectAsync<List<Channel>>(txt);
            
        }



























        public void OnPropertyChanged([CallerMemberName] string s = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(s));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
