using HealthCare.Application.DTOs.Base;

namespace HealthCare.Application.DTOs
{
    public class PatientDTO : PersonDTO
	{
		public List<AppointmentDTO> Appointments { get; set; } = [];
	}
}
