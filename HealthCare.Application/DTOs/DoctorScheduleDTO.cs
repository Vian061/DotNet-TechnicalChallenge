using BuildingBlocks.Shared.DTOs;

namespace HealthCare.Application.DTOs
{
    public class CreateDoctorScheduleDTO
    {
        public required int DoctorId { get; set; }
        public required string DaysOfWeek { get; set; }
        public required TimeSpan StartTime { get; set; }
        public required TimeSpan EndTime { get; set; }
    }

    public class UpdateDoctorScheduleDTO : BaseIdDTO
    {
        public required int DoctorId { get; set; }
        public required string DaysOfWeek { get; set; }
        public required TimeSpan StartTime { get; set; }
        public required TimeSpan EndTime { get; set; }
    }

    public class DoctorScheduleDTO : BaseObjectDTO
    {
        //public DoctorDTO Doctor { get; set; } = default!;
        public string DaysOfWeek { get; set; } = default!;

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
