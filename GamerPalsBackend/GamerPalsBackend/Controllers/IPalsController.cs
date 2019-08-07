using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GamerPalsBackend.Controllers
{
    public interface IPalsController<T>
    {
        Task<List<T>> GetAll();
        Task<IActionResult> GetSingle(ObjectId id);
        Task<IActionResult> PostBase(T document);
        Task<IActionResult> PutBase(ObjectId id, string document);
        Task<IActionResult> DeleteBase(ObjectId id);
    }
}
