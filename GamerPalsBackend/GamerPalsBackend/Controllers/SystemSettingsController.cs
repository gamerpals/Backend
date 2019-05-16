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
    public class SystemSettingsController : ControllerBase
    {
        private readonly PalsContext _context;

        public SystemSettingsController(PalsContext context)
        {
            _context = context;
        }

        // GET: api/SystemSettings
        [HttpGet]
        public IEnumerable<SystemSettings> GetSystemSettings()
        {
            return _context.SystemSettings;
        }

        // GET: api/SystemSettings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSystemSettings([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var systemSettings = await _context.SystemSettings.FindAsync(id);

            if (systemSettings == null)
            {
                return NotFound();
            }

            return Ok(systemSettings);
        }

        // PUT: api/SystemSettings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSystemSettings([FromRoute] int id, [FromBody] SystemSettings systemSettings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != systemSettings.SystemSettingsId)
            {
                return BadRequest();
            }

            _context.Entry(systemSettings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SystemSettingsExists(id))
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

        // POST: api/SystemSettings
        [HttpPost]
        public async Task<IActionResult> PostSystemSettings([FromBody] SystemSettings systemSettings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SystemSettings.Add(systemSettings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSystemSettings", new { id = systemSettings.SystemSettingsId }, systemSettings);
        }

        // DELETE: api/SystemSettings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSystemSettings([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var systemSettings = await _context.SystemSettings.FindAsync(id);
            if (systemSettings == null)
            {
                return NotFound();
            }

            _context.SystemSettings.Remove(systemSettings);
            await _context.SaveChangesAsync();

            return Ok(systemSettings);
        }

        private bool SystemSettingsExists(int id)
        {
            return _context.SystemSettings.Any(e => e.SystemSettingsId == id);
        }
    }
}