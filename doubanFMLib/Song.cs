using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doubanFMLib
{
    //"album":"\/subject\/4732961\/",
    //  "picture":"http:\/\/img3.douban.com\/mpic\/s4246176.jpg",
    //  "ssid":"8074",
    //  "artist":"Ólafur Arnalds",
    //  "url":"http:\/\/mr4.douban.com\/201309301009\/58419e1d4dfc45d9ff800b559839ff56\/view\/song\/small\/p1639693.mp3",
    //  "company":"Erased Tapes Records",
    //  "title":"Gleypa Okkur",
    //  "rating_avg":4.46059,
    //  "length":348,
    //  "subtype":"",
    //  "public_time":"2010",
    //  "sid":"1639693",
    //  "aid":"4732961",
    //  "sha256":"834329357c6ab0f591d84d14bef8c3e6c9333fdac0661308629785a6101df2ea",
    //  "kbps":"64",
    //  "albumtitle":"...And They Have ...",
    //  "like":0

    public class Song
    {
        public string album { get; set; }
        public string picture { get; set; }
        public string ssid { get; set; }
        public string artist { get; set; }
        public string url { get; set; }
        public string company { get; set; }
        public string title { get; set; }
        public double rating_avg { get; set; }
        public long length { get; set; }
        public string subtype { get; set; }
        public string public_time { get; set; }
        public string sid { get; set; }
        public string aid { get; set; }
        public string sha256 { get; set; }
        public string kbps { get; set; }
        public string albumtitle { get; set; }
        public int like { get; set; }
        public int adtype { get; set; }
        public string monitor_url { get; set; }


    }
}
