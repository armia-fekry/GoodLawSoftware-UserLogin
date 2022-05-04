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
		private User(Guid id,string userName,string password,
			string firstName, string lastName, string email)
		{
			AssertionConcern.AssertionAgainstNotNull(id, "Invalid User Id");
			AssertionConcern.AssertionAgainstNotNullOrEmplty(userName, "Invalid User Name");
			AssertionConcern.AssertionAgainstNotNullOrEmplty(password, "Invalid User Password");

			Id = id;
			UserName = userName;
			Password = password;
			FirstName = firstName;
			LastName = lastName;
			Email= email;
		}
		public static User Create(Guid id, string userName, string password,
			string firstName,string lastName,string Email)
		{
			return new(id, userName, password,firstName,lastName,Email);
		}
		

	}
}
