using System.Runtime.CompilerServices;

namespace Talabat.Extentions
{
    public static class SwggerServicesExtentions
    {
        public static IServiceCollection AddSwggerServecis(this IServiceCollection Services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();

            return Services;
        }
        public static void UseSwggerMiddleWares(this WebApplication app)
        {

            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
