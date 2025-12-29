using HealthCare.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Domain.Extensions
{
    public static class EnumExtensions
    {
		public static List<string> ToReadableList(this DayOfWeekFlags flags)
		{
			return [.. Enum.GetValues<DayOfWeekFlags>()
				.Where(d => d != DayOfWeekFlags.None && flags.HasFlag(d))
				.Select(d => d.ToString())];
		}
	}
}
