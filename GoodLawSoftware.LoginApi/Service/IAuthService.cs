using GoodLawSoftware.LoginApi.AuthModels;
using GoodLawSoftware.LoginApi.Data.AuthModels;

namespace GoodLawSoftware.Service
{
	public interface IAuthService
	{
		Task<AuthModel> RegisterAsync(UserRegisterModel registerModel);
		Task<AuthModel> LoginAsync(LoginModel loginModel);
		Task<string> AddRoleToUserAsync(UserRoleModel userRoleModel);
	}
}
