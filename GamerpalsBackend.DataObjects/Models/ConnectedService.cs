using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace GamerPalsBackend.DataObjects.Models
{
    public class ConnectedService : IModelBase
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
    }
}
