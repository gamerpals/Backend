using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamerPalsBackend.DataObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GamerPalsBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/SearchSettings")]
    public class SearchSettingsController : Controller
    {
        private readonly PalsContext _context;

        public SearchSettingsController(PalsContext context)
        {
            _context = context;
        }

        // GET: api/SearchSettings
        [HttpGet]
        public IEnumerable<SearchSettings> GetSearchSettings()
        {
            return _context.SearchSettings;
        }

        // GET: api/SearchSettings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSearchSettings([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var searchSettings = await _context.SearchSettings.SingleOrDefaultAsync(m => m.SearchSettingsID == id);

            if (searchSettings == null)
            {
                return NotFound();
            }

            return Ok(searchSettings);
        }

        // PUT: api/SearchSettings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSearchSettings([FromRoute] int id, [FromBody] SearchSettings searchSettings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != searchSettings.SearchSettingsID)
            {
                return BadRequest();
            }

            _context.Entry(searchSettings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SearchSettingsExists(id))
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

        // POST: api/SearchSettings
        [HttpPost]
        public async Task<IActionResult> PostSearchSettings([FromBody] SearchSettings searchSettings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SearchSettings.Add(searchSettings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSearchSettings", new { id = searchSettings.SearchSettingsID }, searchSettings);
        }

        // DELETE: api/SearchSettings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSearchSettings([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var searchSettings = await _context.SearchSettings.SingleOrDefaultAsync(m => m.SearchSettingsID == id);
            if (searchSettings == null)
            {
                return NotFound();
            }

            _context.SearchSettings.Remove(searchSettings);
            await _context.SaveChangesAsync();

            return Ok(searchSettings);
        }

        private bool SearchSettingsExists(int id)
        {
            return _context.SearchSettings.Any(e => e.SearchSettingsID == id);
        }
    }
}