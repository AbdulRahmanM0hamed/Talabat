
using AdminPanalTalabatMVC.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;

using Talabat.Core;
using Talabat.Core.Entities.identity;

using Talabat.Reopsitory;
using Talabat.Reopsitory.Date;
using Talabat.Reopsitory.Identity;


namespace AdminPanalTalabatMVC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();


			//DateBase
			builder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			builder.Services.AddDbContext<AppIdentityDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));

			});

			builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
			{
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireNonAlphanumeric = true;
			}).AddEntityFrameworkStores<AppIdentityDbContext>();


			builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
			builder.Services.AddAutoMapper(typeof(MapProfile));


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
			   pattern: "{controller=Admin}/{action=Login}/{id?}");


			app.Run();
		}
	}
}