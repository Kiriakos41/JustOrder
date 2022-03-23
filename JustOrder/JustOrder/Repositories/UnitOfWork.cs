using JustOrder.Intefaces;
using SQLite;

namespace JustOrder.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(SQLiteAsyncConnection context)
        {
            BurgerTable = new BurgerRepository(context);
            CartTable = new CartRepository(context);
        }

        public IBurger BurgerTable { get; }
        public ICart CartTable { get; }

        public void Dispose()
        {
            return;
        }
    }
}
