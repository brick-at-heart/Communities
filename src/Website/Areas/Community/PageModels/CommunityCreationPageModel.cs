using BrickAtHeart.Communities.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BrickAtHeart.Communities.Areas.Community.PageModels
{
    public class CommunityCreationPageModel
    {
        [Required]
        [MaxLength(256)]
        [DisplayName("Full Name")]
        public string FullName { get; set; }

        [Required]
        [MaxLength(64)]
        [DisplayName("Short Name")]
        public string ShortName { get; set; }

        [DisplayName("Slack Workspace")]
        public string SlackWorkspaceId { get; set; }

        [Required]
        [DisplayName("Join Type")]
        public CommunityJoinType JoinType { get; set; }

        public string RedirectUrl { get; set; }
    }
}
