using HealthCare.Application.CQRS.Patients;
using HealthCare.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatienController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatienController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPatients(int PageNumber = 1, int PageSize = 20)
        {
            var result = await _mediator.Send(new GetPatientsCommand(PageNumber, PageSize));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientDTO patientDto)
        {
            var createdPatient = await _mediator.Send(new CreatePatientCommand(patientDto));
            return Ok(createdPatient);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePatient([FromBody] PatientDTO patientDto)
        {
            var updatedPatient = await _mediator.Send(new UpdatePatientCommand(patientDto));
            return Ok(updatedPatient);
        }
    }
}
