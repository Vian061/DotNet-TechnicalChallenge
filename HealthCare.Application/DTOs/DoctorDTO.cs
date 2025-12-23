using BuildingBlocks.Shared.DTOs;
using HealthCare.Application.DTOs.Base;

namespace HealthCare.Application.DTOs
{
    public class CreateDoctorDTO : PersonDTO
    {
        public string TimeZone { get; set; } = "Asia/Jakarta";
        public required string Name { get; set; }
        public int Age { get; set; }
        public required string Gender { get; set; }
    }

    public class DoctorDTO : BaseObjectDTO, PersonDTO
    {
        public string TimeZone { get; set; } = "Asia/Jakarta";
        public required string Name { get; set; }
        public int Age { get; set; }
        public required string Gender { get; set; }
        public List<DoctorScheduleDTO> Schedules { get; set; } = [];
        public List<AppointmentDTO> Appointments { get; set; } = [];
    }
}
