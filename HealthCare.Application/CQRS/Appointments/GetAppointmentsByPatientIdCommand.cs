using AutoMapper;
using BuildingBlocks.Shared.Entities;
using Hangfire;
using HealthCare.Application.DTOs;
using HealthCare.Application.Services;
using HealthCare.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Application.CQRS.Appointments
{
	public sealed record GetAppointmentsByPatientIdCommand(int PatientId, int PageNumber = 1, int PageSize = 20) : IRequest<PagedResult<AppointmentDTO>>;

	public sealed class GetAppointmentsByPatientIdCommandHandler : IRequestHandler<GetAppointmentsByPatientIdCommand, PagedResult<AppointmentDTO>>
	{
		private readonly IAppointmentRepository _appointmentRepo;
		private readonly IMapper _mapper;
		public GetAppointmentsByPatientIdCommandHandler(
			IAppointmentRepository appointmentRepo,
			IMapper mapper)
		{
			_appointmentRepo = appointmentRepo;
			_mapper = mapper;
		}
		public async Task<PagedResult<AppointmentDTO>> Handle(
			GetAppointmentsByPatientIdCommand request,
			CancellationToken ct)
		{
			var pagedAppointments = await _appointmentRepo.GetByPatientIdAsync(request.PatientId, request.PageNumber, request.PageSize);
			return _mapper.Map<PagedResult<AppointmentDTO>>(pagedAppointments);
		}
	}
}
