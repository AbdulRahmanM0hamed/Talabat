using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Talabat.Core.Entities.identity;
using Talabat.Core.Repositories;
using Talabat.Error;
using Talabat.Extentions;
using Talabat.Helper;
using Talabat.Middelwares;
using Talabat.Reopsitory;
using Talabat.Reopsitory.Date;
using Talabat.Reopsitory.Identity;

namespace Talabat
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //DateBase
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));

            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(option =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");

                return ConnectionMultiplexer.Connect(connection);
            });

            //exServis
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddApplicationServes();
            builder.Services.AddSwggerServecis();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("myPolicy", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration["frontUrl"]); 
                });

            });

            var app = builder.Build();

            #region Up-date Date Base insed-Main

            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var DbContext = services.GetRequiredService<StoreContext>();
                await DbContext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(DbContext);

                var IdentityDbContext = services.GetRequiredService<AppIdentityDbContext>();
                await IdentityDbContext.Database.MigrateAsync();
                
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsync(userManager);  
            }
            catch (Exception ex)
            {
                var ILogger = loggerFactory.CreateLogger<Program>();
                ILogger.LogError(ex, "an error occured during apply the mihration ");
            }
            #endregion


            #region Configure requst into Piplines
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwggerMiddleWares();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMiddleware<ExMiddleware>();
            app.UseStatusCodePagesWithRedirects("/erorrs/{0}");
            app.UseCors("myPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            #endregion

            app.Run();


        }
    }
}