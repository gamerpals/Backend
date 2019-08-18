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
    [Route("api/SearchParameter")]
    [ApiController]
    [Authorize(Roles = Role.AdminBlank)]
    public class SearchParametersController : AbstractPalsController<SearchParameter>
    {
        public SearchParametersController(MongoContext context) : base(context)
        {

        }
        // GET: api/Default
        [HttpGet]
        public async Task<List<SearchParameter>> Get()
        {
            return await base.GetAll();
        }

        // GET: api/Default/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await base.GetSingle(id));
        }

        // POST: api/Default
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SearchParameter value)
        {
            return Ok(await base.PostBase(value));
        }

        // PUT: api/Default/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]string id, [FromBody] string document)
        {
            var res = await base.PutBase(id, document);
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
            var res =  await base.DeleteBase(id);
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
