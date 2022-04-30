using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GoodLawSoftware.Application.Shared
{
	public class ValidateEmail: ValidationAttribute
    {
        private const string EmailRegex = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var email = value.ToString();
            if (string.IsNullOrEmpty(email))
                return new ValidationResult("not valid email");
            var domain = email.Contains('@')
                ? email.Split('@')[1].Trim()
                : email;
            var regex = new Regex(EmailRegex);
            if (!regex.IsMatch(email))
                return new ValidationResult("not valid email");
            return ValidationResult.Success;
        }
    }
}
