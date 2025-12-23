using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Infrastructure.Context
{
    public class HealthCareDBContextFactory : IDesignTimeDbContextFactory<HealthCareDBContext>
    {
        public HealthCareDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HealthCareDBContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=HealthCareDB;User Id=sa;Password=QwertY!23;TrustServerCertificate=True;");

            return new HealthCareDBContext(optionsBuilder.Options);
        }
    }
}
