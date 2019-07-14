using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GamerPalsBackend.DataObjects.Models
{

    public class ActiveSearch : IModelBase
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public DateTime CreateTime { get; set; }
        public ObjectId SearchingGame { get; set; } //ref Game
        public ObjectId Administrator { get; set; } //ref user
        public List<ObjectId> JoinedUser { get; set; } //ref User
        public string Description { get; set; }
        public List<Parameters> Parameters { get; set; }
        public List<ChatMessage> ChatMessages { get; set; }
        public string DiscordJoinCode { get; set; }
        public bool Active { get; set; }
    }
    public class Parameters
    {
        public ObjectId Parameter { get; set; }
        public int ValueFrom { get; set; }
        public int ValueTo { get; set; }
    }
}
