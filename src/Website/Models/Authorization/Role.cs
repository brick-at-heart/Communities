using Microsoft.AspNetCore.Identity;

namespace BrickAtHeart.Communities.Models.Authorization
{
    public class Role : IdentityRole<long>
    {
        public long CommunityId { get; set; }

        public bool IsDefault { get; set; }
    }
}