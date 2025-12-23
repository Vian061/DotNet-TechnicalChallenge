using BuildingBlocks.Common.Exceptions;
using BuildingBlocks.Shared.Entities;
using HealthCare.Domain.Enums;

namespace HealthCare.Domain.Entities
{
    public class DoctorSchedule : BaseObject
    {
        public DoctorSchedule(
        int doctorId,
        DayOfWeekFlags daysOfWeek,
        TimeSpan startTime,
        TimeSpan endTime)
        {
            if (startTime >= endTime)
                throw new ValidationException("StartTime must be before EndTime");

            DoctorId = doctorId;
            DaysOfWeek = daysOfWeek;
            StartTime = startTime;
            EndTime = endTime;
        }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = default!;
        public DayOfWeekFlags DaysOfWeek { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public bool IsActive { get; set; } = true;
        public void Deactivate() => IsActive = false;
    }
}
