using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace ASP.NET_Core_03.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		public IActionResult Register()
		{
			return View();
		}
		public IActionResult Register(RegisterViewModel model) 
		{
			if (!ModelState.IsValid) return View(model);
			// Create appliction User Object
			var user = new ApplicationUser
			{
				UserName = model.UserName,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,

			};
			var result = _userManager.CreateAsync(user, model.Password).Result;
			if (result.Succeeded) return RedirectToAction(nameof(Login));
			foreach (var error in result.Errors)
				ModelState.AddModelError(string.Empty, error.Description);
			return View();		
		}

		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Login(LoginViewModel model)
		{
			if (!ModelState.IsValid) return View(model);
			var user = _userManager.FindByEmailAsync(model.Email).Result;
			if (user is not null)
			{
				if (_userManager.CheckPasswordAsync(user, model.Password).Result)
				{
					var result = _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false).Result;
					if (result.Succeeded) return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", string.Empty));
				}

			}
			ModelState.AddModelError(string.Empty, "InCorrect Email Or Password");
			return View(model);
		}
	}
}
