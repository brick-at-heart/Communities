namespace BrickAtHeart.Communities.Areas.Community.PageModels
{
    public class CommunityDisplayPageModel
    {
        public string DisplayName { get; set;}

        public long Id { get; set; }

        public long MembershipId { get; set;} = -1;
    }
}