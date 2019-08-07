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
    [Route("api/ActiveSearch")]
    [ApiController]
    public class ActiveSearchesController : AbstractPalsController<ActiveSearch>
    {
        private MongoContext _context;
        public ActiveSearchesController(MongoContext context) : base(context)
        {
            _context = context;
        }
        // GET: api/Default
        [HttpGet]
        public async Task<List<ActiveSearch>> Get()
        {
            return await base.GetAll();
        }

        // GET: api/Default/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return await base.GetSingle(id);
        }

        // POST: api/Default
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ActiveSearch value)
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
        public async Task<IActionResult> Delete(string id)
        {
            return await base.DeleteBase(id);
        }
    }
}
