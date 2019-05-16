using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamerPalsBackend.DataObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GamerPalsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGamesController : ControllerBase
    {
        private readonly PalsContext _context;

        public UserGamesController(PalsContext context)
        {
            _context = context;
        }

        // GET: api/UserGames
        [HttpGet]
        public IEnumerable<UserGame> GetUserGame()
        {
            return _context.UserGame;
        }

        // GET: api/UserGames/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserGame([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userGame = await _context.UserGame.FindAsync(id);

            if (userGame == null)
            {
                return NotFound();
            }

            return Ok(userGame);
        }

        // PUT: api/UserGames/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserGame([FromRoute] int id, [FromBody] UserGame userGame)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userGame.GameID)
            {
                return BadRequest();
            }

            _context.Entry(userGame).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserGameExists(id))
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

        // POST: api/UserGames
        [HttpPost]
        public async Task<IActionResult> PostUserGame([FromBody] UserGame userGame)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserGame.Add(userGame);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserGameExists(userGame.GameID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserGame", new { id = userGame.GameID }, userGame);
        }

        // DELETE: api/UserGames/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserGame([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userGame = await _context.UserGame.FindAsync(id);
            if (userGame == null)
            {
                return NotFound();
            }

            _context.UserGame.Remove(userGame);
            await _context.SaveChangesAsync();

            return Ok(userGame);
        }

        private bool UserGameExists(int id)
        {
            return _context.UserGame.Any(e => e.GameID == id);
        }
    }
}