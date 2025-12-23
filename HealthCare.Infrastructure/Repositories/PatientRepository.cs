using BuildingBlocks.Persistence.Repositories;
using BuildingBlocks.Shared.Entities;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Interfaces.Repositories;
using HealthCare.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Infrastructure.Repositories
{
    public class PatientRepository : BaseSQLServerRepository<Patient, HealthCareDBContext>, IPatientRepository
    {
        public PatientRepository(HealthCareDBContext context) : base(context)
        {
        }

        public override async Task<PagedResult<Patient>> GetAllAsync(int pageNumber = 1, int pageSize = 20)
        {
            IQueryable<Patient> query = Context.Patients;

            int totalItems = await query.CountAsync();

            List<Patient> data = await query
                .Include(_ => _.Appointments)
                .OrderByDescending(_ => _.DateCreated)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Patient>(
                data,
                pageNumber,
                pageSize,
                totalItems
            );
        }

        public override async Task<Patient?> GetByIdAsync(int id)
        {
            return await Context.Patients
                .Include(_ => _.Appointments)
                .FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<Patient> CreateAsync(Patient datum)
        {
            var result = Context.Patients.Add(datum);
            await Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Patient> UpdateAsync(Patient datum)
        {
            var result = Context.Patients.Update(datum);
            await Context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
