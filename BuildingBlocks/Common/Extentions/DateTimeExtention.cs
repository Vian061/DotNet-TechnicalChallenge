using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Common.Extentions
{
    public static class DateTimeExtention
    {
        public static DateTime RoundToFiveMinutes(DateTime localTime)
        {
            var ticks = TimeSpan.FromMinutes(5).Ticks;
            return new DateTime(
                ((localTime.Ticks + ticks - 1) / ticks) * ticks,
                localTime.Kind
            );
        }
    }
}
