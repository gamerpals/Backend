using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GamerPalsBackend.DataObjects.Models;
using GamerPalsBackend.Mongo;
using GamerPalsBackend.Other;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;

namespace GamerPalsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectController : ControllerBase
    {
        private readonly MongoHelper<User> helper;
        public ConnectController(MongoContext context)
        {
            helper = new MongoHelper<User>(context);
        }

        [HttpPost]
        [Route("Discord/{userId}")]
        public async Task<IActionResult> ConnectDiscordAsync([FromRoute] string userId, string code)
        {
            var user = helper.Get(new ObjectId(userId)).Result;
            if (user.ConnectedServices?.ContainsKey("Discord") ?? false)
            {
                return Conflict();
            }
            else
            {

                string redirect_url = "http://localhost:8088/Discord/";

                /*Get Access Token from authorization code by making http post request*/

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://discordapp.com/api/oauth2/token");
                webRequest.Method = "POST";
                string parameters = "client_id=" + PalsConfiguration.SystemSettings["DiscordClientID"] + "&client_secret=" + PalsConfiguration.SystemSettings["DiscordClientSecret"] + "&grant_type=authorization_code&code=" + code + "&redirect_uri=" + redirect_url;
                byte[] byteArray = Encoding.UTF8.GetBytes(parameters);
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = byteArray.Length;
                Stream postStream = webRequest.GetRequestStream();

                postStream.Write(byteArray, 0, byteArray.Length);
                postStream.Close();
                WebResponse response = webRequest.GetResponse();
                postStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(postStream);
                string responseFromServer = reader.ReadToEnd();

                string tokenInfo = responseFromServer.Split(',')[0].Split(':')[1];
                string access_token = tokenInfo.Trim().Substring(1, tokenInfo.Length - 3);

                HttpWebRequest userRequest = (HttpWebRequest)WebRequest.Create("https://discordapp.com/api/users/@me");
                userRequest.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + access_token);
                WebResponse resp = userRequest.GetResponse();
                StreamReader userReader = new StreamReader(resp.GetResponseStream());
                string userResponse = userReader.ReadToEnd();
                var id = JObject.Parse(userResponse)["id"].ToString();
                user.ConnectedServices.Add("Discord", id);
                await helper.Update(user._id, user);
            }
            return NotFound();
        }
    }
}