using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DotnetApi.CoreLib;
using DotnetApi.CoreLib.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotnetApi.InfraLib.EntityFramework
{
    public class Repository : IRepository
    {
        private readonly WebContext _context;
        public Repository(WebContext context)
        {
            _context = context;
        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            _ = await _context.Set<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            await _context.Set<T>().AddRangeAsync(entities);

        }

        public async Task<int> CountAsync<T>(Expression<Func<T, bool>> filter) where T : class
        {
            return await _context.Set<T>().CountAsync(filter);
        }

        public async Task<int> CountAsync<T>() where T : class
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task DeleteAsync<T>(object key) where T : class
        {
            T obj = await FindAsync<T>(key);
            _context.Set<T>().Remove(obj);
        }

        public T Find<T>(object Key) where T : class
        {
            return _context.Set<T>().Find(Key);
        }

        public async Task<T> FindAsync<T>(object Key) where T : class
        {
            return await _context.Set<T>().FindAsync(Key);
        }

        public async Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public IQueryable<T> GetList<T>(Expression<Func<T, bool>> filter) where T : class
        {
            if (filter == null)
                return _context.Set<T>();
            return _context.Set<T>().Where(filter);
        }

        public IQueryable<T> GetListAsync<T>(Expression<Func<T, bool>> filter) where T : class
        {
            if (filter == null)
                return _context.Set<T>();
            return _context.Set<T>().Where(filter);
        }

        public async Task<T> Last<T>() where T : class
        {
            return (await _context.Set<T>().ToListAsync()).LastOrDefault();
        }

        public async Task SaveChangeAsync()
        {
            _ = await _context.SaveChangesAsync();
        }

        public async Task<T> SingleOrDefaultAsync<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return await _context.Set<T>().SingleOrDefaultAsync(expression);
        }

        public async Task UpdateAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
            await Task.Delay(1);
        }

        public async Task UpdateRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            _context.Set<T>().UpdateRange(entities);
            await Task.Delay(10);
        }
    }
}