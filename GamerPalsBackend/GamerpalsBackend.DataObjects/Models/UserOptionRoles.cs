namespace GamerPalsBackend.DataObjects.Models
{
    public class UserOptionRoles
    {
        public int UserOptionId { get; set; }
        public UserOptions UserOptions { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
