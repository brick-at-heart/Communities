namespace BrickAtHeart.Communities.Data.Entity
{
    public class RoleEntity : IRoleEntity
    {
        public long Id { get; set; }

        public bool IsCommunityDefault { get; set; }

        public bool IsSystemGeneratedOwner { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public long CommunityId { get; set; }
    }
}
