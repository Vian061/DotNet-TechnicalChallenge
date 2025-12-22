using BuildingBlocks.Persistence.Repositories;
using BuildingBlocks.Shared.Entities;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Interfaces.Repositories;
using HealthCare.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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

        public override async Task<DoctorSchedule?> GetByAliasAsync(int id)
        {
            return await Context.DoctorSchedules
                .FirstOrDefaultAsync(_ => _.Id == id);
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
