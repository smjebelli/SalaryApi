using Microsoft.AspNetCore.Diagnostics;
using SalaryApi.Services;
using System.Net;

namespace SalaryApi.StartupExtensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        Serilog.Log.Error($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new 
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal server error",
                            Error= contextFeature.Error.Message
                        }.ToString());
                    }
                });
            });
        }
        public static void ConfigureMainServices(this IServiceCollection services)
        {
            services.AddScoped<ISalaryService, SalaryService>();           

        }


    }
}
