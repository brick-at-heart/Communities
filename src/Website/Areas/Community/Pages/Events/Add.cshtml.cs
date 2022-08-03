using BrickAtHeart.Communities.Areas.Community.PageModels;
using BrickAtHeart.Communities.Models;
using BrickAtHeart.Communities.Models.Events;
using BrickAtHeart.Communities.Models.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Areas.Community.Pages.Events
{
    [Authorize(Policy = "CanCreateEvent")]
    public class EventsAddModel : CommunityBasePageModel
    {
        [BindProperty]
        public EventPageModel Event { get; set; }

        public EventsAddModel(UserStore userStore,
                              MembershipStore membershipStore,
                              CommunityStore communityStore,
                              EventStore eventStore,
                              ILogger<EventsAddModel> logger) :
            base(userStore, membershipStore, communityStore)
        {
            this.eventStore = eventStore;
            this.logger = logger;
        }

        public async Task OnGetAsync()
        {
            Models.Community community = await GetCurrentCommunityForUser(User);

            Event = new EventPageModel
            {
                CommunityId = community.Id,
                CommunityDisplayName = community.DisplayName
            };
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            Validation();

            if (ModelState.IsValid)
            {
                Event @event = new Event
                {
                    CommunityId = Event.CommunityId,
                    Name = Event.EventName,
                    Description = Event.Description,
                    Location = Event.Location,
                    Status = EventStatus.Scheduled
                };

                await eventStore.CreateEventAsync(@event);

                EventSchedule schedule = new EventSchedule
                {
                    EventId = @event.Id,
                    Start = TimeZoneInfo.ConvertTimeToUtc(Event.StartDate.Value.AddHours(Event.StartTime.Value), User.GetTimeZoneInfo(userStore)),
                    End = TimeZoneInfo.ConvertTimeToUtc(Event.EndDate.Value.AddHours(Event.EndTime.Value), User.GetTimeZoneInfo(userStore))
                };

                await eventStore.CreateEventScheduleAsync(schedule);

                StatusMessage = $"{@event.Name} event created successfully.";
                logger.LogInformation($"Event with Id ({@event.Id}) was created");

                return RedirectToPage("./Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostSaveAndNewAsync()
        {
            Validation();

            if (ModelState.IsValid)
            {
                Event @event = new Event
                {
                    CommunityId = Event.CommunityId,
                    Name = Event.EventName,
                    Description = Event.Description,
                    Location = Event.Location,
                    Status = EventStatus.Scheduled
                };

                await eventStore.CreateEventAsync(@event);

                EventSchedule schedule = new EventSchedule
                {
                    EventId = @event.Id,
                    Start = TimeZoneInfo.ConvertTimeToUtc(Event.StartDate.Value.AddHours(Event.StartTime.Value), User.GetTimeZoneInfo(userStore)),
                    End = TimeZoneInfo.ConvertTimeToUtc(Event.EndDate.Value.AddHours(Event.EndTime.Value), User.GetTimeZoneInfo(userStore))
                };

                await eventStore.CreateEventScheduleAsync(schedule);

                StatusMessage = $"{@event.Name} event created successfully.";
                logger.LogInformation($"Event with Id ({@event.Id}) was created");

                return RedirectToPage();
            }

            return Page();
        }

        private void Validation()
        {
            DateTime start = TimeZoneInfo.ConvertTimeToUtc(Event.StartDate.Value.AddHours(Event.StartTime.Value), User.GetTimeZoneInfo(userStore));
            DateTime end = TimeZoneInfo.ConvertTimeToUtc(Event.EndDate.Value.AddHours(Event.EndTime.Value), User.GetTimeZoneInfo(userStore));

            if (end < start)
            {
                ModelState.AddModelError("EndDate", "The End Date and Time must come after the Start Date and Time.");
                ModelState.AddModelError("EndTime", "");
            }

            if (end < DateTime.UtcNow)
            {
                ModelState.AddModelError("EndDate", "The End Date and Time must come after Now. You cannot schedule an event which ends in the past.");
                ModelState.AddModelError("EndTime", "");
            }
        }

        private readonly EventStore eventStore;
        private readonly ILogger<EventsAddModel> logger;
    }
}
