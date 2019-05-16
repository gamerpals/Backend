using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using GamerPalsBackend.DataObjects.Models;
using Microsoft.IdentityModel.Tokens;
using GamerPalsBackend.DTOs;
using GamerPalsBackend.Managers;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;

namespace GamerPalsBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly PalsContext _context;
        public LoginController(PalsContext context)
        {
            _context = context;
        }
        // POST: Login
        [HttpPost]
        public ActionResult LoginAsync([FromBody] Login login)
        {
            var payload = GoogleJsonWebSignature.ValidateAsync(login.Token).Result;
            
            var user = Authenticate(payload.Subject, login.Type);
            
            if (user != null)
            {
                return new OkObjectResult(user);
            }
            else
            {
                return NotFound();
            }
        }

        private UserDTO CreateUser(string googletoken)
        {
            User u = new User
            {
                GoogleID = googletoken
            };
            return null;
        }
        private UserDTO Authenticate(string id, int type)
        {
            User user = null;if (type == 1)
            {
                user = _context.Users.SingleOrDefault(u => u.GoogleID.Equals(id));
            }
            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = _context.GetClaimsForUser(user.UserID),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenText = tokenHandler.WriteToken(token);
            
            return new UserDTO { User = user, Token = tokenText};
        }
    }
}