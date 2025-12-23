using BuildingBlocks.Persistence.Repositories;
using BuildingBlocks.Shared.Entities;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Enums;
using HealthCare.Domain.Interfaces.Repositories;
using HealthCare.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Infrastructure.Repositories
{
    public class DoctorScheduleRepository : BaseSQLServerRepository<DoctorSchedule, HealthCareDBContext>, IDoctorScheduleRepository
    {
        public DoctorScheduleRepository(HealthCareDBContext context) : base(context)
        {
        }

        public override async Task<PagedResult<DoctorSchedule>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            IQueryable<DoctorSchedule> query = Context.DoctorSchedules;

            int totalItems = await query.CountAsync();

            List<DoctorSchedule> data = await query
                .OrderByDescending(o => o.DateCreated)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<DoctorSchedule>(
                data,
                pageNumber,
                pageSize,
                totalItems
            );
        }

        public override async Task<DoctorSchedule?> GetByIdAsync(int id)
        {
            return await Context.DoctorSchedules
                .FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<PagedResult<DoctorSchedule>> GetAllByDoctorIdAsync(int doctorId, int pageNumber = 1, int pageSize = 20)
        {
            IQueryable<DoctorSchedule> query = Context.DoctorSchedules;

            int totalItems = await query.CountAsync();

            List<DoctorSchedule> data = await query
                .Where(ds => ds.DoctorId == doctorId)
                .OrderByDescending(o => o.DateCreated)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<DoctorSchedule>(
                data,
                pageNumber,
                pageSize,
                totalItems
            );
        }

        public async Task<bool> IsWithinDoctorSchedule(
            int doctorId,
            DayOfWeekFlags day,
            DateTime start,
            DateTime end)
        {
            return await Context.DoctorSchedules
                .AnyAsync(s =>
                    s.DoctorId == doctorId &&
                    s.DaysOfWeek.HasFlag(day) &&
                    start.TimeOfDay >= s.StartTime &&
                    end.TimeOfDay <= s.EndTime
                );
        }

        public async Task<List<DoctorSchedule>> GetActiveSchedulesAsync(int doctorId)
        {
            return await Context.DoctorSchedules
                .Where(ds => ds.DoctorId == doctorId && ds.IsActive)
                .ToListAsync();
        }

        public async Task<bool> ExistsOverlapAsync(
            int doctorId,
            DayOfWeekFlags days,
            TimeSpan start,
            TimeSpan end)
        {
            return await Context.DoctorSchedules
                .AnyAsync(ds =>
                    ds.DoctorId == doctorId &&
                    (ds.DaysOfWeek & days) != 0 &&
                    ds.IsActive &&
                    ((start < ds.EndTime) && (end > ds.StartTime))
                );
        }

        public async Task<DoctorSchedule> CreateAsync(DoctorSchedule datum)
        {
            var result = Context.DoctorSchedules.Add(datum);
            await Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<DoctorSchedule> UpdateAsync(DoctorSchedule datum)
        {
            var result = Context.DoctorSchedules.Update(datum);
            await Context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
