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
    public class UserLanguagesController : ControllerBase
    {
        private readonly PalsContext _context;

        public UserLanguagesController(PalsContext context)
        {
            _context = context;
        }

        // GET: api/UserLanguages
        [HttpGet]
        public IEnumerable<UserLanguage> GetUserLanguages()
        {
            return _context.UserLanguages;
        }

        // GET: api/UserLanguages/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserLanguage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userLanguage = await _context.UserLanguages.FindAsync(id);

            if (userLanguage == null)
            {
                return NotFound();
            }

            return Ok(userLanguage);
        }

        // PUT: api/UserLanguages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserLanguage([FromRoute] int id, [FromBody] UserLanguage userLanguage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userLanguage.LanguageID)
            {
                return BadRequest();
            }

            _context.Entry(userLanguage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLanguageExists(id))
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

        // POST: api/UserLanguages
        [HttpPost]
        public async Task<IActionResult> PostUserLanguage([FromBody] UserLanguage userLanguage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserLanguages.Add(userLanguage);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserLanguageExists(userLanguage.LanguageID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserLanguage", new { id = userLanguage.LanguageID }, userLanguage);
        }

        // DELETE: api/UserLanguages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserLanguage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userLanguage = await _context.UserLanguages.FindAsync(id);
            if (userLanguage == null)
            {
                return NotFound();
            }

            _context.UserLanguages.Remove(userLanguage);
            await _context.SaveChangesAsync();

            return Ok(userLanguage);
        }

        private bool UserLanguageExists(int id)
        {
            return _context.UserLanguages.Any(e => e.LanguageID == id);
        }
    }
}