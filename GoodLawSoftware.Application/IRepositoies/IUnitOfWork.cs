namespace GoodLawSoftware.Application.IRepositoies
{
	public interface IUnitOfWork:IDisposable
	{
		public IUserRepository UserRepository { get; }
		int Complete();
	}
}
