using HealthCare.Application.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Application.DTOs
{
	public class CreateDoctorDTO : PersonDTO
	{
		public string TimeZone { get; set; } = "Asia/Jakarta";
	}

    public class DoctorDTO : CreateDoctorDTO
    {
		public List<DoctorScheduleDTO> Schedules { get; set; } = [];
		public List<AppointmentDTO> Appointments { get; set; } = [];
	}
}
