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
    [Route("api/ActiveSearch")]
    [ApiController]
    [Authorize(Roles = Role.VerifiedBlank)]
    public class ActiveSearchesController : ControllerBase
    {
        private ControllerHelper<ActiveSearch> cont;
        public ActiveSearchesController(MongoContext context)
        {
            cont = new ControllerHelper<ActiveSearch>(context);
        }
        // GET: api/Default
        [HttpGet]
        public async Task<List<ActiveSearch>> Get()
        {
            return await cont.FetchAll();
        }

        //GET: api/Default/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await cont.FetchSingle(id));
        }

        // POST: api/Default
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ActiveSearch value)
        {
            return Ok(await cont.Create(value));
        }

        // PUT: api/Default/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]string id, [FromBody] string document)
        {
            var res = await cont.Edit(id, document);
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
        public async Task<IActionResult> Delete(string id)
        {
            var res = await cont.Remove(id);
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
