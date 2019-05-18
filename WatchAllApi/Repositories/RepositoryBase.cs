using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using WatchAllApi.Interfaces.Repositories;
using WatchAllApi.Models;

namespace WatchAllApi.Repositories
{
    /// <summary>
    /// Base implementation of  BaseRepository
    /// </summary>
    /// <typeparam name="T">Represents type that will be store in DB</typeparam>
    public abstract class MongoRepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// Collection name where will be stored entities
        /// </summary>
        public abstract string CollectionName { get; }

        /// <summary>
        /// Mongo configuration
        /// </summary>
        protected IMongoDatabase MongoDatabase { get; }

        /// <summary>
        /// Ctor for repository
        /// </summary>
        /// <param name="settings"></param>
        protected MongoRepositoryBase(IOptions<MongoDbConfiguration> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                MongoDatabase = client.GetDatabase(settings.Value.Database);
        }

        /// <summary>
        /// Collection in DB
        /// </summary>
        protected IMongoCollection<T> Collection => MongoDatabase.GetCollection<T>(CollectionName);

        /// <summary>
        /// Insert new entity of T in DB
        /// </summary>
        /// <param name="entity">The entity of T that will be inserted in DB</param>
        /// <returns></returns>
        public Task InsertAsync(T entity)
        {
            return MongoDatabase.GetCollection<T>(CollectionName)
                                .InsertOneAsync(entity);
        }

        /// <summary>
        /// Insert range of new entities of T in DB
        /// </summary>
        /// <param name="entities">The range of entities of T that will be inserted in DB</param>
        /// <returns></returns>
        public Task InsertRangeAsync(IEnumerable<T> entities)
        {
            return MongoDatabase.GetCollection<T>(CollectionName)
                                .InsertManyAsync(entities);
        }

        /// <summary>
        /// Replace existing entity of T in DB by expression
        /// </summary>
        /// <param name="entity">The entity of T that will be replace existing record in DB</param>
        /// <param name="expression">Predicate for find entity in DB</param>
        /// <returns></returns>
        public Task ReplaceAsync(T entity, Expression<Func<T, bool>> expression)
        {
            return MongoDatabase.GetCollection<T>(CollectionName)
                                .ReplaceOneAsync(expression, entity);
        }

        /// <summary>
        /// Replace existing entity of T in DB by expression
        /// </summary>
        /// <param name="id">Id of entity that will be replaced</param>
        /// <param name="entity">The entity of T that will be inserted in DB</param>
        /// <returns></returns>
        public Task ReplaceByIdAsync(string id, T entity)
        {
            var filter = new BsonDocument("_id", id);
            return MongoDatabase.GetCollection<T>(CollectionName)
                .ReplaceOneAsync(filter, entity);
        }

        /// <summary>
        /// Delete existing entity of T in DB by expression
        /// </summary>
        /// <param name="expression">Predicate for find entity in DB</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            var deleteResult = await MongoDatabase.GetCollection<T>(CollectionName)
                                                  .DeleteOneAsync(expression);

            return deleteResult.DeletedCount > 0;
        }

        /// <summary>
        /// Delete existing entity of T in DB by expression
        /// </summary>
        /// <param name="id">Id of existing entity</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdAsync(string id)
        {
            var filter = new BsonDocument("_id", id);
            var deleteResult = await MongoDatabase.GetCollection<T>(CollectionName)
                .DeleteOneAsync(filter);

            return deleteResult.DeletedCount > 0;
        }

        /// <summary>
        /// Find existing entity of T in DB by id
        /// </summary>
        /// <param name="key">Id of existing entity</param>
        /// <returns></returns>
        public async Task<T> FindAsync(string key)
        {
            var filter = new BsonDocument("_id", key);
            var cursor = await MongoDatabase.GetCollection<T>(CollectionName)
                                            .FindAsync(filter);

            return await cursor.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Find existing entity of T in DB by expression
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> SelectAllAsync()
        {
            var cursor = await MongoDatabase.GetCollection<T>(CollectionName)
                                            .FindAsync(new BsonDocument());

            return await cursor.ToListAsync();
        }

        /// <summary>
        /// Get all existing entities of T in DB
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Collection as queryable
        /// </summary>
        /// <returns></returns>
        protected IQueryable<T> Query()
        {
            return MongoDatabase.GetCollection<T>(CollectionName)
                                .AsQueryable();
        }
    }
}
