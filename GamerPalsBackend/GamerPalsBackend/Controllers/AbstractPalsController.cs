using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamerPalsBackend.DataObjects;
using GamerPalsBackend.DataObjects.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GamerPalsBackend.Controllers
{
    public class AbstractPalsController<T> : ControllerBase, IPalsController<T> where T : IModelBase
    {
        protected MongoHelper<T> helper;

        public AbstractPalsController(MongoContext context)
        {
            helper = new MongoHelper<T>(context);
        }

        public async Task<List<T>> GetAll()
        {
            return await helper.GetAll();
        }

        public async Task<IActionResult> GetSingle(string id)
        {
            return await GetSingle(new ObjectId(id));
        }

        public async Task<IActionResult> GetSingle(ObjectId id)
        {
            if (!await helper.Exists(id))
            {
                return NotFound();
            }
            var doc = await helper.Get(id);

            return Ok(doc);
        }

        public async Task<IActionResult> PostBase(T doc)
        {
            var id = await helper.Create(doc);
            return await GetSingle(id._id);
        }

        public async Task<IActionResult> PutBase([FromRoute] string id, [FromBody] string document)
        {
            try
            {
                JObject.Parse(document);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return await PutBase(new ObjectId(id), document);
        }

        public async Task<IActionResult> PutBase([FromRoute] ObjectId id,[FromBody] string document)
        {
            if (!await helper.Exists(id))
            {
                return NotFound();
            }

            T original = helper.Get(id).Result;
            var overwrite = BsonDocument.Parse(document);
            var origin = original.ToBsonDocument();
            var newDoc = origin.Merge(overwrite, true);
            var update = BsonSerializer.Deserialize<T>(newDoc);
            var doc = await helper.Update(id, update);
            if (doc)
            {
                return Ok();
            }
            else
            {
                return NoContent();
            }
        }

        public async Task<IActionResult> DeleteBase(string id)
        {
            return await DeleteBase(new ObjectId(id));
        }

        public async Task<IActionResult> DeleteBase(ObjectId id)
        {
            if (!await helper.Exists(id))
            {
                return NotFound();
            }
            var result = await helper.Delete(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return NoContent();
            }
        }
    }
}
