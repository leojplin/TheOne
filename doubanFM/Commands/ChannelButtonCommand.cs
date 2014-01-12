using doubanFMLib;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace doubanFM
{

    public enum PlayerPageAction
    {
        Nothing,
        New
    }

    public enum PlayMode
    {
        Stream,
        Offline
    }

    public class ChannelButtonCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var frame = App.Current.RootVisual as PhoneApplicationFrame;
            var ch = (Channel)parameter;
            Settings.Put<Channel>("currentChannel", ch);
            var chs = Settings.Get<List<Channel>>("recentChannels");
            if(chs == null)
            {
                var c = new List<Channel>();
                c.Add(ch);
                Settings.Put<ICollection<Channel>>("recentChannels", c);
            }
            else
            {
                if(chs.Count >= 9)
                {
                    chs.RemoveAt(0);
                }
                chs.Add(ch);
                Settings.Put<ICollection<Channel>>("recentChannels", chs);
            }
            frame.Navigate(new Uri("/PlayerPage.xaml?action=" + PlayerPageAction.New + "&origin=" + PlayMode.Stream, UriKind.Relative));
        }
    }
}
