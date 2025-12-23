using AutoMapper;
using BuildingBlocks.Shared.Entities;
using HealthCare.Application.DTOs;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Interfaces.Repositories;
using MediatR;

namespace HealthCare.Application.CQRS.DoctorSchedules
{
    public sealed record GetDoctorSchedulesCommand(int DoctorId, int PageNumber, int PageSize) : IRequest<PagedResult<DoctorScheduleDTO>>;

    public sealed class GetDoctorSchedulesCommandHandler : IRequestHandler<GetDoctorSchedulesCommand, PagedResult<DoctorScheduleDTO>>
    {
        private readonly IDoctorScheduleRepository _doctorScheduleRepository;
        private readonly IMapper _mapper;
        public GetDoctorSchedulesCommandHandler(IDoctorScheduleRepository doctorScheduleRepository, IMapper mapper)
        {
            _doctorScheduleRepository = doctorScheduleRepository;
            _mapper = mapper;
        }
        public async Task<PagedResult<DoctorScheduleDTO>> Handle(GetDoctorSchedulesCommand request, CancellationToken cancellationToken)
        {
            PagedResult<DoctorSchedule> schedules = await _doctorScheduleRepository.GetAllByDoctorIdAsync(request.DoctorId, request.PageNumber, request.PageSize);
            return _mapper.Map<PagedResult<DoctorScheduleDTO>>(schedules);
        }
    }
}
