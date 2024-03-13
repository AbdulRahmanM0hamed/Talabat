using AdminPanalTalabatMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities.identity;
using Talabat.Reopsitory.Date;

namespace AdminPanalTalabatMVC.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
        private readonly StoreContext store;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, StoreContext store)
		{
			_userManager = userManager;
			_roleManager = roleManager;
            this.store = store;
        }
		public async Task<IActionResult> Index()
		{
			var User = await _userManager.Users.Select(u => new UserViewModel
			{
				Id = u.Id,
				UserName = u.UserName,
				Email = u.Email,
				DisplayName = u.DisplayName,
				Roles = _userManager.GetRolesAsync(u).Result
			}).ToListAsync();

			return View(User);
		}

		public async Task<IActionResult> Edit(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			var allRoles = await _roleManager.Roles.ToListAsync();

			var UserVM = new UserRoleViewModel
			{
				UserId = user.Id,
				UserName = user.UserName,
				Roles = allRoles.Select(r => new RoleViewModel
				{
					Id = r.Id,
					Name = r.Name,
					IsSelected = _userManager.IsInRoleAsync(user, r.Name).Result
				}).ToList(),


			};
			return View(UserVM);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(UserRoleViewModel model)
		{
			var user = await _userManager.FindByIdAsync(model.UserId);
			var userRoles = await _userManager.GetRolesAsync(user);

			foreach (var role in model.Roles)
			{
				if (userRoles.Any(r => r == role.Name) && !role.IsSelected)
				{
					await _userManager.RemoveFromRoleAsync(user, role.Name);
				}
				if (!userRoles.Any(r => r == role.Name) && role.IsSelected)
				{
					await _userManager.AddToRoleAsync(user, role.Name);
				}
			}
			return RedirectToAction(nameof(Index));


		}


	}
}
