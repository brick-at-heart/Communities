using BrickAtHeart.Communities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BrickAtHeart.Communities.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : CommunityBasePageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
                
        public ErrorModel(UserStore userStore,
                          MembershipStore membershipStore,
                          CommunityStore communityStore,
                          ILogger<ErrorModel> logger) : 
            base(userStore, membershipStore, communityStore)
        {
            this.logger = logger;
        }

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }

        private readonly ILogger<ErrorModel> logger;
    }
}