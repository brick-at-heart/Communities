using BrickAtHeart.Communities.Models;
using BrickAtHeart.Communities.Models.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.Community.Pages.Events
{
    public class EventsIndexModel : CommunityBasePageModel
    {
        [BindProperty(SupportsGet = true)]
        public List<Event> Events { get; set; }

        public string EventFilter { get; set;}

        public EventsIndexModel(UserStore userStore,
                                MembershipStore membershipStore,
                                CommunityStore communityStore,
                                EventStore eventStore,
                                ILogger<EventsIndexModel> logger) :
            base(userStore, membershipStore, communityStore)
        {
            this.eventStore = eventStore;
            this.logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(string filter = "Scheduled")
        {
            Models.Community community = await GetCurrentCommunityForUser(User);
            Events = (List<Event>)await eventStore.RetrieveEventByCommunityIdAsync(community.Id, true);

            if (filter == "Prior")
            {
                EventFilter = "Prior";
                Events = Events.Where(e => e.Schedules.Max(d => d.End) < System.DateTimeOffset.UtcNow)
                               .OrderByDescending(e => e.Schedules.Max(d => d.End))
                               .ToList();
            }
            else if (filter == "Cancelled")
            {
                EventFilter = "Cancelled";
                Events = Events.Where(e => e.Status == EventStatus.Cancelled && e.Schedules.Max(d => d.End) > System.DateTimeOffset.UtcNow)
                               .OrderBy(e => e.Schedules.Min(d => d.Start))
                               .ToList();
            }
            else
            {
                EventFilter = "Scheduled";
                Events = Events.Where(e => e.Status == EventStatus.Scheduled && e.Schedules.Max(d => d.End) > System.DateTimeOffset.UtcNow)
                               .OrderBy(e => e.Schedules.Min(d => d.Start))
                               .ToList();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostCancelEventAsync(long eventId, string filter = "Scheduled")
        {
            Event @event = await eventStore.RetrieveEventByEventIdAsync(eventId);
            @event.Status = EventStatus.Cancelled;
            await eventStore.UpdateEventAsync(@event);

            logger.LogInformation($"Event with Id ({@event.Id}) was cancelled.");

            return RedirectToPage(new { filter = filter });
        }

        public async Task<IActionResult> OnPostDeleteEventAsync(long eventId, string filter = "Scheduled")
        {
            await eventStore.DeleteEventAsync(eventId);

            logger.LogInformation($"Event with Id ({eventId}) was deleted.");

            return RedirectToPage( new { filter = filter });
        }

        private readonly EventStore eventStore;
        private readonly ILogger<EventsIndexModel> logger;
    }
}
