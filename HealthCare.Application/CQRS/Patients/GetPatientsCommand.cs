using AutoMapper;
using BuildingBlocks.Shared.Entities;
using HealthCare.Application.DTOs;
using HealthCare.Domain.Entities;
using HealthCare.Domain.Interfaces.Repositories;
using MediatR;

namespace HealthCare.Application.CQRS.Patients
{
    public sealed record GetPatientsCommand(int PageNumber, int PageSize) : IRequest<PagedResult<PatientDTO>>;

    public sealed class GetPatientsCommandHandler : IRequestHandler<GetPatientsCommand, PagedResult<PatientDTO>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        public GetPatientsCommandHandler(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<PatientDTO>> Handle(GetPatientsCommand request, CancellationToken cancellationToken)
        {
            PagedResult<Patient> result = await _patientRepository.GetAllAsync(request.PageNumber, request.PageSize);
            return _mapper.Map<PagedResult<PatientDTO>>(result);
        }
    }
}
