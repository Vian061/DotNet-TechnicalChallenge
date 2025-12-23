using HealthCare.Application.DTOs.Base;

namespace HealthCare.Application.DTOs
{
    internal class PatientDTO : PersonDTO
	{
		public List<AppointmentDTO> Appointments { get; set; } = [];
	}
}
