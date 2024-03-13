using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.identity;

namespace Talabat.Extentions
{
    public static class UserManagerExtentions
    {
        public static async Task<AppUser> FindUserWithAddressByEmailAsync(this UserManager<AppUser> userManager,ClaimsPrincipal currrnteuser)
        {
            var email= currrnteuser.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(u=>u.Addess).FirstOrDefaultAsync(u=>u.Email==email);
            return user;
        }

    }
}
