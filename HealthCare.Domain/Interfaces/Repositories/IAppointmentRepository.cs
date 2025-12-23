using BuildingBlocks.Persistence.Interfaces;
using HealthCare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Domain.Interfaces.Repositories
{
    public interface IAppointmentRepository : IBaseSQLServerRepository<Appointment>, ITransactionRepository<Appointment>
	{
		Task<List<Appointment>> GetActiveAppointmentsInRangeAsync(int doctorId, DateTime start, DateTime end);

	}
}
