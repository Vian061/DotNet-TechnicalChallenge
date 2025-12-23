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
