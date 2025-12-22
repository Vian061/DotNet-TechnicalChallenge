using BuildingBlocks.Shared.Entities;
using HealthCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Domain.Entities
{
    public class DoctorSchedule : BaseObject
    {
        public Doctor Doctor { get; set; } = default!;
        public DayOfWeekFlags DaysOfWeek { get; set; }

        public TimeSpan StartTime { get; set; } 
        public TimeSpan EndTime { get; set; }   

        public bool IsActive { get; set; } = true;
    }
}
