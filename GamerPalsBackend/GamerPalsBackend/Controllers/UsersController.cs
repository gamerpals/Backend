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
    public class UsersController : ControllerBase
    {
        private ControllerHelper<User> cont;
        private IAuthorizationService authorization;
        public UsersController(MongoContext context, IAuthorizationService auth)
        {
            cont = new ControllerHelper<User>(context);
            authorization = auth;
        }
        // GET: api/Default
        [HttpGet]
        public async Task<List<User>> Get()
        {
            return await cont.FetchAll();
        }

        // GET: api/Default/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            if (!authorization.AuthorizeAsync(User, new ObjectId(id), "IsOwnerPolicy").Result.Succeeded)
            {
                if (!authorization.AuthorizeAsync(User, new ObjectId(id), "IsInFriendsListPolicy").Result.Succeeded)
                {
                    return Ok(AnonymizeUserData(cont.FetchSingle(id).Result, false));
                }
                else
                {
                    return Ok(AnonymizeUserData(cont.FetchSingle(id).Result, true));
                }
            }
            
            return Ok(await cont.FetchSingle(id));
        }

        // POST: api/Default
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User value)
        {
            return Ok(await cont.Create(value));
        }

        // PUT: api/Default/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]string id, [FromBody] object document)
        {
            var permission = await authorization.AuthorizeAsync(User, new ObjectId(id), new IsOwnerPolicyRequirements());
            if (!permission.Succeeded)
            {
                return Forbid();
            }

            var res = await cont.Edit(id, document.ToString());
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

        private User AnonymizeUserData(User u, bool friend)
        {
            if (!friend)
            {
                u.OnlineStatus = "";
                u.ActiveSearches = null;
                u.PassiveSearches = null;
            }
            u.CurrentSession = null;
            u.GoogleId = null;
            u.Karma = null;
            u.GamesSelected = null;
            u.FriendsList = null;
            u.RecievedFriendRequests = null;
            u.SentFriendRequests = null;
            u.PrivateChats = null;
            u.Notifications = null;
            u.ConnectedServices = null;
            u.ProfileComplete = null;
            return u;
        }
    }
}
