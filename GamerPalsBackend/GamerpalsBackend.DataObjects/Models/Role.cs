using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GamerPalsBackend.DataObjects.Models
{
    public class Role : IModelBase
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string RoleName { get; set; }
        public int RoleCode { get; set; }

        public const string Verified = "Verified";
        public const string VIP = "VIP" + ", " + Verified;
        public const string Mod = "Mod, " + VIP;
        public const string Admin = "Admin, " + Mod;
    }
}
