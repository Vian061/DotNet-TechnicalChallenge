using AutoMapper;
using BuildingBlocks.Common.Exceptions;
using BuildingBlocks.Common.Extentions;
using HealthCare.Application.DTOs;
using HealthCare.Application.Extentions;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Application.CQRS.Appointments
{
    public sealed record CreateAppointmentCommand(CreateAppointmentDTO Appointment) : IRequest<AppointmentDTO>;

    public sealed class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, AppointmentDTO>
    {
        private readonly IAppointmentRepository _appointmentRepo;
        private readonly IDoctorRepository _doctorRepo;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorScheduleRepository _doctorScheduleRepo;
        private readonly IMapper _mapper;
        public CreateAppointmentCommandHandler(
            IAppointmentRepository appointmentRepo,
            IDoctorRepository doctorRepo,
            IPatientRepository patientRepository,
            IDoctorScheduleRepository doctorScheduleRepo,
            IMapper mapper)
        {
            _appointmentRepo = appointmentRepo;
            _doctorRepo = doctorRepo;
            _patientRepository = patientRepository;
            _doctorScheduleRepo = doctorScheduleRepo;
            _mapper = mapper;
        }

        public async Task<AppointmentDTO> Handle(
            CreateAppointmentCommand request,
            CancellationToken ct)
        {
            // 1️ Load doctor
            var doctor = await _doctorRepo.GetByIdAsync(request.Appointment.DoctorId)
                ?? throw new NotFoundException("Doctor not found");

            // 12 Load patient
            var patient = await _patientRepository.GetByIdAsync(request.Appointment.PatientId)
                ?? throw new NotFoundException("Patient not found");

            var tz = TimeZoneInfo.FindSystemTimeZoneById(doctor.TimeZone);

            // 3️ Timezone & rounding
            var localStart = request.Appointment.StartUtc.RoundToFiveMinutes();
            var startUtc = TimeZoneInfo.ConvertTimeToUtc(localStart, tz);
            var endUtc = startUtc.AddMinutes(request.Appointment.Duration);
            var dayOfWeek = localStart.DayOfWeek.ToFlag();

            // 4️ Validate doctor working schedule
            var isWithinDoctorSchedule = await _doctorScheduleRepo.IsWithinDoctorSchedule(request.Appointment.DoctorId, dayOfWeek, localStart, TimeZoneInfo.ConvertTimeFromUtc(endUtc, tz));
            if (!isWithinDoctorSchedule)
            {
                throw new BadRequestException("Appointment is outside doctor working hours");
            }

            // 5 Overlap check
            var overlap = await _appointmentRepo.ExistsOverlapAsync(
                request.Appointment.DoctorId,
                startUtc,
                endUtc);

            if (overlap)
                throw new ConflictException("Overlapping appointment");

            // 6 Create entity
            var appointment = _mapper.Map<Appointment>(request.Appointment);
            appointment.StartUtc = startUtc;
            appointment.EndUtc = endUtc;

			// 5️ Persist
			var created = await _appointmentRepo.CreateAsync(appointment);

            return _mapper.Map<AppointmentDTO>(created);
        }
    }
}
