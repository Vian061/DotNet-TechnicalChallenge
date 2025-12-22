using BuildingBlocks.Shared.Entities;
using HealthCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Domain.Entities.Base
{
    public class Person : BaseObject
    {
        public string Name { get; set; } = default!;
        public int Age { get; set; }
        public Gender Gender { get; set; }
    }
}
