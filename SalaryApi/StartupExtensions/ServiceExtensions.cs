using SalaryApi.Services;

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
