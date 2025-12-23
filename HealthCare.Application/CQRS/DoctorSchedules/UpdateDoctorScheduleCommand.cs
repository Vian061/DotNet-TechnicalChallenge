using AutoMapper;
using BuildingBlocks.Common.Exceptions;
using HealthCare.Application.DTOs;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Interfaces.Repositories;
using MediatR;

namespace HealthCare.Application.CQRS.DoctorSchedules
{
    public sealed record UpdateDoctorScheduleCommand(UpdateDoctorScheduleDTO DoctorSchedule) : IRequest<DoctorScheduleDTO>;

    public sealed class UpdateDoctorScheduleCommandHandler : IRequestHandler<UpdateDoctorScheduleCommand, DoctorScheduleDTO>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IDoctorScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;
        public UpdateDoctorScheduleCommandHandler(
            IDoctorRepository doctorRepository,
            IDoctorScheduleRepository scheduleRepository,
            IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
        }

        public async Task<DoctorScheduleDTO> Handle(
            UpdateDoctorScheduleCommand request,
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

            var updated = await _scheduleRepository.UpdateAsync(schedule);

            return _mapper.Map<DoctorScheduleDTO>(updated);
        }
    }
}
