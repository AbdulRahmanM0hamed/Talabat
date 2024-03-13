using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Talabat.Error;

namespace Talabat.Middelwares
{
    public class ExMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExMiddleware> logger;
        private readonly IHostEnvironment evn;

        public ExMiddleware(RequestDelegate next, ILogger<ExMiddleware> logger, IHostEnvironment evn)
        {
            this.next = next;
            this.logger = logger;
            this.evn = evn;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var rep = evn.IsDevelopment() ?
                    new ApiExResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    :
                      new ApiExResponse((int)HttpStatusCode.InternalServerError, ex.Message);

                var option = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(rep, option);

                await context.Response.WriteAsync(json);





            }

        }








    }
}
