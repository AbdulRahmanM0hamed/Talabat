using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities.identity;
using Talabat.Dtos;

namespace AdminPanalTalabatMVC.Controllers
{
	public class AdminController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public AdminController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginDto loginDto)
		{
			var user = await _userManager.FindByEmailAsync(loginDto.Email);
			if (user == null)
			{
				ModelState.AddModelError("Email", "Email is IN Valid");
				return RedirectToAction(nameof(Login));
			}
			var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password,false) ;
			if(!result.Succeeded || await _userManager.IsInRoleAsync(user, "Admin"))
			{
				ModelState.AddModelError(string.Empty, "You Are not Authorized");
				return RedirectToAction("index","Home");
			}
			else
			{
				return RedirectToAction(nameof(Login));
			}

		}


		public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
	}
}
