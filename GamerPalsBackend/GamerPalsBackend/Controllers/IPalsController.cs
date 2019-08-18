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
        Task<List<T>> FetchAll();
        Task<T> FetchSingle(ObjectId id);
        Task<T> Create(T document);
        Task<bool?> Edit(ObjectId id, string document);
        Task<bool?> Remove(ObjectId id);
    }
}
