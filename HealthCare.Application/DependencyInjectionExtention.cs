using Microsoft.Extensions.DependencyInjection;

namespace HealthCare.Application
{
    public static class DependencyInjectionExtention
    {
        public static void RegisterApplication(this IServiceCollection services)
        {
            services.RegisterMapperConfigs();
            services.RegisterMediatR();
        }

        private static void RegisterMapperConfigs(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(DependencyInjectionExtention).Assembly);
        }

        private static void RegisterMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjectionExtention).Assembly));
        }
    }
}
