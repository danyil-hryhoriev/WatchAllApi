using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WatchAllApi.Interfaces.Repositories
{

    public interface IRepositoryBase<T> where T : class
    {
        Task InsertAsync(T entity);
        Task InsertRangeAsync(IEnumerable<T> entities);
        Task ReplaceAsync(T entity, Expression<Func<T, bool>> expression);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> expression);
        Task<T> FindAsync(string key);
        Task<List<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> SelectAllAsync();
    }
}
