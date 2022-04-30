
using System.ComponentModel.DataAnnotations;

namespace JWT_NET_5.Application.Service.UserService.Dto
{
	public class UserCreateDto
    {
		[Required,MaxLength(32)]
		public string UserName { get; set; }
		[Required, MaxLength(32)]
		public string Password { get; set; }
		
		
		
	}
}
