namespace BrickAtHeart.Communities.Areas.Administration.PageModels
{
    public class MembershipDetailsPageModel
    {
        public long Id { get; set; }

        public string FullName { get; set; }

        public bool IsActive { get; set; }

        public bool IsPrimary { get; set; }
    }
}
