using System.Collections.Generic;

namespace GamerPalsBackend.DataObjects.Models
{
    public class UserOptions
    {
        public int UserOptionsID { get; set; }
        public int UserId { get; set; }
        public List<UserOptionRoles> UserOptionRoles { get; set; }
    }
}
