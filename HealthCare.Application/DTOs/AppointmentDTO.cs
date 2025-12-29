using BuildingBlocks.Shared.DTOs;

namespace HealthCare.Application.DTOs
{
    public class CreateAppointmentDTO
    {
        public required int DoctorId { get; set; }
        public required int PatientId { get; set; }
        public required DateTime StartUtc { get; set; }
        public required int Duration { get; set; }
    }

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
