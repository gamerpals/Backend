namespace GamerPalsBackend.DataObjects.Models
{
    public class UserGame
    {
        public int UserID { get; set; }
        public int GameID { get; set; }
        public Game Game { get; set; }
        public User User { get; set; }
    }
}
