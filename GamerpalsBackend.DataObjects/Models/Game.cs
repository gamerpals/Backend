using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GamerPalsBackend.DataObjects.Models
{
    public class Game : IModelBase
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public List<ObjectId> AvailableParameters { get; set; }
    }
}
