using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GamerPalsBackend.DataObjects.Models;
using GamerPalsBackend.Other;
using Newtonsoft.Json.Linq;

namespace GamerPalsBackend.Managers
{
    public static class DiscordChannelManager
    {
        public static bool CreateChannelForActiveSearch(this ActiveSearch search, params long[] userIds)
        {
            var server = GetLeastPopulatedServer();
            //Creating a new Channel
            if (search.DiscordInviteCode == null)
            {
                search.DiscordChannelId = CreateChannel("dis another more random name" + new Random().Next(10000), server);
                //Creating a channel invite for the new channel
                search.DiscordInviteCode = CreateChannelInvite(search.DiscordChannelId, server);
            }

            if (search.DiscordRoleId == 0)
            {
                //Setting new permissions for the Channel
                search.DiscordRoleId = CreateRoleForLobby(search.DiscordChannelId, server);
                EditEveryonePermissions(search.DiscordChannelId);
                AddRoleToChannel(search.DiscordChannelId, search.DiscordRoleId);
                AddRoleToUsers(search.DiscordRoleId,server, 359727752372420608);
            }
            
            return true;
        }

        public static bool AddUserToExistingChannel(this ActiveSearch search, long userId)
        {
            if (search.DiscordRoleId == 0 || search.DiscordChannelId == 0)
                return false;

            AddRoleToUsers(search.DiscordRoleId, userId);
            return true;
        }

        public static long GetLeastPopulatedServer()
        {
            List<long> ids = new List<long>();
            var guilds = PalsConfiguration.SystemSettings["DiscordGuilds"].Split(',');
            foreach (var guild in guilds)
            {
                ids.Add(long.Parse(guild.Trim()));
            }
            List<Tuple<long,int>> cache = new List<Tuple<long, int>>();
            foreach (long id in ids)
            {
                WebRequest request = WebRequest.Create("https://discordapp.com/api/guilds/" + id);
                request.Method = "POST";
                request.Headers.Add("Authorization: Bot NDIzMDQ5NjAyNzMyNzg1NjY1.DYksOw.iUAbB-FHu6yM4NPO6-z7u3dHhC4");
                var stream = new StreamReader(request.GetResponse().GetResponseStream());
                var data = JObject.Parse(stream.ReadToEnd());
                var roles = data["roles"] as JArray;
                cache.Add(new Tuple<long, int>(id, roles.Count));
            }

            return cache.Single(i => i.Item2 == cache.Max(s => s.Item2)).Item1;
        }

        private static void AddRoleToUsers(long roleid,long server, params long[] ids)
        {
            foreach (var id in ids)
            {
                WebRequest channel = WebRequest.Create("https://discordapp.com/api/guilds/"+server+"/members/" + id + "/roles/" + roleid);
                channel.Method = "PUT";
                channel.Headers.Add("Authorization: Bot NDIzMDQ5NjAyNzMyNzg1NjY1.DYksOw.iUAbB-FHu6yM4NPO6-z7u3dHhC4");
                channel.ContentLength = 0;
                WebResponse channelResp = channel.GetResponse();
                Stream responseStream = channelResp.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string rete = reader.ReadToEnd();
                if (!rete.Equals(""))
                {
                    throw new Exception("u fucked up");
                }
            }
        }
        private static string AddRoleToChannel(long channelid, long roleid)
        {
            WebRequest channel = WebRequest.Create("https://discordapp.com/api/channels/" + channelid);
            channel.Method = "GET";
            channel.Headers.Add("Authorization: Bot NDIzMDQ5NjAyNzMyNzg1NjY1.DYksOw.iUAbB-FHu6yM4NPO6-z7u3dHhC4");
            WebResponse channelResp = channel.GetResponse();
            Stream responseStream = channelResp.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string rete = reader.ReadToEnd();
            JObject obje = JObject.Parse(rete);
            var permissions = obje["permission_overwrites"] as JArray;
            var token = JToken.Parse("{\"id\":" + roleid + ", \"allow\":36703232, \"type\":\"role\" }");
            permissions.Add(token);
            obje["permission_overwrites"] = permissions;
            WebRequest request = WebRequest.Create("https://discordapp.com/api/channels/" + channelid);
            request.Method = "PUT";
            request.Headers.Add("Authorization: Bot NDIzMDQ5NjAyNzMyNzg1NjY1.DYksOw.iUAbB-FHu6yM4NPO6-z7u3dHhC4");
            request.ContentType = "application/json";
            var body = obje.ToString();
            byte[] data = Encoding.UTF8.GetBytes(body);
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            // Reading the id of the new generated channel
            WebResponse response = request.GetResponse();
            Stream s = response.GetResponseStream();
            StreamReader read = new StreamReader(s);
            string ret = read.ReadToEnd();
            JObject obj = JObject.Parse(ret);
            return obj.ToString();
        }
        private static long CreateChannel(string channelname, long server)
        {
            //Creating the reqest for a new channel
            WebRequest request = WebRequest.Create("https://discordapp.com/api/guilds/"+server+"/channels");
            request.Method = "POST";
            request.Headers.Add("Authorization: Bot NDIzMDQ5NjAyNzMyNzg1NjY1.DYksOw.iUAbB-FHu6yM4NPO6-z7u3dHhC4");
            request.ContentType = "application/json";
            //The post body contains the new channel name and the id of the parent channel
            var body = "{\"name\":\"" + channelname + "\",\"type\":2,\"parent_id\":\"382126402880929794\"}";
            byte[] data = Encoding.UTF8.GetBytes(body);
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            // Reading the id of the new generated channel
            WebResponse response = request.GetResponse();
            Stream s = response.GetResponseStream();
            StreamReader read = new StreamReader(s);
            string ret = read.ReadToEnd();
            JObject obj = JObject.Parse(ret);
            long id = long.Parse(obj["id"].ToString());
            return id;
        }
        private static string CreateChannelInvite(long channelid, long server)
        {
            //The body contains the id of the new channel, which the invite is generated for
            string body = "{\"code\": \"0vCdhLbwjZZTWZLD\", \"guild\": {\"id\": \""+server+"\" }, \"channel\": {\"id\": \"" + channelid + "\"}}";
            byte[] data = Encoding.UTF8.GetBytes(body);
            //Creating the POST to the JSON API for generating a channel invite code
            WebRequest request = WebRequest.Create("https://discordapp.com/api/channels/" + channelid + "/invites");
            request.Method = "POST";
            //The authorisation header is used to validate the request
            request.Headers.Add("Authorization: Bot NDIzMDQ5NjAyNzMyNzg1NjY1.DYksOw.iUAbB-FHu6yM4NPO6-z7u3dHhC4");
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            //
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            // Reading the JSON Object which contains the invite code
            WebResponse response = request.GetResponse();
            Stream s = response.GetResponseStream();
            StreamReader read = new StreamReader(s);
            string ret = read.ReadToEnd();
            JObject json = JObject.Parse(ret);
            string code = (string)json["code"];
            return code;
        }
        private static long CreateRoleForLobby(long channelID, long server)
        {
            //JSON Object containing the channelid and the permissions which should be set
            string body = "{\"name\": \"Role for Lobby" + channelID + "\",\"hoist\": true, \"permissions\": 36703232,\"mentionable\": true}";
            //Creating the POST to the JSON API for changing the channel permissions
            WebRequest request = WebRequest.Create("https://discordapp.com/api/guilds/"+server+"/roles");
            request.Method = "POST";
            //The authorisation header is used to validate the request
            request.Headers.Add("Authorization: Bot NDIzMDQ5NjAyNzMyNzg1NjY1.DYksOw.iUAbB-FHu6yM4NPO6-z7u3dHhC4");
            request.ContentType = "application/json";
            byte[] data = Encoding.UTF8.GetBytes(body);
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            WebResponse response = request.GetResponse();
            Stream s = response.GetResponseStream();
            StreamReader read = new StreamReader(s);
            string ret = read.ReadToEnd();
            JObject obj = JObject.Parse(ret);
            long id = long.Parse(obj["id"].ToString());
            return id;
        }
        private static void EditEveryonePermissions(long channelid)
        {
            string body = "{ \"type\":\"role\", \"deny\":3145728 ,\"allow\":0  }";
            WebRequest request = WebRequest.Create("https://discordapp.com/api/channels/" + channelid + "/permissions/382126402444460032");
            request.Method = "PUT";
            //The authorisation header is used to validate the request
            request.Headers.Add("Authorization: Bot NDIzMDQ5NjAyNzMyNzg1NjY1.DYksOw.iUAbB-FHu6yM4NPO6-z7u3dHhC4");
            request.ContentType = "application/json";
            byte[] data = Encoding.UTF8.GetBytes(body);
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
        }
    }
}
