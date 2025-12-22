using BuildingBlocks.Persistence.DBContex;
using BuildingBlocks.Shared.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Persistence
{
    public static class DependencyInjectionExtention
    {
        public static void RegisterSQLServer<TContext>(this IServiceCollection services, DatabaseConfig databaseConfig) where TContext : SQLServerBaseContext
        {
            services.AddDbContext<SQLServerBaseContext, TContext>(options =>
            {
                options.UseSqlServer(databaseConfig.ConnectionString);
            });
        }
    }
}
