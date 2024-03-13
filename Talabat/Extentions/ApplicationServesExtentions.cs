using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Talabat.Core;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Error;
using Talabat.Helper;
using Talabat.Reopsitory;
using Talabat.Service;

namespace Talabat.Extentions
{
    public static class ApplicationServesExtentions
    {
        public static IServiceCollection AddApplicationServes(this IServiceCollection Services)
        {
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<IPaymentServices, PaymentServices>();
            // Services.AddScoped(typeof(IGenaricRepositort<>), typeof(GenaricRepositort<>));

            Services.AddAutoMapper(typeof(MappingProfile));

           Services.Configure<ApiBehaviorOptions>(option =>
            {
                option.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var erorr = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                            .SelectMany(P => P.Value.Errors)
                                                            .Select(E => E.ErrorMessage).ToArray();

                    var ValedationErorrRepo = new ApiValidtionErorrResponse()
                    {
                        Erorrs = erorr,
                    };
                    return new BadRequestObjectResult(ValedationErorrRepo);
                };

            });

            Services.AddScoped(typeof(IBasketReopsitort), typeof(BasketRepositortL));

            return Services;
        }
    }
}
