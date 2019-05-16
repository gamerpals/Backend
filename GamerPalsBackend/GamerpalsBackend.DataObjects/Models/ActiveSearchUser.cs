namespace GamerPalsBackend.DataObjects.Models
{
    public class ActiveSearchUser
    {
        public int ActiveSearchID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public ActiveSearch ActiveSearch { get; set; }
    }
}
