using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamerPalsBackend.DataObjects;
using GamerPalsBackend.DataObjects.Models;
using GamerPalsBackend.Mongo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GamerPalsBackend.Controllers
{
    public class ControllerHelper<T> : IPalsController<T> where T : IModelBase
    {
        protected MongoHelper<T> helper;
        protected MongoContext _context;
        protected IAuthorizationService auth;

        public ControllerHelper(MongoContext context)
        {
            helper = new MongoHelper<T>(context);
            _context = context;
        }

        public ControllerHelper(MongoContext context, IAuthorizationService auth) : this(context)
        {
            this.auth = auth;
        }

        public async Task<List<T>> FetchAll()
        {
            return await helper.GetAll();
        }

        public async Task<T> FetchSingle(string id)
        {
            return await FetchSingle(new ObjectId(id));
        }

        public async Task<T> FetchSingle(ObjectId id)
        {
            if (!await helper.Exists(id))
            {
                return default(T);
            }
            var doc = await helper.Get(id);

            return doc;
        }

        public async Task<T> Create(T doc)
        {
            var id = await helper.Create(doc);
            return await FetchSingle(id._id);
        }

        public async Task<bool?> Edit([FromRoute] string id, [FromBody] string document)
        {
            try
            {
                JObject.Parse(document);
            }
            catch (Exception e)
            {
                return null;
            }

            return await Edit(new ObjectId(id), document);
        }

        public async Task<bool?> Edit([FromRoute] ObjectId id,[FromBody] string document)
        {
            if (!await helper.Exists(id))
            {
                return null;
            }

            T original = helper.Get(id).Result;
            var overwrite = BsonDocument.Parse(document);
            var origin = original.ToBsonDocument();
            var newDoc = origin.Merge(overwrite, true);
            var update = BsonSerializer.Deserialize<T>(newDoc);
            
            var doc = await helper.Update(id, update);
            if (doc)
            {
                var cache = update as User;
                if (cache != null)
                {
                    if (!cache.ProfileComplete ?? false)
                    {
                        if (IsProfileComplete(cache))
                        {
                            cache.ProfileComplete = true;
                            bool extra = await new MongoHelper<User>(_context).Update(id, cache);
                            if (extra)
                            {
                                return true;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool?> Remove(string id)
        {
            return await Remove(new ObjectId(id));
        }

        public async Task<bool?> Remove(ObjectId id)
        {
            if (!await helper.Exists(id))
            {
                return null;
            }
            var result = await helper.Delete(id);
            return result;
        }

        private bool IsProfileComplete(User u)
        {
            return u.ProfileName != default(string) && u.Birthday != default(DateTime) && u.Country != default(ObjectId) &&
                   u.Gender != default(string) && u.Languages != default(List<ObjectId>) &&
                   u.ProfileDescription != default(string);
        }
    }
}
