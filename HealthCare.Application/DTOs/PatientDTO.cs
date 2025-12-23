using BuildingBlocks.Shared.DTOs;
using HealthCare.Application.DTOs.Base;

namespace HealthCare.Application.DTOs
{
    public class CreatePatientDTO : PersonDTO
    {
        public required string Name { get; set; }
        public int Age { get; set; }
        public required string Gender { get; set; }
    }

    public class PatientDTO : BaseObjectDTO, PersonDTO
    {
        public required string Name { get; set; }
        public int Age { get; set; }
        public required string Gender { get; set; }
        public List<AppointmentDTO> Appointments { get; set; } = [];
    }
}
