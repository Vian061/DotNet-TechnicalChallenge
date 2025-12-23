using BuildingBlocks.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace HealthCare.Application.DTOs
{
	public class AppointmentDTO : BaseObjectDTO
	{
		public DoctorDTO Doctor { get; set; } = default!;
		public PatientDTO Patient { get; set; } = default!;

		public DateTime StartUtc { get; set; }
		public DateTime EndUtc { get; set; }

		public string Status { get; set; } = default!;

		public DateTime? CancelledAtUtc { get; set; }
	}
}
