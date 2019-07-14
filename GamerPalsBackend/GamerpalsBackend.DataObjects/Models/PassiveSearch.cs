using GamerPalsBackend.DataObjects.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamerpalsBackend.DataObjects.Models
{
    public class Answer
    {
        public object sender { get; set; }
        public string postText { get; set; }
        public string createTime { get; set; }
    }

    public class PassiveSearch
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string createTime { get; set; }
        public object searchingGame { get; set; }
        public object creator { get; set; }
        public string postTitle { get; set; }
        public string postText { get; set; }
        public List<Parameters> searchParameters { get; set; }
        public List<Answer> answers { get; set; }
        public bool active { get; set; }
    }
}
