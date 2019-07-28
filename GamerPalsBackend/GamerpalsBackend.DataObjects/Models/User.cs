using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GamerPalsBackend.DataObjects.Models
{
    public class CurrentSession
    {
        public ObjectId _id { get; set; }
        public DateTime ValidTo { get; set; }
        public string SessionToken { get; set; }
    }

    public class KarmaChange
    {
        public int Points { get; set; }
        public string Timestamp { get; set; }
        public string Reason { get; set; }
    }

    public class Karma
    {
        public int CurrentKarma { get; set; }
        public List<KarmaChange> ChangeHistory { get; set; }
    }

    public class RecievedFriendRequest
    {
        public object RequestFrom { get; set; }
        public bool Accepted { get; set; }
    }

    public class SentFriendRequest
    {
        public object RequestTo { get; set; }
        public bool Accepted { get; set; }
    }

    public class Notification
    {
        public string MessageType { get; set; }
        public string Message { get; set; }
        public string CreateTime { get; set; }
        public string LinkedUrl { get; set; }
        public bool Dismissed { get; set; }
    }

    public class User : IModelBase
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public DateTime CreateTime { get; set; }
        public string GoogleId { get; set; }
        public string ProfileName { get; set; }
        public string ProfileDescription { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime Birthday { get; set; }
        public string OnlineStatus { get; set; }
        public ObjectId Country { get; set; }
        public List<ObjectId> Languages { get; set; }
        public string Gender { get; set; }
        public CurrentSession CurrentSession { get; set; }
        public Karma Karma { get; set; }
        public List<ObjectId> GamesSelected { get; set; }
        public List<ObjectId> ActiveSearches { get; set; }
        public List<ObjectId> PassiveSearches { get; set; }
        public ObjectId Role { get; set; }
        public List<ObjectId> FriendsList { get; set; }
        public List<RecievedFriendRequest> RecievedFriendRequests { get; set; }
        public List<SentFriendRequest> SentFriendRequests { get; set; }
        public List<ObjectId> PrivateChats { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<ObjectId> ConnectedServices { get; set; }
        public bool ProfileComplete { get; set; }
    }
}
