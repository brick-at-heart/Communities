namespace BrickAtHeart.Communities.Data.Entity
{
    public interface IEventEntity
    {
        public long CommunityId { get; set; }

        public string Description { get; set; }

        public long Id { get; set; }

        public string Location { get; set; }

        public string Name { get; set; }

        public byte Status { get; set; }
    }
}
