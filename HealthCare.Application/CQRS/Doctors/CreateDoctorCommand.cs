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
    public record CreateDoctorCommand(CreateDoctorDTO Doctor) : IRequest<DoctorDTO>;

    public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, DoctorDTO>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        public CreateDoctorCommandHandler(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<DoctorDTO> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            Doctor doctor = _mapper.Map<Doctor>(request.Doctor);
            Doctor created = await _doctorRepository.CreateAsync(doctor);
            return _mapper.Map<DoctorDTO>(created);
        }
	}
}
