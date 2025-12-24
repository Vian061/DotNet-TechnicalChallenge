using HealthCare.Application.CQRS.Appointments;
using HealthCare.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{patientId:int}")]
        public async Task<IActionResult> GetAllAppointments(int patientId, int pageNumber = 1, int pageSize = 20)
        {
            var appointments = await _mediator.Send(new GetAppointmentsByPatientIdCommand(patientId, pageNumber, pageSize));
            return Ok(appointments);
		}

		[HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDTO appointmentDto)
        {
            var createdAppointment = await _mediator.Send(new CreateAppointmentCommand(appointmentDto));
            return Ok(createdAppointment);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Cancel(int id)
        {
            await _mediator.Send(new CancelAppointmentCommand(id));
            return Ok();
        }
    }
}
