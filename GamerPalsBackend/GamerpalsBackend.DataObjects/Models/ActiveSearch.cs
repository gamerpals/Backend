using System.Collections.Generic;

namespace GamerPalsBackend.DataObjects.Models
{
    public class ActiveSearch
    {
        public int ActiveSearchID { get; set; }
        public GameServer Server { get; set; }
        public SearchType SearchType { get; set; }
        public bool Active { get; set; }
        public int MaxPlayers { get; set; }
        public List<ActiveSearchUser> JoinedUsers { get; set; }
        public List<SearchParameter> Parameters { get; set; }
        public string Description { get; set; }
        public User Owner { get; set; }
    }
}
