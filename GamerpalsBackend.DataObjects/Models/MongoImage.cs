using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace GamerPalsBackend.DataObjects.Models
{
    public class MongoImage :IModelBase
    {
        public ObjectId _id { get; set; }
        public byte[] Data { get; set; }
        public string FileType { get; set; }
    }
}
