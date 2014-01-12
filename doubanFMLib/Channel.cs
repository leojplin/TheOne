using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doubanFMLib
{


    public class Channel
    {

      //"name":"私人兆赫",
      //"seq_id":0,
      //"abbr_en":"My", // Shouldn't it be "mine"? :)
      //"channel_id":0,
      //"name_en":"Personal Radio"

        public string name { get; set; }
        public string seq_id { get; set; }
        public string abbr_en { get; set; }
        public string channel_id { get; set; }
        public string name_en { get; set; }
        public string cover { get; set; }

        //used when the channels are from search result
        public Creator creater { get; set; }
        public IList<string> hot_songs { get; set; }
        public int id { get; set; }
        public string song_num { get; set; }
        public string banner { get; set; }
        public string intro { get; set; }

        public bool is_fav { get; set; }

    }


    public class Creator
    {
        public long id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string pic { get; set; }
    }
}
