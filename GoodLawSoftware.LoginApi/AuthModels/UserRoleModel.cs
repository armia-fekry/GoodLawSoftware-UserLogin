using System.ComponentModel.DataAnnotations;

namespace GoodLawSoftware.LoginApi.AuthModels
{
	public class UserRoleModel
	{
		[Required]
		public string UserId { get; set; }
		[Required]
		public string Role { get; set; }
	}
}
