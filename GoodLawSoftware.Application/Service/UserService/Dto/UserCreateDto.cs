
using System.ComponentModel.DataAnnotations;

namespace GoodLawSoftware.Application.Service.UserService.Dto
{
	public class UserCreateDto
    {
		[Required,MaxLength(32)]
		public string UserName { get; set; }
		[Required,MaxLength(32)]
		public string FisrtName { get; set; }
		[Required, MaxLength(32)]
		public string LastName { get; set; }
		[Required, MaxLength(32),EmailAddress]
		public string Email { get; set; }
		[Required, MaxLength(32)]
		public string Password { get; set; }
		
		
		
	}
}
