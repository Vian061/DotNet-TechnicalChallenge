using AutoMapper;
using BuildingBlocks.Common.Exceptions;
using HealthCare.Application.DTOs;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Interfaces.Repositories;
using MediatR;

namespace HealthCare.Application.CQRS.DoctorSchedules
{
    public sealed record CreateDoctorScheduleCommand(CreateDoctorScheduleDTO DoctorSchedule) : IRequest<DoctorScheduleDTO>;

    public sealed class CreateDoctorScheduleCommandHandler
    : IRequestHandler<CreateDoctorScheduleCommand, DoctorScheduleDTO>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IDoctorScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;
        public CreateDoctorScheduleCommandHandler(
            IDoctorRepository doctorRepository,
            IDoctorScheduleRepository scheduleRepository,
            IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
        }

        public async Task<DoctorScheduleDTO> Handle(
            CreateDoctorScheduleCommand request,
            CancellationToken ct)
        {
            var doctor = await _doctorRepository.GetByIdAsync(request.DoctorSchedule.DoctorId)
                ?? throw new NotFoundException("Doctor not found");

            var schedule = _mapper.Map<DoctorSchedule>(request.DoctorSchedule);
            schedule.Doctor = doctor;

            var overlaps = await _scheduleRepository.ExistsOverlapAsync(
                schedule.DoctorId,
                schedule.DaysOfWeek,
                schedule.StartTime,
                schedule.EndTime);

            if (overlaps)
                throw new ConflictException("Schedule overlaps existing schedule");

            var created = await _scheduleRepository.CreateAsync(schedule);

            return _mapper.Map<DoctorScheduleDTO>(created);
        }
    }

}
