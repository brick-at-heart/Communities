using System;
using System.Diagnostics;

namespace BrickAtHeart.Communities.Data.Entity
{
    [DebuggerDisplay("Id = {Id}, Event Id = {EventId}, Start = {Start}, End = {End}")]
    public class EventScheduleEntity : IEventScheduleEntity
    {
        public DateTimeOffset End { get; set; }

        public long EventId { get; set; }

        public long Id { get; set; }

        public DateTimeOffset Start { get; set; }
    }
}
