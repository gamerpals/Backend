﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamerPalsBackend.DataObjects;
using GamerPalsBackend.DataObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GamerPalsBackend.Controllers
{
    [Route("api/IModelBase")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private MongoContext _context;
        private MongoHelper<IModelBase> helper;
        public DefaultController(MongoContext context)
        {
            _context = context;
            helper = new MongoHelper<IModelBase>(context);
        }
        // GET: api/Default
        [HttpGet]
        public async Task<List<IModelBase>> Get()
        {
            return await helper.GetAll();
        }

        // GET: api/Default/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(ObjectId id)
        {
            if (! await helper.Exists(id))
            {
                return NotFound();
            }
            var doc = await helper.Get(id);

            return Ok(doc);
        }

        // POST: api/Default
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IModelBase value)
        {
            var id = await helper.Create(value);

            return new CreatedResult("api/Default/"+id._id, id);

        }

        // PUT: api/Default/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(ObjectId id, [FromBody] IModelBase document)
        {
            if (!await helper.Exists(id))
            {
                return NotFound();
            }
            var doc = await helper.Update(id, document);
            if (doc)
            {
                return Ok();
            }
            else
            {
                return NoContent();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(ObjectId id)
        {
            if (! await helper.Exists(id))
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