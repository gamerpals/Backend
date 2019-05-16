using System.Collections.Generic;

namespace GamerPalsBackend.DataObjects.Models
{
    public class User
    {
        public int UserID { get; set; }
        public int FacebookID { get; set; }
        public string GoogleID { get; set; }
        public string Username { get; set; }
        public int Karma { get; set; }
        public List<UserGame> UserGames { get; set; }
        public List<ActiveSearchUser> ActiveSearches { get; set; }
        public List<UserLanguage> Languages { get; set; }
        public GlobalParameters GlobalParams { get; set; }
    }
}
