using HealthCare.Domain.Entities.Base;

namespace HealthCare.Domain.Entities
{
    public class Doctor : Person
    {
        public ICollection<DoctorSchedule> Schedules { get; set; } = [];
        public ICollection<Appointment> Appointments { get; set; } = [];
        public string TimeZone { get; set; } = "Asia/Jakarta";
    }
}
