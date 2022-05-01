using GoodLawSoftware.Application.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace GoodLawSoftware.Application.Service.UserService.Dto
{
	public class UserDto:UserCreateDto
	{
		public Guid Id { get; set; }

		[Required, MaxLength(32)]
		public string UserName { get; set; }
		[Required, MaxLength(32)]
		public string FirstName { get; set; }
		[Required, MaxLength(32)]
		public string LastName { get; set; }
		[Required, MaxLength(128),ValidateEmail]
		public string Email { get; set; }
		[Required, MaxLength(32)]
		public string Password { get; set; }
		





	}
}
