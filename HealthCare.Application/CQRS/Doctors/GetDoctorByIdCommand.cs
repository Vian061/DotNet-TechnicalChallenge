using AutoMapper;
using HealthCare.Application.DTOs;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Application.CQRS.Doctors
{
    public sealed record GetDoctorByIdCommand(int Id) : IRequest<DoctorDTO?>;

    public class GetDoctorByIdCommandHandler : IRequestHandler<GetDoctorByIdCommand, DoctorDTO?>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        public GetDoctorByIdCommandHandler(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }
        public async Task<DoctorDTO?> Handle(GetDoctorByIdCommand request, CancellationToken cancellationToken)
        {
            Doctor? doctor = await _doctorRepository.GetByIdAsync(request.Id);
            return _mapper.Map<DoctorDTO?>(doctor);
        }
	}
}
