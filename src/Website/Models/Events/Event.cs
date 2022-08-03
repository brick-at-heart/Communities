using System.Collections.Generic;

namespace BrickAtHeart.Communities.Models.Events
{
    public class Event
    {
        public long CommunityId { get; set; }

        public string Description { get; set; }

        public long Id { get; set; }

        public string Location { get; set; }

        public string Name { get; set; }

        public IList<EventSchedule> Schedules { get; set; }

        public EventStatus Status { get; set; }
    }
}
