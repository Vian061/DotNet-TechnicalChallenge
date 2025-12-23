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
