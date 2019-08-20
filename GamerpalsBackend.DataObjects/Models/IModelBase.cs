using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace GamerPalsBackend.DataObjects.Models
{
    public interface IModelBase
    {
        ObjectId _id { get; set; }
    }
}
