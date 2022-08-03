using BrickAtHeart.Communities.Areas.Community.PageModels;
using BrickAtHeart.Communities.Models;
using BrickAtHeart.Communities.Models.Events;
using BrickAtHeart.Communities.Models.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.Community.Pages.Events
{
    [Authorize(Policy = "CanUpdateEvent")]
    public class EventsEditModel : CommunityBasePageModel
    {
        [BindProperty(SupportsGet = true)]
        public EventPageModel Event { get; set; }

        public EventsEditModel(UserStore userStore,
                               MembershipStore membershipStore,
                               CommunityStore communityStore,
                               EventStore eventStore,
                               ILogger<EventsEditModel> logger) :
            base(userStore, membershipStore, communityStore)
        {
            this.eventStore = eventStore;
            this.logger = logger;
        }

        public async Task OnGetAsync(long eventId)
        {
            Event = new EventPageModel();

            if (eventId > 0)
            {
                Event @event = await eventStore.RetrieveEventByEventIdAsync(eventId, true);

                Event.EventId = @event.Id;
                Event.CommunityId = @event.CommunityId;
                Event.EventName = @event.Name;
                Event.Description = @event.Description;
                Event.Location = @event.Location;
                Event.Status = @event.Status;

                if (@event.Schedules.Any())
                {
                    Event.EventScheduleId = @event.Schedules.First().Id;

                    DateTime start = TimeZoneInfo.ConvertTimeFromUtc(@event.Schedules[0].Start.UtcDateTime, User.GetTimeZoneInfo(userStore));
                    DateTime end = TimeZoneInfo.ConvertTimeFromUtc(@event.Schedules[0].End.UtcDateTime, User.GetTimeZoneInfo(userStore));

                    Event.StartDate = start.Date;
                    Event.StartTime = start.TimeOfDay.TotalHours;
                    Event.EndDate = end.Date;
                    Event.EndTime = end.TimeOfDay.TotalHours;
                }

                Models.Community community = await communityStore.RetrieveCommunityByCommunityIdAsync(@event.CommunityId);
                Event.CommunityDisplayName = community.DisplayName;
            }

            ModelState.Clear();
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            if (ModelState.IsValid)
            {
                Event @event = new Event
                {
                    Id = Event.EventId,
                    CommunityId = Event.CommunityId,
                    Name = Event.EventName,
                    Description = Event.Description,
                    Location = Event.Location,
                    Status = Event.Status
                };

                await eventStore.UpdateEventAsync(@event);

                EventSchedule schedule = new EventSchedule
                {
                    Id = Event.EventScheduleId,
                    EventId = @event.Id,
                    Start = TimeZoneInfo.ConvertTimeToUtc(Event.StartDate.Value.AddHours(Event.StartTime.Value), User.GetTimeZoneInfo(userStore)),
                    End = TimeZoneInfo.ConvertTimeToUtc(Event.EndDate.Value.AddHours(Event.EndTime.Value), User.GetTimeZoneInfo(userStore))
                };

                await eventStore.UpdateEventScheduleAsync(schedule);

                StatusMessage = $"{@event.Name} event updated successfully.";
                logger.LogInformation($"Event with Id ({@event.Id}) was updated");

                return RedirectToPage("./Index");
            }

            return Page();
        }

        private readonly EventStore eventStore;
        private readonly ILogger<EventsEditModel> logger;
    }
}
