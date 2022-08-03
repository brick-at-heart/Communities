using System.Diagnostics;

namespace BrickAtHeart.Communities.Data.Entity
{
    [DebuggerDisplay("{Name}, Id = {Id}, Community Id = {CommunityId}")]
    public class EventEntity : IEventEntity
    {
        public long CommunityId { get; set; }

        public string Description { get; set; }

        public long Id { get; set; }

        public string Location { get; set; }

        public string Name { get; set; }

        public byte Status { get; set; }
    }
}
