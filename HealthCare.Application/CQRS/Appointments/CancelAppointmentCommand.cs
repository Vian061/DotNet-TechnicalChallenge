using BuildingBlocks.Common.Exceptions;
using HealthCare.Domain.Enums;
using HealthCare.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Application.CQRS.Appointments
{
    public sealed record CancelAppointmentCommand(int AppointmentId) : IRequest;

    public sealed class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointmentRepo;

        private static readonly TimeSpan CutOff = TimeSpan.FromHours(2);

        public CancelAppointmentCommandHandler(
            IAppointmentRepository appointmentRepo
            )
        {
            _appointmentRepo = appointmentRepo;
        }

        public async Task Handle(
            CancelAppointmentCommand request,
            CancellationToken ct)
        {
            var appointment = await _appointmentRepo
            .GetByIdAsync(request.AppointmentId)
            ?? throw new NotFoundException("Appointment not found");

            if (appointment.Status == AppointmentStatus.Cancelled)
                return; // idempotent

            var nowUtc = DateTime.UtcNow;

            if (nowUtc >= appointment.StartUtc.Subtract(CutOff))
                throw new ConflictException(
                    "Appointment cannot be cancelled within 2 hours of start time");

            await _appointmentRepo.CancelAppointment(appointment);
        }
    }
}
