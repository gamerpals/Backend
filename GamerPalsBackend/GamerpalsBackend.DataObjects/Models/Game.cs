using System.Collections.Generic;

namespace GamerPalsBackend.DataObjects.Models
{
    public class Game
    {
        public int GameID { get; set; }
        public string GameName { get; set; }
        public int CurrentSearch { get; set; }
        public int PlayersOnline { get; set; }
        public List<GameServer> Servers { get; set; }
        public List<Parameter> Parameters { get; set; }
        public List<UserGame> GameUsers { get; set; }
    }
}
