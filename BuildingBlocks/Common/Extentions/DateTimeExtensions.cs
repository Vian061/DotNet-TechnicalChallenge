namespace BuildingBlocks.Common.Extentions
{
    public static class DateTimeExtensions
    {
        public static DateTime RoundToFiveMinutes(this DateTime localTime)
        {
            var ticks = TimeSpan.FromMinutes(5).Ticks;
            /// ((a + b - 1) / b) * b  => Ceiling division
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
