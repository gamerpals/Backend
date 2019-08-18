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
        Task<T> GetSingle(ObjectId id);
        Task<T> PostBase(T document);
        Task<bool?> PutBase(ObjectId id, string document);
        Task<bool?> DeleteBase(ObjectId id);
    }
}
