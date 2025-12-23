using BuildingBlocks.Shared.Entities;
using HealthCare.Domain.Enums;

namespace HealthCare.Domain.Entities
{
    public class Appointment : BaseObject
    {
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = default!;
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = default!;

        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Active;

        public DateTime? CancelledAtUtc { get; set; }
    }
}
