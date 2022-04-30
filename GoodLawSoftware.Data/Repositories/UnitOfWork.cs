using GoodLawSoftware.Application.IRepositoies;
using GoodLawSoftware.Infrastructure;


namespace JWT_NET_5.Infrastructure.Repositories
{
	public class UnitOfWork : IUnitOfWork
    {
        private readonly GoodLawContext _context;



        public UnitOfWork(GoodLawContext context)
        {
            _context = context;

            UserRepository = new UserRepository(_context);
        }

        public IUserRepository UserRepository {get;private set;}

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
