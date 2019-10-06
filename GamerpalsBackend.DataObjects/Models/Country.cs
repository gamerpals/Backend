using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using GamerPalsBackend.DataObjects.Models;

namespace GamerpalsBackend.DataObjects.Models
{
    public class Country : IModelBase
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }  
    }
}
