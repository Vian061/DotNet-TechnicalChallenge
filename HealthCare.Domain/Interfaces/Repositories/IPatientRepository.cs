using BuildingBlocks.Persistence.Interfaces;
using HealthCare.Domain.Entities;

namespace HealthCare.Domain.Interfaces.Repositories
{
    public interface IPatientRepository : IBaseSQLServerRepository<Patient>, ITransactionRepository<Patient>
    {
    }
}
