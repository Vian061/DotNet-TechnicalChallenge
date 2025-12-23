using AutoMapper;
using BuildingBlocks.Shared.Entities;
using HealthCare.Application.DTOs;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Application.CQRS.Doctors
{
    public sealed record GetDoctorsCommand(int PageNumber, int PageSize): IRequest<PagedResult<DoctorDTO>>;
    
    public class GetDoctorsCommandHandler : IRequestHandler<GetDoctorsCommand, PagedResult<DoctorDTO>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        public GetDoctorsCommandHandler(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }
        public async Task<PagedResult<DoctorDTO>> Handle(GetDoctorsCommand request, CancellationToken cancellationToken)
        {
            PagedResult<Doctor> doctors = await _doctorRepository.GetAllAsync(request.PageNumber, request.PageSize);
            return _mapper.Map<PagedResult<DoctorDTO>>(doctors);
        }
	}
}
