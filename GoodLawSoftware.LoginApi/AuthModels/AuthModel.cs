using System;
using System.Collections.Generic;

namespace GoodLawSoftware.LoginApi.Data.AuthModels
{
	public class AuthModel
	{
		public string Message { get; set; }
		public bool IsAuthenticated { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public List<string> Roles { get; set; }
		public string Token { get; set; }
		public int ExpireDuration { get; set; }
		public DateTime ExpireOn { get; set; }
		public Guid UserId { get;  set; }
	}
}
