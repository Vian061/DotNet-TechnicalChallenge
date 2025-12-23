using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Application.ValueObjects
{
	public record AvailabilitySlot(
		DateTime StartUtc,
		DateTime EndUtc
	);
}
