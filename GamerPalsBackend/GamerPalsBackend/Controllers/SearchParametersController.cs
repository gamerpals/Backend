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
    public class SearchParametersController : ControllerBase
    {
        private readonly PalsContext _context;

        public SearchParametersController(PalsContext context)
        {
            _context = context;
        }

        // GET: api/SearchParameters
        [HttpGet]
        public IEnumerable<SearchParameter> GetSearchParameters()
        {
            return _context.SearchParameters;
        }

        // GET: api/SearchParameters/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSearchParameter([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var searchParameter = await _context.SearchParameters.FindAsync(id);

            if (searchParameter == null)
            {
                return NotFound();
            }

            return Ok(searchParameter);
        }

        // PUT: api/SearchParameters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSearchParameter([FromRoute] int id, [FromBody] SearchParameter searchParameter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != searchParameter.ActiveSearchID)
            {
                return BadRequest();
            }

            _context.Entry(searchParameter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SearchParameterExists(id))
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

        // POST: api/SearchParameters
        [HttpPost]
        public async Task<IActionResult> PostSearchParameter([FromBody] SearchParameter searchParameter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SearchParameters.Add(searchParameter);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SearchParameterExists(searchParameter.ActiveSearchID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSearchParameter", new { id = searchParameter.ActiveSearchID }, searchParameter);
        }

        // DELETE: api/SearchParameters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSearchParameter([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var searchParameter = await _context.SearchParameters.FindAsync(id);
            if (searchParameter == null)
            {
                return NotFound();
            }

            _context.SearchParameters.Remove(searchParameter);
            await _context.SaveChangesAsync();

            return Ok(searchParameter);
        }

        private bool SearchParameterExists(int id)
        {
            return _context.SearchParameters.Any(e => e.ActiveSearchID == id);
        }
    }
}