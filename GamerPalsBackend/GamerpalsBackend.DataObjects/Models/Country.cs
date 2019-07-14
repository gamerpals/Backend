using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamerpalsBackend.DataObjects.Models
{
    public class Country
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string shortName { get; set; }
        public string isoA2 { get; set; }
        public string isoA3 { get; set; }
        public string fullName { get; set; }
        public string flag { get; set; }
    }
}
