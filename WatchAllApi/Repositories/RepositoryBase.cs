using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using WatchAllApi.Interfaces;
using WatchAllApi.Interfaces.Repositories;
using WatchAllApi.Models;

namespace WatchAllApi.Repositories
{ 
    public abstract class MongoRepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public abstract string CollectionName { get; }

        protected IMongoDatabase MongoDatabase { get; }

        protected MongoRepositoryBase(IOptions<MongoDbConfiguration> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                MongoDatabase = client.GetDatabase(settings.Value.Database);
        }

        protected IMongoCollection<T> Collection => MongoDatabase.GetCollection<T>(CollectionName);

        public Task InsertAsync(T entity)
        {
            return MongoDatabase.GetCollection<T>(CollectionName)
                                .InsertOneAsync(entity);
        }

        public Task InsertRangeAsync(IEnumerable<T> entities)
        {
            return MongoDatabase.GetCollection<T>(CollectionName)
                                .InsertManyAsync(entities);
        }

        public Task ReplaceAsync(T entity, Expression<Func<T, bool>> expression)
        {
            return MongoDatabase.GetCollection<T>(CollectionName)
                                .ReplaceOneAsync(expression, entity);
        }

        public Task ReplaceByIdAsync(string id, T entity)
        {
            var filter = new BsonDocument("_id", id);
            return MongoDatabase.GetCollection<T>(CollectionName)
                .ReplaceOneAsync(filter, entity);
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            var deleteResult = await MongoDatabase.GetCollection<T>(CollectionName)
                                                  .DeleteOneAsync(expression);

            return deleteResult.DeletedCount > 0;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var filter = new BsonDocument("_id", id);
            var deleteResult = await MongoDatabase.GetCollection<T>(CollectionName)
                .DeleteOneAsync(filter);

            return deleteResult.DeletedCount > 0;
        }

        public async Task<T> FindAsync(string key)
        {
            var filter = new BsonDocument("_id", key);
            var cursor = await MongoDatabase.GetCollection<T>(CollectionName)
                                            .FindAsync(filter);

            return await cursor.FirstOrDefaultAsync();
        }


        public async Task<List<T>> SelectAllAsync()
        {
            var cursor = await MongoDatabase.GetCollection<T>(CollectionName)
                                            .FindAsync(new BsonDocument());

            return await cursor.ToListAsync();
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> filter)
        {
            var cursor = await MongoDatabase.GetCollection<T>(CollectionName)
                                            .FindAsync(filter);

            return await cursor.ToListAsync();
        }

        /// <summary>
        /// Select entities with filter.
        /// </summary>
        /// <param name="filter">Filter expression.</param>
        /// <returns>Collection of entities.</returns>
        protected IQueryable<T> QueryWithFilter(Expression<Func<T, bool>> filter)
        {
            return MongoDatabase.GetCollection<T>(CollectionName)
                                .AsQueryable()
                                .Where(filter);
        }

        protected IQueryable<T> Query()
        {
            return MongoDatabase.GetCollection<T>(CollectionName)
                                .AsQueryable();
        }
    }
}
