namespace ASP.NET_Core_03.ViewModels
{
	public class ResetPasswordViewModel
	{

		public string Password { get; set; }
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Password & Confirm Doesn't Match")]
		public string ConFirmPassword { get; set; }
		public string Email { get; set; }
		public string Token { get; set; }

	}
}
