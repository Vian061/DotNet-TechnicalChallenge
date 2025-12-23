using BuildingBlocks.Shared.DTOs;

namespace HealthCare.Application.DTOs
{
    public class CreateDoctorScheduleDTO
    {
        public int DoctorId { get; set; }
        public required string DaysOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

    public class UpdateDoctorScheduleDTO : BaseIdDTO
    {
        public int DoctorId { get; set; }
        public required string DaysOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

    public class DoctorScheduleDTO : BaseObjectDTO
    {
        public DoctorDTO Doctor { get; set; } = default!;
        public string DaysOfWeek { get; set; } = default!;

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
