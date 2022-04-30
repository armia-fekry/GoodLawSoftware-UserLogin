namespace GoodLawSoftware.LoginApi.Helper
{
	public class JWT
	{
		public const string Jwt = "JWT";
		public string Key { get; set; } = string.Empty;
		public string Issuer { get; set; } = string.Empty;
		public string Audiance { get; set; } = string.Empty;
		public double TimeOutInHours { get; set; } = 0;
	}
}
