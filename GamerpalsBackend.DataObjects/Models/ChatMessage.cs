using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace GamerPalsBackend.DataObjects.Models
{
    public class ChatMessage
    {
        public ObjectId Sender { get; set; } //ref User
        public string MessageType { get; set; }
        public string Message { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
