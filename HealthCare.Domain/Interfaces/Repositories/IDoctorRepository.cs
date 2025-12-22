using BuildingBlocks.Persistence.Interfaces;
using HealthCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Domain.Interfaces.Repositories
{
    public interface IDoctorRepository : IBaseSQLServerRepository<Doctor>
    {
    }
}
