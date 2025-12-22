using BuildingBlocks.Shared.Entities;
using HealthCare.Domain.Entities.Base;
using HealthCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Domain.Entities
{
    public class Patient : Person
    {
        public ICollection<Appointment> Appointments { get; set; } = [];
    }
}
