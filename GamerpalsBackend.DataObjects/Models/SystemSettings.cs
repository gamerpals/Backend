using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GamerPalsBackend.DataObjects.Models
{
    //TODO in dev
    public class SystemSettings
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string SettingsName { get; set; }
        public string Setting { get; set; }
    }
}
