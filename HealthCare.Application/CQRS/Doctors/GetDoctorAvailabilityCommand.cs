using BuildingBlocks.Common.Exceptions;
using BuildingBlocks.Common.Extentions;
using HealthCare.Application.Extentions;
using HealthCare.Application.ValueObjects;
using HealthCare.Domain.Enums;
using HealthCare.Domain.Interfaces.Repositories;
using MediatR;

namespace HealthCare.Application.CQRS.Doctors
{
    public sealed record GetDoctorAvailabilityCommand(
        int DoctorId,
        DateTime From,
        DateTime To,
        int SlotMinutes
    ) : IRequest<IReadOnlyList<AvailabilitySlot>>;

    public sealed class GetDoctorAvailabilityCommandHandler : IRequestHandler<GetDoctorAvailabilityCommand, IReadOnlyList<AvailabilitySlot>>
    {
        private readonly IDoctorRepository _doctorRepo;
        private readonly IDoctorScheduleRepository _scheduleRepo;
        private readonly IAppointmentRepository _appointmentRepo;
        public GetDoctorAvailabilityCommandHandler(IDoctorRepository doctorRepo, IDoctorScheduleRepository scheduleRepo, IAppointmentRepository appointmentRepo)
        {
            _doctorRepo = doctorRepo;
            _scheduleRepo = scheduleRepo;
            _appointmentRepo = appointmentRepo;
        }
        public async Task<IReadOnlyList<AvailabilitySlot>> Handle(GetDoctorAvailabilityCommand request, CancellationToken cancellationToken)
        {
            // 1️ Doctor & timezone
            var doctor = await _doctorRepo.GetByIdAsync(request.DoctorId)
            ?? throw new NotFoundException("Doctor not found");

            var tz = TimeZoneInfo.FindSystemTimeZoneById(doctor.TimeZone);

            // 2️ Fetch schedules
            var schedules = await _scheduleRepo.GetActiveSchedulesAsync(request.DoctorId);

            // 3️ Convert range to UTC for DB query
            var fromUtc = TimeZoneInfo.ConvertTimeToUtc(request.From, tz);
            var toUtc = TimeZoneInfo.ConvertTimeToUtc(request.To, tz);

            // 4️ Fetch existing appointments
            var appointments = await _appointmentRepo
                .GetActiveAppointmentsInRangeAsync(
                    request.DoctorId,
                    fromUtc,
                    toUtc);

            var slots = new List<AvailabilitySlot>();

            // 5️ Slot generation (same logic as before)
            for (var date = request.From.Date; date <= request.To.Date; date = date.AddDays(1))
            {
                var dayFlag = date.DayOfWeek.ToFlag();

                foreach (var schedule in schedules.Where(s => s.DaysOfWeek.HasFlag(dayFlag)))
                {
                    var localStart = date.Add(schedule.StartTime);
                    var localEnd = date.Add(schedule.EndTime);

                    for (var t = localStart;
                         t.AddMinutes(request.SlotMinutes) <= localEnd;
                         t = t.AddMinutes(request.SlotMinutes))
                    {
                        var rounded = t.RoundToFiveMinutes();

                        var startUtc = TimeZoneInfo.ConvertTimeToUtc(rounded, tz);
                        var endUtc = startUtc.AddMinutes(request.SlotMinutes);

                        var overlap = appointments.Any(a =>
                            a.StartUtc < endUtc &&
                            a.EndUtc > startUtc);

                        if (!overlap)
                            slots.Add(new AvailabilitySlot(startUtc, endUtc));
                    }
                }
            }

            return slots;
        }

    }
}
