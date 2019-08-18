using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamerPalsBackend.DataObjects;
using GamerPalsBackend.DataObjects.Models;
using GamerPalsBackend.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GamerPalsBackend.Controllers
{
    [Route("api/User")]
    [ApiController]
    [Authorize(Roles = Role.VerifiedBlank)]
    public class UsersController : AbstractPalsController<User>
    {
        public UsersController(MongoContext context, IAuthorizationService auth) : base(context, auth)
        {
        }
        // GET: api/Default
        [HttpGet]
        public async Task<List<User>> Get()
        {
            return await base.GetAll();
        }

        // GET: api/Default/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            if (!auth.AuthorizeAsync(User, new ObjectId(id), "IsOwnerPolicy").Result.Succeeded)
            {
                return Ok(AnonymizeUserData(GetSingle(id).Result));
            }
            return Ok(await base.GetSingle(id));
        }

        // POST: api/Default
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User value)
        {
            return Ok(await base.PostBase(value));
        }

        // PUT: api/Default/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]string id, [FromBody] object document)
        {
            var permission = await auth.AuthorizeAsync(User, new ObjectId(id), new IsOwnerPolicyRequirements());
            if (!permission.Succeeded)
            {
                return Forbid();
            }

            var res = await base.PutBase(id, document.ToString());
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

        private User AnonymizeUserData(User u)
        {
            u.CurrentSession = null;
            return u;
        }
    }
}
