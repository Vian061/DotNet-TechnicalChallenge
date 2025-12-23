using AutoMapper;
using HealthCare.Application.DTOs;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Interfaces.Repositories;
using MediatR;

namespace HealthCare.Application.CQRS.Patients
{
    public sealed record CreatePatientCommand(CreatePatientDTO Patient) : IRequest<PatientDTO>;

    public sealed class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, PatientDTO>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        public CreatePatientCommandHandler(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<PatientDTO> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            Patient patient = _mapper.Map<Patient>(request.Patient);

            Patient createdPatient = await _patientRepository.CreateAsync(patient);
            return _mapper.Map<PatientDTO>(createdPatient);
        }
    }
}
