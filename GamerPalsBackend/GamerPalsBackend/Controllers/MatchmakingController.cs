using System.Collections.Generic;
using System.Linq;
using GamerPalsBackend.DataObjects.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace GamerPalsBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/Matchmaking")]
    public class MatchmakingController : Controller
    {
        private readonly MongoContext _context;

        public MatchmakingController(MongoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IEnumerable<ActiveSearch> DoMatchmaking(ActiveSearch search)
        {
            var matches = _context.ActiveSearchs.Find(s => s.SearchingGame == search.SearchingGame).ToList().Where(s =>
                search.Parameters.TrueForAll(s.Parameters.Contains));
            return matches;
        }
    }
}