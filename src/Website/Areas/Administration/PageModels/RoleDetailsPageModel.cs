using BrickAtHeart.Communities.Models.Authorization;
using System.Collections.Generic;

namespace BrickAtHeart.Communities.Areas.Administration.PageModels
{
    public class RoleDetailsPageModel
    {
        public long Id { get; set; }

        public bool? IdDefault { get; set; }

        public string Name { get; set; }

        public List<RoleRight> Rights { get; set; }

        public List<Models.User> Users { get; set; }
    }
}
