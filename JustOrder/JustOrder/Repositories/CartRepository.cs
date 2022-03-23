using JustOrder.Intefaces;
using JustOrder.Models;
using SQLite;

namespace JustOrder.Repositories
{
    public class CartRepository : Repository<Cart>, ICart
    {
        private readonly SQLiteAsyncConnection _connection;
        public CartRepository(SQLiteAsyncConnection db) : base(db)
        {
            _connection = db;
        }
    }
}
