using JustOrder.Intefaces;
using JustOrder.Models;
using SQLite;
namespace JustOrder.Repositories
{
    public class BurgerRepository : Repository<Burger>, IBurger
    {
        private readonly SQLiteAsyncConnection _connection;
        public BurgerRepository(SQLiteAsyncConnection db) : base(db)
        {
            _connection = db;
        }
    }
}
