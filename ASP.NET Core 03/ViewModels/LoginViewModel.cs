namespace ASP.NET_Core_03.ViewModels
{
	public class LoginViewModel
	{
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public bool RememberMe { get; set; }

	}
}
