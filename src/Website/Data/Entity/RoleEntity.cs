namespace BrickAtHeart.Communities.Data.Entity
{
    public class RoleEntity : IRoleEntity
    {
        public long Id { get; set; }

        public bool IsDefault { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public long CommunityId { get; set; }

        public RoleEntity(string name, string normalizedName)
        {
            Name = name;
            NormalizedName = normalizedName;
        }
    }
}