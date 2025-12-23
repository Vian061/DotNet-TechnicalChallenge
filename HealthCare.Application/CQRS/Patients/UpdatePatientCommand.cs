using AutoMapper;
using HealthCare.Application.DTOs;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Interfaces.Repositories;
using MediatR;

namespace HealthCare.Application.CQRS.Patients
{
    public sealed record UpdatePatientCommand(PatientDTO Patient) : IRequest<PatientDTO>;

    public sealed class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, PatientDTO>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        public UpdatePatientCommandHandler(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }
        public async Task<PatientDTO> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            Patient patient = _mapper.Map<Patient>(request.Patient);
            Patient updated = await _patientRepository.UpdateAsync(patient);
            return _mapper.Map<PatientDTO>(updated);
        }
    }
}
