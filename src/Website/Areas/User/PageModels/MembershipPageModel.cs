namespace BrickAtHeart.Communities.Areas.User.PageModels
{
    public class MembershipPageModel
    {
        public long CommunityId { get; set; }

        public string DisplayName { get; set; }

        public long Id { get; set; }

        public bool IsActive { get; set; }

        public bool IsCurrent { get; set; }
    }
}
