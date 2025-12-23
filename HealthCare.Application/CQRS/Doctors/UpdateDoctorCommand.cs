using AutoMapper;
using HealthCare.Application.DTOs;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Interfaces.Repositories;
using MediatR;

namespace HealthCare.Application.CQRS.Doctors
{
    public sealed record UpdateDoctorCommand(DoctorDTO Doctor) : IRequest<DoctorDTO>;

    public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, DoctorDTO>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        public UpdateDoctorCommandHandler(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }
        public async Task<DoctorDTO> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            Doctor doctor = _mapper.Map<Doctor>(request.Doctor);
            Doctor updated = await _doctorRepository.UpdateAsync(doctor);

            return _mapper.Map<DoctorDTO>(updated);
        }
    }
}
