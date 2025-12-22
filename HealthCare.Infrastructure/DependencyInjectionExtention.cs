using BuildingBlocks.Persistence;
using BuildingBlocks.Shared.Configs;
using HealthCare.Domain.Interfaces.Repositories;
using HealthCare.Infrastructure.Context;
using HealthCare.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HealthCare.Infrastructure
{
    public static class DependencyInjectionExtention
    {
        public static void RegisterInfrastructure(this IServiceCollection services, DatabaseConfig dbConfig)
        {
            services.RegisterDatabase(dbConfig);
            services.RegisterRepositories();
        }

        private static void RegisterDatabase(this IServiceCollection services, DatabaseConfig dbConfig)
        {
            services.RegisterSQLServer<HealthCareDBContext>(dbConfig);
        }

        private static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IDoctorScheduleRepository, DoctorScheduleRepository>();
        }
    }
}
