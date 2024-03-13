using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entities.identity;
using Talabat.Core.Services;
using Talabat.Reopsitory.Identity;
using Talabat.Service;

namespace Talabat.Extentions
{
    public static class IdentityServerExtentions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddScoped<ITokenService, TokenServices>();
            Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit=true;
                options.Password.RequireLowercase=true;
                options.Password.RequireUppercase=true;
                options.Password.RequireNonAlphanumeric=true;


            }).AddEntityFrameworkStores<AppIdentityDbContext>();


            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidateIssuer = true,
                       ValidIssuer = configuration["Jwt:ValidIssure"],
                       ValidateAudience = true,
                       ValidAudience = configuration["Jwt:ValidAudience"],
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]))
                   };
               });

            return Services;
        }
    }
}
