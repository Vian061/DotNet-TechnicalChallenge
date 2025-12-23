using BuildingBlocks.Persistence.Interfaces;
using BuildingBlocks.Shared.Entities;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Domain.Interfaces.Repositories
{
    public interface IDoctorScheduleRepository : IBaseSQLServerRepository<DoctorSchedule>, ITransactionRepository<DoctorSchedule>
	{
		Task<PagedResult<DoctorSchedule>> GetAllByDoctorIdAsync(int doctorId, int pageNumber = 1, int pageSize = 20);
		Task<List<DoctorSchedule>> GetActiveSchedulesAsync(int doctorId);
		Task<bool> ExistsOverlapAsync(int doctorId, DayOfWeekFlags days, TimeSpan start, TimeSpan end);
	}
}
