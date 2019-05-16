namespace GamerPalsBackend.DataObjects.Models
{
    public class GameServer
    {
        public int GameServerID { get; set; }
        public Game ServerGame { get; set; }
        public string ServerName { get; set; }
    }
}
