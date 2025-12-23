using BuildingBlocks.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Application.DTOs.Base
{
	public interface PersonDTO
	{
		public string Name { get; set; }
		public int Age { get; set; }
		public string Gender { get; set; }
	}
}
