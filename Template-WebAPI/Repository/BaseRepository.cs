﻿using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template_WebAPI.Interfaces;

namespace Template_WebAPI.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> 
        where TEntity : class
    {
        protected readonly IMongoContext _mongoContext;
        protected IMongoCollection<TEntity> _dbCollection;

        protected BaseRepository(IMongoContext context)
        {
            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var all = await _dbCollection.FindAsync(Builders<TEntity>.Filter.Empty);
            return await all.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            var objectId = new ObjectId(id);
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", objectId);

            _dbCollection = _mongoContext.GetCollection<TEntity>(typeof(TEntity).Name);
            return await _dbCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public async Task AddAsync(TEntity obj)
        {
            if (obj == null) 
            {
                throw new ArgumentNullException(typeof(TEntity).Name + " object is null");
            }
            _dbCollection = _mongoContext.GetCollection<TEntity>(typeof(TEntity).Name);
            await _dbCollection.InsertOneAsync(obj);
        }

        public async Task AddByIdAsync(TEntity obj, string templateId)
        {
            if (obj == null || templateId == null)
            {
                throw new ArgumentNullException(typeof(TEntity).Name + " object is null");
            }
            else if (templateId.Length != 24)
            {
                throw new ArgumentOutOfRangeException(typeof(TEntity).Name + " string has to be 24 characters.");
            }

            // Adds entry
            var objectId = new ObjectId(templateId);
            await _dbCollection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", objectId), obj);
        }

        public async Task UpdateAsync(TEntity obj, string id)
        {
            var objectId = new ObjectId(id);
            await _dbCollection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", objectId), obj);
        }
        public async Task RemoveAsync(string id)
        {
            var objectId = new ObjectId(id);
            await _dbCollection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", objectId));
        }
    }
}
