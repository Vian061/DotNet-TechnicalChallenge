using BuildingBlocks.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Application.DTOs.Base
{
	public class PersonDTO : BaseObjectDTO
	{
		public string Name { get; set; } = default!;
		public int Age { get; set; }
		public required string Gender { get; set; }
	}
}
