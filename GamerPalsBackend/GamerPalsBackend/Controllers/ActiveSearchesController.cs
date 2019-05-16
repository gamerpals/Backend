using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamerPalsBackend.DataObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace GamerPalsBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/ActiveSearches")]
    [Authorize(Roles = "Verified")]
    public class ActiveSearchesController : Controller
    {
        private readonly PalsContext _context;

        public ActiveSearchesController(PalsContext context)
        {
            _context = context;
        }

        // GET: api/ActiveSearches
        [HttpGet]
        public IEnumerable<ActiveSearch> GetActiveSearches()
        {
            return _context.ActiveSearches.Include(b => b.JoinedUsers).ThenInclude(joins => joins.User).Include(b => b.Parameters).Include(b => b.Owner);
        }

        // GET: api/ActiveSearches/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActiveSearch([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var activeSearch = await _context.ActiveSearches.SingleOrDefaultAsync(m => m.ActiveSearchID == id);

            if (activeSearch == null)
            {
                return NotFound();
            }

            return Ok(activeSearch);
        }

        // PUT: api/ActiveSearches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActiveSearch([FromRoute] int id, [FromBody] ActiveSearch activeSearch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activeSearch.ActiveSearchID)
            {
                return BadRequest();
            }

            _context.Entry(activeSearch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActiveSearchExists(id))
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

        // POST: api/ActiveSearches
        [HttpPost]
        public async Task<IActionResult> PostActiveSearch([FromBody] ActiveSearch activeSearch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ActiveSearches.Add(activeSearch);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActiveSearch", new { id = activeSearch.ActiveSearchID }, activeSearch);
        }

        // DELETE: api/ActiveSearches/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "LoggedIn")]
        public async Task<IActionResult> DeleteActiveSearch([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activeSearch = await _context.ActiveSearches.SingleOrDefaultAsync(m => m.ActiveSearchID == id);
            if (activeSearch == null)
            {
                return NotFound();
            }

            _context.ActiveSearches.Remove(activeSearch);
            await _context.SaveChangesAsync();

            return Ok(activeSearch);
        }

        private bool ActiveSearchExists(int id)
        {
            return _context.ActiveSearches.Any(e => e.ActiveSearchID == id);
        }
    }
}