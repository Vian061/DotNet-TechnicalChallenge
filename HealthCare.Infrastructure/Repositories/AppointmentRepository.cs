using BuildingBlocks.Persistence.Repositories;
using BuildingBlocks.Shared.Entities;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Interfaces.Repositories;
using HealthCare.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Infrastructure.Repositories
{
    public class AppointmentRepository : BaseSQLServerRepository<Appointment, HealthCareDBContext>, IAppointmentRepository
    {
        public AppointmentRepository(HealthCareDBContext context) : base(context)
        {
        }

        public override async Task<PagedResult<Appointment>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            IQueryable<Appointment> query = Context.Appointments
                .Include(_ => _.Doctor)
                .Include(_ => _.Patient);

            int totalItems = await query.CountAsync();

            List<Appointment> data = await query
                .OrderByDescending(_ => _.DateCreated)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Appointment>(
                data,
                pageNumber,
                pageSize,
                totalItems
            );
        }

        public override async Task<Appointment?> GetByAliasAsync(int id)
        {
            return await Context.Appointments
                .Include(_ => _.Doctor)
                .Include(_ => _.Patient)
                .FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<Appointment> CreateAsync(Appointment appointment)
        {
            var result = Context.Appointments.Add(appointment);
            await Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Appointment> UpdateAsync(Appointment appointment)
        {
            var result = Context.Appointments.Update(appointment);
            await Context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
