using BuildingBlocks.Common.Extentions;
using BuildingBlocks.Shared.Entities;
using HealthCare.Application.CQRS.Doctors;
using HealthCare.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DoctorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctors(int PageNumber = 1, int PageSize = 20)
        {
            PagedResult<DoctorDTO> result = await _mediator.Send(new GetDoctorsCommand(PageNumber, PageSize));
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            DoctorDTO? doctor = await _mediator.Send(new GetDoctorByIdCommand(id));
            return Ok(doctor);
        }

        [HttpGet]
        [Route("{id:int}/availability")]
        public async Task<IActionResult> GetDoctorAvailability(int id, DateTime from, DateTime to, int slot )
        {
            if(from.IsOverlapping(to))
            {
                return BadRequest("The 'from' date must be earlier than the 'to' date.");
            }

            if (slot is not (15 or 30 or 60))
                return BadRequest("slot must be 15, 30, or 60 minutes");

            var result = await _mediator.Send(new GetDoctorAvailabilityCommand(id, from, to, slot));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorDTO doctorDto)
        {
            DoctorDTO createdDoctor = await _mediator.Send(new CreateDoctorCommand(doctorDto));
            return Ok(createdDoctor);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDoctor([FromBody] DoctorDTO doctorDto)
        {
            DoctorDTO updatedDoctor = await _mediator.Send(new UpdateDoctorCommand(doctorDto));
            return Ok(updatedDoctor);
        }
    }
}
