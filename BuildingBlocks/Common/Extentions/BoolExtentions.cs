using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Common.Extentions
{
    public static class BoolExtentions
    {
        public static bool IsBetween(
            this DateTime target,
            DateTime start,
            DateTime end)
        {
            return target >= start && target <= end;
        }

        public static bool IsOverlapping(DateTime start, DateTime end)
        {
            return start >= end;
        }

        public static bool IsOverlapping(
            DateTime start1, DateTime end1,
            DateTime start2, DateTime end2)
        {
            return start1 < end2 && end1 > start2;
        }
    }
}
