using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Template_WebAPI.Interfaces
{
    interface IMongoContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
