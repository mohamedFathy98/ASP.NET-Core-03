namespace ASP.NET_Core_03.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "First Name Is Requird")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Last Name Is Requird")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "User Name Is Requird")]
		public string UserName { get; set; }
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Password & Confirm Doesn't Match")]
		public string ConFirmPassword { get; set; }
		public bool IsAgree { get; set; }
	}
}
