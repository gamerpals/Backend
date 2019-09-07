using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamerpalsBackend.DataObjects;
using GamerPalsBackend.DataObjects.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace GamerPalsBackend.Mongo
{
    public class MongoHelper<T> where T : IModelBase
    {
        private readonly MongoContext context;
        private string ObjectName = typeof(T).Name + "s";
        private IMongoCollection<T> coll => (IMongoCollection<T>)context.GetType().GetProperty(ObjectName).GetValue(context);

        public MongoHelper(MongoContext context)
        {
            this.context = context;
        }
        public async Task<T> Get(ObjectId id)
        {
            return (await coll.FindAsync(Builders<T>.Filter.Eq("_id", id))).FirstOrDefaultAsync().Result;
        }

        public async Task<List<T>> GetAll()
        {
            return await coll.FindAsync(_ => true).Result.ToListAsync();
        }

        public async Task<bool> Update(ObjectId id, T document)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            
            return (await coll.ReplaceOneAsync(filter, document)).IsAcknowledged;
        }

        public async Task<T> Create(T document)
        {
            coll.InsertOne(document);
            return document;
        }

        public async Task<bool> Delete(ObjectId id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return (await coll.DeleteOneAsync(filter)).IsAcknowledged;
        }

        public async Task<bool> Exists(ObjectId id)
        {
            return (await coll.FindAsync(f => f._id.Equals(id))).FirstOrDefaultAsync().Result != null;
        }
    }
}
