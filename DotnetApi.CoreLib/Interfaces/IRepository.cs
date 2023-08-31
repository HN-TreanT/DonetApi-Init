using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DotnetApi.CoreLib.Interfaces
{
    public interface IRepository
    {
        IQueryable<T> GetListAsync<T>(Expression<Func<T, bool>> filter) where T : class;

        IQueryable<T> GetList<T>(Expression<Func<T, bool>> filter) where T : class;
        Task<int> CountAsync<T>(Expression<Func<T, bool>> filter) where T : class;
        Task<int> CountAsync<T>() where T : class;

        Task<T> FindAsync<T>(object Key) where T : class;
        T Find<T>(object Key) where T : class;

        Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class;
        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class;
        Task<T> Last<T>() where T : class;
        Task AddAsync<T>(T entity) where T : class;
        Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class;
        Task UpdateAsync<T>(T entity) where T : class;
        Task UpdateRangeAsync<T>(IEnumerable<T> entities) where T : class;
        Task DeleteAsync<T>(object key) where T : class;
        Task SaveChangeAsync();

    }
}