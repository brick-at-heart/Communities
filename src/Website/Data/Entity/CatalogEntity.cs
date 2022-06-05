using System.Diagnostics;

namespace BrickAtHeart.Communities.Data.Entity
{
    [DebuggerDisplay("{Name}, Id = {Id}, User Group Id = {UserGroupId}")]
    public class CatalogEntity : ICatalogEntity
    {
        public static bool operator == (CatalogEntity entityA, CatalogEntity entityB)
        {
            return entityA.NormalizedName == entityB.NormalizedName;
        }

        public static bool operator != (CatalogEntity entityA, CatalogEntity entityB)
        {
            return entityA.NormalizedName != entityB.NormalizedName;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public long UserGroupId { get; set; }

        public CatalogEntity( string name,
                              string normalizedName )
        {
            Name = name;
            NormalizedName = normalizedName;
        }

        public override bool Equals(object? entityA)
        {
            if (entityA is CatalogEntity entity)
            {
                return NormalizedName == entity.NormalizedName;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return NormalizedName.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}