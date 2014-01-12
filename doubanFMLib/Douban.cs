using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace doubanFMLib
{


    public class Douban
    {
        public const string none = "n"; //none, used for getting songlist
        public const string end = "e"; //noramlly ended a song
        public const string unlike = "u"; //unlike a song
        public const string like = "r"; //like a song
        public const string skip = "s"; //skip a song
        public const string trash = "b"; //trash a song

        public const string App_name = "radio_desktop_win";
        public const string Version = "100";
        public const string BaseAddress = "http://www.douban.com";

        public async static Task<User> Authenticate(string email, string password)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseAddress);
            KeyValuePair<string, string>[] content = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("email", email),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("app_name", App_name),
                new KeyValuePair<string, string>("version", Version)
            };
            FormUrlEncodedContent con = new FormUrlEncodedContent(content);
            var response = await client.PostAsync("/j/app/login", con);


            return await ParseAccount(response);
        }

        private async static Task<User> ParseAccount(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            JObject job = JObject.Parse(json);
            return job.ToObject<User>();

        }

        public async static Task<ICollection<Channel>> GetChannels()
        {
            string uri = "/j/app/radio/channels";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseAddress);
            var response = await client.GetStringAsync(uri);
            JObject job = JObject.Parse(response);
            return await JsonConvert.DeserializeObjectAsync<List<Channel>>(job.GetValue("channels").ToString());
        }

        public async static Task<ICollection<Song>> GetSongs(User user, string cid)
        {
            string type = "n";
            string uri = "/j/app/radio/people";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseAddress);
            KeyValuePair<string, string>[] content;
            if (user == null)
            {
                content = new KeyValuePair<string, string>[]
                {
            
                new KeyValuePair<string, string>("app_name", App_name),
                new KeyValuePair<string, string>("version", Version),
                new KeyValuePair<string, string>("channel", cid),
                new KeyValuePair<string, string>("type", type )
                };
            }
            else
            {
                content = new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("app_name", App_name),
                    new KeyValuePair<string, string>("version", Version),
                    new KeyValuePair<string, string>("user_id", user.user_id),
                    new KeyValuePair<string, string>("expire", user.expire),
                    new KeyValuePair<string, string>("token", user.token),
                    new KeyValuePair<string, string>("channel", cid),
                    new KeyValuePair<string, string>("type", type)
                };
            }


            FormUrlEncodedContent con = new FormUrlEncodedContent(content);
            var response = await client.PostAsync(uri, con);
            var data = await response.Content.ReadAsStringAsync();
            return await JsonConvert.DeserializeObjectAsync<List<Song>>(JObject.Parse(data).GetValue("song").ToString());
        }

        public async static void ReportUserAction(User user, string sid, string cid, string type)
        {
            if (user != null)
            {
                string uri = "/j/app/radio/people";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BaseAddress);
                KeyValuePair<string, string>[] content = new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("user_id", user.user_id),
                    new KeyValuePair<string, string>("expire", user.expire),
                    new KeyValuePair<string, string>("sid", sid),
                    new KeyValuePair<string, string>("channel", cid),
                    new KeyValuePair<string, string>("type", type),
                    new KeyValuePair<string, string>("app_name", App_name),
                    new KeyValuePair<string, string>("version", Version)
                };
                FormUrlEncodedContent con = new FormUrlEncodedContent(content);
                var response = await client.PostAsync(uri, con);
                //var data = await response.Content.ReadAsStringAsync();
                //return await JsonConvert.DeserializeObjectAsync<List<Song>>(JObject.Parse(data).GetValue("song").ToString());
            }


        }

        public async static Task<Channel> GetChannel(User user, string cid)
        {

            string uri = "http://www.douban.fm/j/explore/channel_detail?channel_id=" + cid;
            HttpClient client = new HttpClient();
            KeyValuePair<string, string>[] content;
            if (user != null)
            {
                content = new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("user_id", user.user_id),
                    new KeyValuePair<string, string>("expire", user.expire),
                    new KeyValuePair<string, string>("app_name", App_name),
                    new KeyValuePair<string, string>("version", Version)
                };
            }

            else
            {
                content = new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("app_name", App_name),
                    new KeyValuePair<string, string>("version", Version)
                };
            }
            FormUrlEncodedContent con = new FormUrlEncodedContent(content);
            var response = await client.PostAsync(uri, con);
            var data = await response.Content.ReadAsStringAsync();
            var idk = JObject.Parse(data).GetValue("data")["channel"];
            var idks = idk.ToString();
            return await JsonConvert.DeserializeObjectAsync<Channel>(idks);
        }

        public async static Task<Object> test()
        {
            string uri = "http://www.douban.fm/j/login" + "?source=radio&alias=peipei.520%40hotmail.com&form_password=5201314";
            HttpClient client = new HttpClient();
            KeyValuePair<string, string>[] content;
            
            
            content = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("spurce", "radio"),
                new KeyValuePair<string, string>("email", "peipei.520@hotmail.com"),
                new KeyValuePair<string, string>("form_password", "5201314"),
                new KeyValuePair<string, string>("captcha_solution", "sponge"),
                new KeyValuePair<string, string>("captcha_id", "wg9TUgJ0VCijHjDDyFL1oj6h:en"),
                new KeyValuePair<string, string>("task", "sync_channel_list")
            };
            
            FormUrlEncodedContent con = new FormUrlEncodedContent(content);
            var response = await client.PostAsync(uri, con);
            var data = await response.Content.ReadAsStringAsync();
            return null;
        }

        public async static Task<ICollection<Channel>> SearchChannel(string query)
        {
            string uri = "http://douban.fm/j/explore/search?query=" + query + "&start=0&limit=20";
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(uri);
            return JsonConvert.DeserializeObject<ICollection<Channel>>(
                JObject.Parse(response).GetValue("data").Value<JObject>().GetValue("channels").ToString());

        }
    }
}
