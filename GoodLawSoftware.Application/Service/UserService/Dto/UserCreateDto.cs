
using System.ComponentModel.DataAnnotations;

namespace GoodLawSoftware.Application.Service.UserService.Dto
{
	public class UserCreateDto
    {
		[Required,MaxLength(32)]
		public string UserName { get; set; }
		[Required, MaxLength(32)]
		public string Password { get; set; }
		
		
		
	}
}
