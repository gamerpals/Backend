using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamerPalsBackend.DataObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GamerPalsBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/Parameters")]
    public class ParametersController : Controller
    {
        private readonly PalsContext _context;

        public ParametersController(PalsContext context)
        {
            _context = context;
        }

        // GET: api/Parameters
        [HttpGet]
        public IEnumerable<Parameter> GetParameters()
        {
            return _context.Parameters;
        }

        // GET: api/Parameters/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetParameter([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parameter = await _context.Parameters.SingleOrDefaultAsync(m => m.ParameterID == id);

            if (parameter == null)
            {
                return NotFound();
            }

            return Ok(parameter);
        }

        // PUT: api/Parameters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParameter([FromRoute] int id, [FromBody] Parameter parameter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parameter.ParameterID)
            {
                return BadRequest();
            }

            _context.Entry(parameter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParameterExists(id))
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

        // POST: api/Parameters
        [HttpPost]
        public async Task<IActionResult> PostParameter([FromBody] Parameter parameter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Parameters.Add(parameter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParameter", new { id = parameter.ParameterID }, parameter);
        }

        // DELETE: api/Parameters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParameter([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parameter = await _context.Parameters.SingleOrDefaultAsync(m => m.ParameterID == id);
            if (parameter == null)
            {
                return NotFound();
            }

            _context.Parameters.Remove(parameter);
            await _context.SaveChangesAsync();

            return Ok(parameter);
        }

        private bool ParameterExists(int id)
        {
            return _context.Parameters.Any(e => e.ParameterID == id);
        }
    }
}