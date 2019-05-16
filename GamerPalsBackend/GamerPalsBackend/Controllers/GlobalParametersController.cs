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
    public class GlobalParametersController : ControllerBase
    {
        private readonly PalsContext _context;

        public GlobalParametersController(PalsContext context)
        {
            _context = context;
        }

        // GET: api/GlobalParameters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GlobalParameters>>> GetGlobalParameters()
        {
            return await _context.GlobalParameters.ToListAsync();
        }

        // GET: api/GlobalParameters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GlobalParameters>> GetGlobalParameters(int id)
        {
            var globalParameters = await _context.GlobalParameters.FindAsync(id);

            if (globalParameters == null)
            {
                return NotFound();
            }

            return globalParameters;
        }

        // PUT: api/GlobalParameters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGlobalParameters(int id, GlobalParameters globalParameters)
        {
            if (id != globalParameters.GlobalParametersID)
            {
                return BadRequest();
            }

            _context.Entry(globalParameters).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GlobalParametersExists(id))
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

        // POST: api/GlobalParameters
        [HttpPost]
        public async Task<ActionResult<GlobalParameters>> PostGlobalParameters(GlobalParameters globalParameters)
        {
            _context.GlobalParameters.Add(globalParameters);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGlobalParameters", new { id = globalParameters.GlobalParametersID }, globalParameters);
        }

        // DELETE: api/GlobalParameters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GlobalParameters>> DeleteGlobalParameters(int id)
        {
            var globalParameters = await _context.GlobalParameters.FindAsync(id);
            if (globalParameters == null)
            {
                return NotFound();
            }

            _context.GlobalParameters.Remove(globalParameters);
            await _context.SaveChangesAsync();

            return globalParameters;
        }

        private bool GlobalParametersExists(int id)
        {
            return _context.GlobalParameters.Any(e => e.GlobalParametersID == id);
        }
    }
}
