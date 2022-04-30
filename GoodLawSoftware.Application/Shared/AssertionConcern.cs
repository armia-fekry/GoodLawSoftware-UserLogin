using System.Text.RegularExpressions;

namespace GoodLawSoftware.Application.Shared
{
	public class AssertionConcern
	{
        private const string EmailRegex = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public static void AssertionAgainstNotNull(object object1, string message)
        {
            if (object1 is null)
                throw new ArgumentNullException(nameof(object1));
        }
        public static void AssertionAgainstNullGuid(Guid object1, string message)
        {
            if (object1 == default(Guid))
                throw new ArgumentNullException($"{nameof(object1)} {message}");
        }
        public static void AssertionAgainstNotNullOrEmplty(string object1, string message)
        {
            if (string.IsNullOrEmpty(object1))
                throw new ArgumentNullException(nameof(object1));
        }
        public static void AssertionAgainstNotValidEmail(string email, string message)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));
            var regex = new Regex(EmailRegex);
            if (regex.IsMatch(email))
                throw new Exception(message);
        }
        
    }
}
