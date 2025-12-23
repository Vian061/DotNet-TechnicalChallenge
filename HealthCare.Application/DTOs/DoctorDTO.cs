using HealthCare.Application.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Application.DTOs
{
    public class DoctorDTO : PersonDTO
    {
		public List<DoctorScheduleDTO> Schedules { get; set; } = [];
		public List<AppointmentDTO> Appointments { get; set; } = [];
		public string TimeZone { get; set; } = "Asia/Jakarta";
	}
}
