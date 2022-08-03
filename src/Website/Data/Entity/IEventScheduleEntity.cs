using System;

namespace BrickAtHeart.Communities.Data.Entity
{
    public interface IEventScheduleEntity
    {
        public DateTimeOffset End { get; set; }

        public long EventId { get; set; }

        public long Id { get; set; }

        public DateTimeOffset Start { get; set; }
    }
}
