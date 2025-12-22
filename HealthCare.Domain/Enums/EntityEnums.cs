using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Domain.Enums
{
    public enum DayOfWeekFlags
    {
        Monday = 10,
        Tuesday = 20,
        Wednesday = 30,
        Thursday = 40,
        Friday = 50,
        Saturday = 60,
        Sunday = 70
    }

    public enum Gender
    {
        Male,
        Female
    }

    public enum AppointmentStatus
    {
        Active = 10,
        Cancelled = 20
    }
}
