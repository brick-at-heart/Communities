using System;

namespace BrickAtHeart.Communities.Models.Events
{
    public class EventSchedule
    {
        public DateTimeOffset End { get; set; }

        public long EventId { get; set; }

        public long Id { get; set; }

        public DateTimeOffset Start { get; set; }

        public string ToString(TimeZoneInfo tzi)
        {
            DateTime start = TimeZoneInfo.ConvertTimeFromUtc(Start.UtcDateTime, tzi);
            DateTime end = TimeZoneInfo.ConvertTimeFromUtc(End.UtcDateTime, tzi);

            if (start.Date.Equals(end.Date))
            {
                return $"{start.ToString("g")} - {end.ToString("t")}";
            }

            return $"{start.ToString("g")} - {end.ToString("g")}";
        }
    }
}
