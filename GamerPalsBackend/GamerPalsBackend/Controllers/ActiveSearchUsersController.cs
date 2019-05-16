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
    public class ActiveSearchUsersController : ControllerBase
    {
        private readonly PalsContext _context;

        public ActiveSearchUsersController(PalsContext context)
        {
            _context = context;
        }

        // GET: api/ActiveSearchUsers
        [HttpGet]
        public IEnumerable<ActiveSearchUser> GetActiveSearchUsers()
        {
            return _context.ActiveSearchUsers;
        }

        // GET: api/ActiveSearchUsers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActiveSearchUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activeSearchUser = await _context.ActiveSearchUsers.FindAsync(id);

            if (activeSearchUser == null)
            {
                return NotFound();
            }

            return Ok(activeSearchUser);
        }

        // PUT: api/ActiveSearchUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActiveSearchUser([FromRoute] int id, [FromBody] ActiveSearchUser activeSearchUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activeSearchUser.ActiveSearchID)
            {
                return BadRequest();
            }

            _context.Entry(activeSearchUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActiveSearchUserExists(id))
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

        // POST: api/ActiveSearchUsers
        [HttpPost]
        public async Task<IActionResult> PostActiveSearchUser([FromBody] ActiveSearchUser activeSearchUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ActiveSearchUsers.Add(activeSearchUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ActiveSearchUserExists(activeSearchUser.ActiveSearchID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetActiveSearchUser", new { id = activeSearchUser.ActiveSearchID }, activeSearchUser);
        }

        // DELETE: api/ActiveSearchUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActiveSearchUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activeSearchUser = await _context.ActiveSearchUsers.FindAsync(id);
            if (activeSearchUser == null)
            {
                return NotFound();
            }

            _context.ActiveSearchUsers.Remove(activeSearchUser);
            await _context.SaveChangesAsync();

            return Ok(activeSearchUser);
        }

        private bool ActiveSearchUserExists(int id)
        {
            return _context.ActiveSearchUsers.Any(e => e.ActiveSearchID == id);
        }
    }
}