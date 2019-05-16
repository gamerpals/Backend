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
    public class UserOptionRolesController : ControllerBase
    {
        private readonly PalsContext _context;

        public UserOptionRolesController(PalsContext context)
        {
            _context = context;
        }

        // GET: api/UserOptionRoles
        [HttpGet]
        public IEnumerable<UserOptionRoles> GetUserOptionRoles()
        {
            return _context.UserOptionRoles;
        }

        // GET: api/UserOptionRoles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserOptionRoles([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userOptionRoles = await _context.UserOptionRoles.FindAsync(id);

            if (userOptionRoles == null)
            {
                return NotFound();
            }

            return Ok(userOptionRoles);
        }

        // PUT: api/UserOptionRoles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserOptionRoles([FromRoute] int id, [FromBody] UserOptionRoles userOptionRoles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userOptionRoles.UserOptionId)
            {
                return BadRequest();
            }

            _context.Entry(userOptionRoles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserOptionRolesExists(id))
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

        // POST: api/UserOptionRoles
        [HttpPost]
        public async Task<IActionResult> PostUserOptionRoles([FromBody] UserOptionRoles userOptionRoles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserOptionRoles.Add(userOptionRoles);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserOptionRolesExists(userOptionRoles.UserOptionId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserOptionRoles", new { id = userOptionRoles.UserOptionId }, userOptionRoles);
        }

        // DELETE: api/UserOptionRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserOptionRoles([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userOptionRoles = await _context.UserOptionRoles.FindAsync(id);
            if (userOptionRoles == null)
            {
                return NotFound();
            }

            _context.UserOptionRoles.Remove(userOptionRoles);
            await _context.SaveChangesAsync();

            return Ok(userOptionRoles);
        }

        private bool UserOptionRolesExists(int id)
        {
            return _context.UserOptionRoles.Any(e => e.UserOptionId == id);
        }
    }
}