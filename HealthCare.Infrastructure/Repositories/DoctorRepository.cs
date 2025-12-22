using BuildingBlocks.Persistence.Repositories;
using BuildingBlocks.Shared.Entities;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Interfaces.Repositories;
using HealthCare.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Infrastructure.Repositories
{
    public class DoctorRepository : BaseSQLServerRepository<Doctor, HealthCareDBContext>, IDoctorRepository
    {
        public DoctorRepository(HealthCareDBContext context) : base(context)
        {
        }

        public override async Task<PagedResult<Doctor>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            IQueryable<Doctor> query = Context.Doctors;

            int totalItems = await query.CountAsync();

            List<Doctor> data = await query
                .Include(_ => _.Appointments)
                .Include(_ => _.Schedules)
                .OrderByDescending(_ => _.DateCreated)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Doctor>(
                data,
                pageNumber,
                pageSize,
                totalItems
            );
        }

        public override async Task<Doctor?> GetByAliasAsync(int id)
        {
            return await Context.Doctors
                .Include(_ => _.Appointments)
                .Include(_ => _.Schedules)
                .FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<Doctor> CreateAsync(Doctor datum)
        {
            var result = Context.Doctors.Add(datum);
            await Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Doctor> UpdateAsync(Doctor datum)
        {
            var result = Context.Doctors.Update(datum);
            await Context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
