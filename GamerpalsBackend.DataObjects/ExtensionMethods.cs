using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using GamerPalsBackend.DataObjects.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GamerpalsBackend.DataObjects
{
    public static class ExtensionMethods
    {
        public static T As<T>(this ObjectId id)
        {
            var name = typeof(T).Name;
            var context = new MongoContext();
            var coll = (IMongoCollection<T>)context.GetType().GetProperty(name).GetValue(context);
            return coll.Find(Builders<T>.Filter.Eq("_id", id)).FirstOrDefaultAsync().Result;
        }

        public static T Single<T>(this IMongoCollection<T> coll, Expression<Func<T, bool>> filter)
        {
            return coll.FindAsync(filter).Result.Single();
        }
    }
}
