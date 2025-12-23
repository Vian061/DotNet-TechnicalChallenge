using HealthCare.Domain.Entities.Base;

namespace HealthCare.Domain.Entities
{
    public class Patient : Person
    {
        public ICollection<Appointment> Appointments { get; set; } = [];
    }
}
