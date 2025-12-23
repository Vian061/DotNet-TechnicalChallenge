using BuildingBlocks.Persistence.Interfaces;
using HealthCare.Domain.Entities;

namespace HealthCare.Domain.Interfaces.Repositories
{
    public interface IAppointmentRepository : IBaseSQLServerRepository<Appointment>, ITransactionRepository<Appointment>
    {
        Task CancelAppointment(Appointment appointment);
        Task<List<Appointment>> GetActiveAppointmentsInRangeAsync(int doctorId, DateTime start, DateTime end);
        Task<bool> ExistsOverlapAsync(int doctorId, DateTime startUtc, DateTime endUtc);
    }
}
