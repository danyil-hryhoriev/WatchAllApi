using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WatchAll.Api.Interfaces.Repositories
{

    /// <summary>
    /// Base repository for DB
    /// </summary>
    /// <typeparam name="T">Represents type that will be store in DB</typeparam>
    public interface IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// Insert new entity of T in DB
        /// </summary>
        /// <param name="entity">The entity of T that will be inserted in DB</param>
        /// <returns></returns>
        Task InsertAsync(T entity);

        /// <summary>
        /// Insert range of new entities of T in DB
        /// </summary>
        /// <param name="entities">The range of entities of T that will be inserted in DB</param>
        /// <returns></returns>
        Task InsertRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Replace existing entity of T in DB by expression
        /// </summary>
        /// <param name="entity">The entity of T that will be replace existing record in DB</param>
        /// <param name="expression">Predicate for find entity in DB</param>
        /// <returns></returns>
        Task ReplaceAsync(T entity, Expression<Func<T, bool>> expression);

        /// <summary>
        /// Replace existing entity of T in DB by expression
        /// </summary>
        /// <param name="id">Id of entity that will be replaced</param>
        /// <param name="entity">The entity of T that will be inserted in DB</param>
        /// <returns></returns>
        Task ReplaceByIdAsync(string id, T entity);

        /// <summary>
        /// Delete existing entity of T in DB by expression
        /// </summary>
        /// <param name="expression">Predicate for find entity in DB</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Delete existing entity of T in DB by expression
        /// </summary>
        /// <param name="id">Id of existing entity</param>
        /// <returns></returns>
        Task<bool> DeleteByIdAsync(string id);

        /// <summary>
        /// Find existing entity of T in DB by id
        /// </summary>
        /// <param name="key">Id of existing entity</param>
        /// <returns></returns>
        Task<T> FindAsync(string key);

        /// <summary>
        /// Find existing entity of T in DB by expression
        /// </summary>
        /// <param name="expression">Predicate for find entity in DB</param>
        /// <returns></returns>
        Task<List<T>> FindAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Get all existing entities of T in DB
        /// </summary>
        /// <returns></returns>
        Task<List<T>> SelectAllAsync();
    }
}
