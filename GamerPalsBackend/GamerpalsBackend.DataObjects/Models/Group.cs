namespace GamerPalsBackend.DataObjects.Models
{
    public class Group
    {
        public int GroupID { get; set; }
        public User Leader { get; set; }
        public ActiveSearch ActiveSearch { get; set; }
    }
}
