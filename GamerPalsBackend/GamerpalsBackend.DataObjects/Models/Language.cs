using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GamerPalsBackend.DataObjects.Models
{
    public class Language : IModelBase
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string nativeName { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
