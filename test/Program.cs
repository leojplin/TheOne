using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using doubanFMLib;
using System.Net.Http;

namespace test
{
    class Program
    {
        static Channel c; 
        static void Main(string[] args)
        {
            dowork();
            Console.ReadKey();
        }

        public static async void  dowork()
        {
            //User u = await Douban.Authenticate("peipei.520@hotmail.com", "5201314");
            //var v = await Douban.GetChannels();
            //var s = await Douban.GetSongs(u, v.ToList<Channel>()[0].channel_id);
            //var r = await Douban.SearchChannel("S.H.E");
            //c = await Douban.GetChannel(u, "1");
            Douban.test();
        }
    }
}
