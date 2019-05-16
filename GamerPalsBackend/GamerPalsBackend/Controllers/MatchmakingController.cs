using System.Collections.Generic;
using System.Linq;
using GamerPalsBackend.DataObjects.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamerPalsBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/Matchmaking")]
    public class MatchmakingController : Controller
    {
        private readonly PalsContext _context;

        public MatchmakingController(PalsContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IEnumerable<ActiveSearch> DoMatchmaking(GameServer server, List<SearchParameter> parameters)
        {
            var matches = _context.ActiveSearches.Where(search => search.Server.GameServerID == server.GameServerID).Where(search =>
                search.Parameters.TrueForAll(parameters.Contains));
            return matches;
        }
    }
}