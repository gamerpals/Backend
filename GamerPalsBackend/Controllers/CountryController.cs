using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamerPalsBackend.DataObjects;
using GamerpalsBackend.DataObjects.Models;
using GamerPalsBackend.DataObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GamerPalsBackend.Controllers
{
    [Route("api/Country")]
    [ApiController]
    [Authorize(Roles = Role.VerifiedBlank)]
    public class CountriesController : ControllerBase
    {
        private ControllerHelper<Country> cont;
        public CountriesController(MongoContext context)
        {
            cont = new ControllerHelper<Country>(context);
        }
        // GET: api/Default
        [HttpGet]
        public async Task<List<Country>> Get()
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
        [Authorize(Roles = Role.AdminBlank)]
        public async Task<IActionResult> Post([FromBody] Country value)
        {
            return Ok(await cont.Create(value));
        }

        // PUT: api/Default/5
        [HttpPut("{id}")]
        [Authorize(Roles = Role.AdminBlank)]
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
        [Authorize(Roles = Role.AdminBlank)]
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
