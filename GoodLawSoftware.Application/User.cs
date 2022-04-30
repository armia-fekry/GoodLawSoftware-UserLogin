using GoodLawSoftware.Application.Shared;
using Microsoft.AspNetCore.Identity;

namespace GoodLawSoftware.Application
{
	public class User : IdentityUser<Guid>
	{
		#region Properties
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Password { get; set; }
		#endregion

		public User()
		{

		}
		private User(Guid id,string userName,string password)
		{
			AssertionConcern.AssertionAgainstNotNull(id, "Invalid User Id");
			AssertionConcern.AssertionAgainstNotNullOrEmplty(userName, "Invalid User Name");
			AssertionConcern.AssertionAgainstNotNullOrEmplty(password, "Invalid User Password");

			Id = id;
			UserName = userName;
			Password = password;
		}
		public static User Create(Guid id, string userName, string password)
		{
			return new(id, userName, password);
		}
		

	}
}
