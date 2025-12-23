using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Common.Extentions
{
    public static class DateTimeExtensions
    {
        public static DateTime RoundToFiveMinutes(this DateTime localTime)
        {
            var ticks = TimeSpan.FromMinutes(5).Ticks;
            return new DateTime((
                (localTime.Ticks + ticks - 1) / ticks) * ticks,
                localTime.Kind
            );
        }

		public static bool IsOverlapping(this DateTime start, DateTime end)
		{
			return start >= end;
		}
	}
}
