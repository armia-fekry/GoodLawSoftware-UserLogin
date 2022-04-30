using System.ComponentModel.DataAnnotations;

namespace GoodLawSoftware.LoginApi.AuthModels
{
	public class LoginModel
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
