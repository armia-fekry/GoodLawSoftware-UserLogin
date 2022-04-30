using GoodLawSoftware.Application.Shared;
using System.ComponentModel.DataAnnotations;

namespace GoodLawSoftware.LoginApi.Data.AuthModels
{
	public class UserRegisterModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		[Required, MaxLength(50)]
		public string UserName { get; set; }
		public string Password { get; set; }
		[ValidateEmail]
		public string Email { get; set; }
	}
}
