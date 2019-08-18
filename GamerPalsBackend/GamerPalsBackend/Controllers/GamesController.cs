using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamerPalsBackend.DataObjects;
using GamerPalsBackend.DataObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GamerPalsBackend.Controllers
{
    [Route("api/Game")]
    [ApiController]
    [Authorize(Roles = Role.VerifiedBlank)]
    public class GamesController : AbstractPalsController<Game>
    {
        public GamesController(MongoContext context) : base(context)
        {
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
            return Ok(await base.GetSingle(id));
        }

        // POST: api/Default
        [HttpPost]
        [Authorize(Roles = Role.AdminBlank)]
        public async Task<IActionResult> Post([FromBody] Game value)
        {
            return Ok(await base.PostBase(value));
        }

        // PUT: api/Default/5
        [HttpPut("{id}")]
        [Authorize(Roles = Role.AdminBlank)]
        public async Task<IActionResult> Put([FromRoute]string id, [FromBody] string document)
        {
            var res =  await base.PutBase(id, document);
            if (res.HasValue)
            {
                if (res.Value)
                {
                    return Ok();
                }
                else
                {
                    return Conflict();
                }
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [Authorize(Roles = Role.AdminBlank)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var res = await base.DeleteBase(id);
            if (res.HasValue)
            {
                if (res.Value)
                {
                    return Ok();
                }
                else
                {
                    return Conflict();
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
