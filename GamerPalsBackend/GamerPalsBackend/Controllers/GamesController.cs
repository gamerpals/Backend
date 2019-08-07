using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamerPalsBackend.DataObjects;
using GamerPalsBackend.DataObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GamerPalsBackend.Controllers
{
    [Route("api/Game")]
    [ApiController]
    public class GamesController : AbstractPalsController<Game>
    {
        private MongoContext _context;
        public GamesController(MongoContext context) : base(context)
        {
            _context = context;
        }
        // GET: api/Default
        [HttpGet]
        public async Task<List<Game>> Get()
        {
            return await base.GetAll();
        }

        // GET: api/Default/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            return await base.GetSingle(id);
        }

        // POST: api/Default
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Game value)
        {
            return await base.PostBase(value);
        }

        // PUT: api/Default/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]string id, [FromBody] string document)
        {
            return await base.PutBase(id, document);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            return await base.DeleteBase(id);
        }
    }
}
