using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamerPalsBackend.DataObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GamerPalsBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/GameServers")]
    public class GameServersController : Controller
    {
        private readonly PalsContext _context;

        public GameServersController(PalsContext context)
        {
            _context = context;
        }

        // GET: api/GameServers
        [HttpGet]
        public IEnumerable<GameServer> GetServers()
        {
            return _context.Servers;
        }

        // GET: api/GameServers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameServer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gameServer = await _context.Servers.SingleOrDefaultAsync(m => m.GameServerID == id);

            if (gameServer == null)
            {
                return NotFound();
            }

            return Ok(gameServer);
        }

        // PUT: api/GameServers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameServer([FromRoute] int id, [FromBody] GameServer gameServer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gameServer.GameServerID)
            {
                return BadRequest();
            }

            _context.Entry(gameServer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameServerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GameServers
        [HttpPost]
        public async Task<IActionResult> PostGameServer([FromBody] GameServer gameServer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Servers.Add(gameServer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGameServer", new { id = gameServer.GameServerID }, gameServer);
        }

        // DELETE: api/GameServers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameServer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gameServer = await _context.Servers.SingleOrDefaultAsync(m => m.GameServerID == id);
            if (gameServer == null)
            {
                return NotFound();
            }

            _context.Servers.Remove(gameServer);
            await _context.SaveChangesAsync();

            return Ok(gameServer);
        }

        private bool GameServerExists(int id)
        {
            return _context.Servers.Any(e => e.GameServerID == id);
        }
    }
}