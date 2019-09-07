using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamerPalsBackend.Controllers;
using GamerPalsBackend.DataObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GamerPalsBackend.Controllers
{
    [Route("api/SystemSetting")]
    [ApiController]
    [Authorize(Roles = Role.AdminBlank)]
    public class SystemSettingsController : ControllerBase
    {
        private ControllerHelper<SystemSetting> cont;
        public SystemSettingsController(MongoContext context)
        {
            cont = new ControllerHelper<SystemSetting>(context);
        }
        // GET: api/Default
        [HttpGet]
        public async Task<List<SystemSetting>> Get()
        {
            return await cont.FetchAll();
        }

        // GET: api/Default/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            return Ok(await cont.FetchSingle(id));
        }

        // POST: api/Default
        [HttpPost]
        [Authorize(Roles = Role.AdminBlank)]
        public async Task<IActionResult> Post([FromBody] SystemSetting value)
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
        public async Task<IActionResult> Delete([FromRoute] string id)
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
