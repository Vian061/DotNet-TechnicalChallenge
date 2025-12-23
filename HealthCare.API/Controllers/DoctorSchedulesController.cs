using BuildingBlocks.Shared.Entities;
using HealthCare.Application.CQRS.DoctorSchedules;
using HealthCare.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class DoctorSchedulesController : ControllerBase
	{
		private readonly IMediator _mediator;

		public DoctorSchedulesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetDoctorSchedules(int doctorId, int PageNumber = 1, int PageSize = 20)
		{
			PagedResult<DoctorScheduleDTO> result = await _mediator.Send(new GetDoctorSchedulesCommand(doctorId, PageNumber, PageSize));
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateDoctorSchedule([FromBody] CreateDoctorScheduleDTO scheduleDto)
		{
			DoctorScheduleDTO createdSchedule = await _mediator.Send(new CreateDoctorScheduleCommand(scheduleDto));
			return Ok(createdSchedule);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateDoctorSchedule([FromBody] UpdateDoctorScheduleDTO scheduleDto)
		{
			DoctorScheduleDTO updatedSchedule = await _mediator.Send(new UpdateDoctorScheduleCommand(scheduleDto));
			return Ok(updatedSchedule);
		}
	}
}
