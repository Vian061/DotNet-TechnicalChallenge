using BuildingBlocks.Persistence.Repositories;
using BuildingBlocks.Shared.Entities;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Enums;
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

        public override async Task<Appointment?> GetByIdAsync(int id)
        {
            return await Context.Appointments
                .Include(_ => _.Doctor)
                .Include(_ => _.Patient)
                .FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<PagedResult<Appointment>> GetByPatientIdAsync(int patientId, int pageNumber = 1, int pageSize = 20)
        {
            IQueryable<Appointment> query = Context.Appointments
                .Where(_ => _.PatientId == patientId)
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

        public async Task<List<Appointment>> GetActiveAppointmentsInRangeAsync(int doctorId, DateTime start, DateTime end)
        {
            return await Context.Appointments
                .Where(_ => _.DoctorId == doctorId
                            && _.Status == AppointmentStatus.Active
                            && ((_.StartUtc >= start && _.StartUtc < end)
                                || (_.EndUtc > start && _.EndUtc <= end)
                                || (_.StartUtc <= start && _.EndUtc >= end)))
                .ToListAsync();
        }

        public async Task<bool> ExistsOverlapAsync(
        int doctorId,
        DateTime startUtc,
        DateTime endUtc)
        {
            return await Context.Appointments.AnyAsync(a =>
                a.DoctorId == doctorId &&
                a.Status == AppointmentStatus.Active &&
                a.StartUtc < endUtc &&
                a.EndUtc > startUtc);
        }

        public async Task CancelAppointment(Appointment appointment)
        {
            appointment.Status = AppointmentStatus.Cancelled;
            appointment.CancelledAtUtc = DateTime.UtcNow;
            Context.Appointments.Update(appointment);
            await Context.SaveChangesAsync();

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
