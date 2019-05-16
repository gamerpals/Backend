using System.Collections.Generic;

namespace GamerPalsBackend.DataObjects.Models
{
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public List<UserOptionRoles> UserOptionRoles { get; set; }
    }
}
