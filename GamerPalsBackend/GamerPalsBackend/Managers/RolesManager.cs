using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using GamerPalsBackend.DataObjects.Models;

namespace GamerPalsBackend.Managers
{
    public static class RolesManager
    {
        
        static RolesManager()
        {

        }
        public static ClaimsIdentity GetClaimsForUser(this PalsContext context, int userID)
        {
            var options = context.UserOptions.FirstOrDefault(opt => opt.UserId == userID);
            var Claims = new List<Claim>();
            Claims.Add(new Claim(ClaimTypes.Role, "Verified"));
            if (options != null)
            {
                foreach (UserOptionRoles s in options.UserOptionRoles)
                {
                    Claims.Add(new Claim(ClaimTypes.Role, s.Role.RoleName));
                }
            }

            return new ClaimsIdentity(Claims.ToArray<Claim>());
        }
        public static void GrantRoleToUser(this PalsContext context, int userId, string rolename)
        {
            var options = context.UserOptions.Single(opt => opt.UserId == userId);
            var role = context.Roles.Single(r => r.RoleName == rolename);
            options.UserOptionRoles.Add(new UserOptionRoles {RoleId = role.RoleID, UserOptionId = options.UserOptionsID, Role = role, UserOptions = options });
            context.Update(options);
            context.SaveChanges();
        }
        public static void RemoveRoleFromUser(this PalsContext context, int userId, string rolename)
        {
            var options = context.UserOptions.Single(opt => opt.UserId == userId);
            var role = context.Roles.Single(r => r.RoleName == rolename);

            var UserOptionRoles = context.UserOptionRoles.Single(uor => (uor.RoleId == role.RoleID && uor.UserOptionId == options.UserOptionsID));
            context.UserOptionRoles.Remove(UserOptionRoles);
            context.SaveChanges();
        }
    }
}
