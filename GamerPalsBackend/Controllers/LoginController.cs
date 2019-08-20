using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using GamerpalsBackend.DataObjects;
using GamerPalsBackend.DataObjects.Models;
using Microsoft.IdentityModel.Tokens;
using GamerPalsBackend.DTOs;
using GamerPalsBackend.Managers;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using GamerPalsBackend.DataObjects;
using GamerPalsBackend.Mongo;
using MongoDB.Driver;

namespace GamerPalsBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private static MongoContext _context;
        public LoginController(MongoContext context)
        {
            _context = context;
        }
        // POST: Login
        [HttpPost]
        public async Task<ActionResult> LoginAsync([FromBody] Login login)
        {
            User user = null;
            if (login.Type != 2)
            {
                var payload = GoogleJsonWebSignature.ValidateAsync(login.Token).Result;
                user = await Authenticate(payload.Subject, login.Type) ?? CreateUser(payload.Subject).Result;
            }
            else
            {
                user = await Authenticate(login.Token, 2);
            }
            
            if (user != null)
            {
                return new OkObjectResult(user);
            }
            else
            {
                return NotFound();
            }
        }

        private async Task<User> CreateUser(string googletoken)
        {
            User u = new User
            {
                GoogleId = googletoken,
                CreateTime = DateTime.Now,
                ProfileComplete = false
            };
            await _context.Users.InsertOneAsync(u);
            return u;
        }
        private async Task<User> Authenticate(string id, int type)
        {
            User user = null;
            try
            {
                if (type == 1)
                {
                    user = _context.Users.Find(u => u.GoogleId.Equals(id)).Single();
                }
                else if (type == 2)
                {
                    user = _context.Users.Find(u => u.GoogleId.Equals(id)).Single();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = _context.GetClaimsForUser(user._id),
                Expires = type != 2 ? DateTime.UtcNow.AddMinutes(10) : DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenText = tokenHandler.WriteToken(token);
            user.CurrentSession = new CurrentSession {SessionToken = tokenText, ValidTo = tokenDescriptor.Expires.Value };
            await new MongoHelper<User>(_context).Update(user._id, user);
            return user;
        }
    }
}