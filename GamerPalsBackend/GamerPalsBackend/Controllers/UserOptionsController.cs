using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamerPalsBackend.DataObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GamerPalsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOptionsController : ControllerBase
    {
        private readonly PalsContext _context;

        public UserOptionsController(PalsContext context)
        {
            _context = context;
        }

        // GET: api/UserOptions
        [HttpGet]
        public IEnumerable<UserOptions> GetUserOptions()
        {
            return _context.UserOptions;
        }

        // GET: api/UserOptions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserOptions([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userOptions = await _context.UserOptions.FindAsync(id);

            if (userOptions == null)
            {
                return NotFound();
            }

            return Ok(userOptions);
        }

        // PUT: api/UserOptions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserOptions([FromRoute] int id, [FromBody] UserOptions userOptions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userOptions.UserOptionsID)
            {
                return BadRequest();
            }

            _context.Entry(userOptions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserOptionsExists(id))
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

        // POST: api/UserOptions
        [HttpPost]
        public async Task<IActionResult> PostUserOptions([FromBody] UserOptions userOptions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserOptions.Add(userOptions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserOptions", new { id = userOptions.UserOptionsID }, userOptions);
        }

        // DELETE: api/UserOptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserOptions([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userOptions = await _context.UserOptions.FindAsync(id);
            if (userOptions == null)
            {
                return NotFound();
            }

            _context.UserOptions.Remove(userOptions);
            await _context.SaveChangesAsync();

            return Ok(userOptions);
        }

        private bool UserOptionsExists(int id)
        {
            return _context.UserOptions.Any(e => e.UserOptionsID == id);
        }
    }
}