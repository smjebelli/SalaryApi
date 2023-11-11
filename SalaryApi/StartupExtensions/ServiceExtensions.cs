using Microsoft.AspNetCore.Diagnostics;
using SalaryApi.Services;
using System.Net;

namespace SalaryApi.StartupExtensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureMainServices(this IServiceCollection services)
        {
            services.AddScoped<ISalaryService, SalaryService>();           

        }


    }
}
