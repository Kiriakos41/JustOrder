using JustOrder.Intefaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JustOrder.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly SQLiteAsyncConnection _db;

        public Repository(SQLiteAsyncConnection db)
        {
            this._db = db;
        }

        public AsyncTableQuery<T> AsQueryable() =>
            _db.Table<T>();

        public async Task<List<T>> Get() =>
            await _db.Table<T>().ToListAsync();

        public async Task<List<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
        {
            var query = _db.Table<T>();

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = query.OrderBy<TValue>(orderBy);

            return await query.ToListAsync();
        }

        public async Task<T> Get(int id) =>
            await _db.FindAsync<T>(id);

        public async Task<T> Get(Expression<Func<T, bool>> predicate) =>
            await _db.FindAsync<T>(predicate);

        public async Task<int> Insert(T entity) =>
            await _db.InsertAsync(entity);

        public async Task<int> Update(T entity) =>
            await _db.UpdateAsync(entity);

        public async Task<int> Delete(T entity) =>
            await _db.DeleteAsync(entity);
    }
}
