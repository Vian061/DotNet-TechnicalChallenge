using HealthCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Application.Extentions
{
    public static class DateTimeExtentions
    {
        public static DayOfWeekFlags ToFlag(this DayOfWeek day) => day switch
        {
            DayOfWeek.Monday => DayOfWeekFlags.Monday,
            DayOfWeek.Tuesday => DayOfWeekFlags.Tuesday,
            DayOfWeek.Wednesday => DayOfWeekFlags.Wednesday,
            DayOfWeek.Thursday => DayOfWeekFlags.Thursday,
            DayOfWeek.Friday => DayOfWeekFlags.Friday,
            DayOfWeek.Saturday => DayOfWeekFlags.Saturday,
            DayOfWeek.Sunday => DayOfWeekFlags.Sunday,
            _ => DayOfWeekFlags.None
        };
    }
}
