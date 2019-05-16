using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamerPalsBackend.DataObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamerPalsBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/SearchTypes")]
    public class SearchTypesController : Controller
    {
        private readonly PalsContext _context;

        public SearchTypesController(PalsContext context)
        {
            _context = context;
        }

        // GET: api/SearchTypes
        [HttpGet]
        public IEnumerable<SearchType> GetSearchTypes()
        {
            return _context.SearchTypes;
        }

        // GET: api/SearchTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSearchType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var searchType = await _context.SearchTypes.SingleOrDefaultAsync(m => m.SearchTypeID == id);

            if (searchType == null)
            {
                return NotFound();
            }

            return Ok(searchType);
        }

        // PUT: api/SearchTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSearchType([FromRoute] int id, [FromBody] SearchType searchType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != searchType.SearchTypeID)
            {
                return BadRequest();
            }

            _context.Entry(searchType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SearchTypeExists(id))
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

        // POST: api/SearchTypes
        [HttpPost]
        public async Task<IActionResult> PostSearchType([FromBody] SearchType searchType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SearchTypes.Add(searchType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSearchType", new { id = searchType.SearchTypeID }, searchType);
        }

        // DELETE: api/SearchTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSearchType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var searchType = await _context.SearchTypes.SingleOrDefaultAsync(m => m.SearchTypeID == id);
            if (searchType == null)
            {
                return NotFound();
            }

            _context.SearchTypes.Remove(searchType);
            await _context.SaveChangesAsync();

            return Ok(searchType);
        }

        private bool SearchTypeExists(int id)
        {
            return _context.SearchTypes.Any(e => e.SearchTypeID == id);
        }
    }
}