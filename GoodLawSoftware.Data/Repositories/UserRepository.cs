using GoodLawSoftware.Application;
using GoodLawSoftware.Application.IRepositoies;
using GoodLawSoftware.Infrastructure;

namespace JWT_NET_5.Infrastructure.Repositories
{
	public class UserRepository:BaseRepository<User>,IUserRepository
    {
		public UserRepository(GoodLawContext context):base(context)
		{

		}
    }
}
