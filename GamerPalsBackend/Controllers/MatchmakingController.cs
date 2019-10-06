using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamerPalsBackend.DataObjects.Models;
using GamerPalsBackend.Managers;
using GamerPalsBackend.Mongo;
using GamerPalsBackend.WebSockets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GamerPalsBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/Matchmaking")]
    [Authorize(Roles = Role.VerifiedBlank)]
    public class MatchmakingController : Controller
    {
        private readonly MongoContext _context;
        private readonly MongoHelper<User> userHelper;
        private readonly MongoHelper<ActiveSearch> searchHelper;

        public MatchmakingController(MongoContext context)
        {
            _context = context;
            userHelper = new MongoHelper<User>(context);
            searchHelper = new MongoHelper<ActiveSearch>(context);
        }

        [HttpPost]
        public IEnumerable<ActiveSearch> DoMatchmaking(ActiveSearch search)
        {
            var matches = _context.ActiveSearchs.Find(s => s.SearchingGame == search.SearchingGame).ToList().Where(s =>
                search.Parameters.TrueForAll(s.Parameters.Contains));
            return matches;
        }

        [HttpPost]
        [Route("/Join")]
        public async Task<IActionResult> JoinActiveSearch(string id, string userid)
        {
            var search = await searchHelper.Get(new ObjectId(userid));
            search.JoinedUser.Add(new ObjectId(userid));
            var usr = await userHelper.Get(new ObjectId(userid));
            await new NotificationHub().SendNotification("User joined activeSearch",
                search.JoinedUser.Select(s => s.ToString()).Where( s => !s.Equals(id)).ToArray());
            var discId = long.Parse(usr.ConnectedServices["Discord"].ToString());
            if (discId == 0)
            {
                return Conflict("User has no connected Discord Account");
            }

            if (search.DiscordChannelId != 0)
            {
                search.AddUserToExistingChannel(discId);
            }
            
            if (await searchHelper.Update(search._id, search))
            {
                
                return Ok();
            }
            else
            {
                return Conflict("ActiveSearch update failed");
            }
        }


    }
}