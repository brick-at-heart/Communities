using BrickAtHeart.Communities.Models;
using BrickAtHeart.Communities.Models.Events;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Pages
{
    public class IndexModel : CommunityBasePageModel
    {
        public List<Event> UpcomingEvents { get; set; }

        public IndexModel(UserStore userStore,
                          MembershipStore membershipStore,
                          CommunityStore communityStore,
                          EventStore eventStore,
                          ILogger<IndexModel> logger) :
            base(userStore, membershipStore, communityStore)
        {
            this.eventStore = eventStore;
            this.logger = logger;
        }

        public async Task OnGetAsync()
        {
            Community community = await GetCurrentCommunityForUser(User);

            if (community != null)
            { 
                UpcomingEvents = (List<Event>)await eventStore.RetrieveEventByCommunityIdAsync(community.Id, true);
                UpcomingEvents = UpcomingEvents.Where(e => e.Status == EventStatus.Scheduled && 
                                                           e.Schedules.Any(es => es.End > DateTime.UtcNow))
                                               .OrderBy(e => e.Schedules.Min(d => d.Start))
                                               .ToList();
            }
        }

        private readonly EventStore eventStore;
        private readonly ILogger<IndexModel> logger;
    }
}