using VRFEngine.Common.Service;
using Microsoft.Extensions.DependencyInjection;

namespace VRFEngine.Service.Helper
{
    public static class ServiceExtension
    {
        public static void AddVRFEngineServices(this IServiceCollection services, string connectionString)
        {
            // services.AddSingleton<IAutoMapperService, AutoMapperService>();
            services.AddTransient<ILoggerService, LoggerService>();

            // services.AddScoped<IIdentificationService, IdentificationService>();

            // services.AddVRFEngineRepositories(connectionString);
        }
    }
}
