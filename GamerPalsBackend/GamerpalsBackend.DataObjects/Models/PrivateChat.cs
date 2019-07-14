using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamerPalsBackend.DataObjects.Models
{
    public class Participant
    {
        public int userId { get; set; }
        public string joinDate { get; set; }
    }

    public class PrivateChat
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public bool isGroupChat { get; set; }
        public List<Participant> participants { get; set; }
        public List<ChatMessage> chatMessages { get; set; }
    }
}
