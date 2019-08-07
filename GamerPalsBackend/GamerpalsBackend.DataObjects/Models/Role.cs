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

        public const string Verified = VerifiedBlank;
        public const string VIP = VIPBlank + ", " + Verified;
        public const string Mod = ModBlank + ", " + VIP;
        public const string Admin = AdminBlank + ", " + Mod;
        public const string AdminBlank = "Admin";
        public const string VerifiedBlank = "Verified";
        public const string VIPBlank = "VIP";
        public const string ModBlank = "Mod";
    }
}
