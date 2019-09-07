using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GamerPalsBackend.DataObjects;
using GamerPalsBackend.DataObjects.Models;
using GamerPalsBackend.Mongo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault.Models;
using MongoDB.Bson;

namespace GamerPalsBackend.Controllers
{
    [Route("api/Image")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private ControllerHelper<MongoImage> cont;
        public ImagesController(MongoContext context)
        {
            cont = new ControllerHelper<MongoImage>(context);
        }
        // GET: api/Default
        [HttpGet]
        [Authorize(Roles = Role.AdminBlank)]
        public async Task<List<MongoImage>> Get()
        {
            return await cont.FetchAll();
        }

        // GET: api/Default/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var img = await cont.FetchSingle(id);
            return File(img.Data, img.FileType);
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];

                if (file.Length > 0 && file.ContentType.Contains("image"))
                {
                    using (var stream = new MemoryStream())
                    {
                        file.CopyTo(stream);
                        var bytes = stream.ToArray();
                        var image = new MongoImage
                        {
                            Data = bytes,
                            FileType = file.ContentType
                        };
                        var img = await cont.Create(image);
                        return Ok(img._id.ToString());
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Default/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]string id, [FromBody] string document)
        {
            var res = await cont.Edit(id, document);
            if (res.HasValue)
            {
                if (res.Value)
                {
                    return Ok();
                }
                else
                {
                    return Conflict();
                }
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var res = await cont.Remove(id);
            if (res.HasValue)
            {
                if (res.Value)
                {
                    return Ok();
                }
                else
                {
                    return Conflict();
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
