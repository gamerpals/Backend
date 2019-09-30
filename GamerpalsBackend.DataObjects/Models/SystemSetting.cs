using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GamerPalsBackend.DataObjects.Models
{
    //TODO in dev
    public class SystemSetting : IModelBase
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string SettingsName { get; set; }
        public string SettingsValue { get; set; }

        public string DataType { get; set; }
    }
}
