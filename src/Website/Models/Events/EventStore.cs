using BrickAtHeart.Communities.Data;
using BrickAtHeart.Communities.Data.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Models.Events
{
    public class EventStore
    {
        public EventStore(IEventDataClient eventDataClient,
                          ILogger<EventStore> logger)
        {
            this.eventDataClient = eventDataClient;
            this.logger = logger;
        }

        public async Task CreateEventAsync(Event @event,
                                           CancellationToken cancellationToken = new())
        {
            logger.LogDebug("Entered CreateEventAsync");

            IEventEntity entity = LoadEventEntity(@event);

            try
            {
                await eventDataClient.CreateEventAsync(entity, cancellationToken);
                @event.Id = entity.Id;

                logger.LogDebug("Leaving CreateEventAsync");
                return;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Exception thrown in CreateEventAsync");
                return;
            }
        }

        public async Task CreateEventScheduleAsync(EventSchedule eventSchedule,
                                                   CancellationToken cancellationToken = new())
        {
            logger.LogDebug("Entered CreateEventScheduleAsync");

            IEventScheduleEntity entity = LoadEventScheduleEntity(eventSchedule);

            try
            {
                await eventDataClient.CreateEventScheduleAsync(entity, cancellationToken);
                eventSchedule.Id = entity.Id;

                logger.LogDebug("Leaving CreateEventScheduleAsync");
                return;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Exception thrown CreateEventScheduleAsync");
                return;
            }
        }

        public async Task DeleteEventAsync(long eventId,
                                           CancellationToken cancellationToken = new())
        {
            logger.LogDebug("Entered DeleteEventAsync");

            await eventDataClient.DeleteEventAsync(eventId, cancellationToken);

            logger.LogDebug("Leaving DeleteEventAsync");
        }

        public async Task DeleteEventScheduleAsync(EventSchedule eventSchedule,
                                                   CancellationToken cancellationToken = new())
        {
            logger.LogDebug("Entered DeleteEventScheduleAsync");

            await eventDataClient.DeleteEventScheduleASync(eventSchedule.Id, cancellationToken);

            logger.LogDebug("Leaving DeleteEventScheduleAsync");
        }

        public async Task<IList<Event>> RetrieveEventByCommunityIdAsync(long communityId,
                                                                        bool loadEventSchedule = false,
                                                                        CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered RetrieveEventByCommunityIdAsync");

            try
            { 
                IList<IEventEntity> eventEntities = await eventDataClient.RetrieveEventByCommunityIdAsync(communityId, cancellationToken);
                IList<IEventScheduleEntity> eventScheduleEntities = loadEventSchedule
                    ? await eventDataClient.RetrieveEventScheduleByCommunityIdAsync(communityId, cancellationToken)
                    : new List<IEventScheduleEntity>();
                List<Event> result = LoadEventModels(eventEntities, eventScheduleEntities);

                logger.LogDebug("Leaving RetrieveEventByCommunityId");
                return result;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Exception thrown in RetrieveEventByCommunityIdAsync");
                return new List<Event>();
            }
        }

        public async Task<Event> RetrieveEventByEventIdAsync(long eventId,
                                                             bool loadEventSchedule = false,
                                                             CancellationToken cancellationToken = new())
        {
            logger.LogDebug("Entered RetrieveEventByEventIdAsync");

            try
            {
                IEventEntity eventEntity = await eventDataClient.RetrieveEventByEventIdAsync(eventId);
                IList<IEventScheduleEntity> eventScheduleEntities = loadEventSchedule
                    ? await eventDataClient.RetrieveEventScheduleByEventIdAsync(eventId, cancellationToken)
                    : new List<IEventScheduleEntity>();
                Event result = LoadEventModel(eventEntity, eventScheduleEntities);

                logger.LogDebug("Leaving RetrieveEventByEventIdAsync");

                return result;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Exception thrown in RetrieveEventByEventIdAsync");
                return null;
            }
        }

        public async Task UpdateEventAsync(Event @event,
                                           CancellationToken cancellationToken = new())
        {
            logger.LogDebug("Entered UpdateEventAsync");

            try
            {
                IEventEntity entity = LoadEventEntity(@event);
                await eventDataClient.UpdateEventAsync(entity);

                logger.LogDebug("Leaving UpdateEventAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Exception thrown in UpdateEventAsync");
            }
        }

        public async Task UpdateEventScheduleAsync(EventSchedule schedule,
                                                   CancellationToken cancellationToken = new())
        {
            logger.LogDebug("Entered UpdateEventScheduleAsync");

            try
            {
                IEventScheduleEntity eventScheduleEntity = LoadEventScheduleEntity(schedule);
                await eventDataClient.UpdateEventScheduleAsync(eventScheduleEntity);

                logger.LogDebug("Leaving UpdateEventScheduleAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Exception thrown in UpdateEventScheduleAsync");
            }
        }

        private IEventEntity LoadEventEntity(Event model)
        {
            if (model != null)
            {
                return new EventEntity
                {
                    Id = model.Id,
                    CommunityId = model.CommunityId,
                    Name = model.Name,
                    Description = model.Description,
                    Location = model.Location,
                    Status = (byte)model.Status
                };
            }

            return null;
        }

        private Event LoadEventModel(IEventEntity eventEntity, IList<IEventScheduleEntity> eventScheduleEntities)
        {
            if (eventEntity != null)
            {
                Event @event = new Event
                {
                    Id = eventEntity.Id,
                    CommunityId = eventEntity.CommunityId,
                    Name = eventEntity.Name,
                    Description = eventEntity.Description,
                    Location = eventEntity.Location,
                    Status = (EventStatus)eventEntity.Status,
                    Schedules = new List<EventSchedule>()
                };

                if (eventScheduleEntities.Any(es => es.EventId == @event.Id))
                {
                    IList<EventSchedule> eventSchedules = LoadEventScheduleModels(eventScheduleEntities
                                                                                  .Where(es => es.EventId == @event.Id)
                                                                                  .OrderBy(es => es.Start)
                                                                                  .ToList());
                    foreach (EventSchedule eventSchedule in eventSchedules)
                    {
                        @event.Schedules.Add(eventSchedule);
                    }
                }

                return @event;
            }

            return null;
        }

        private List<Event> LoadEventModels(IList<IEventEntity> eventEntities, IList<IEventScheduleEntity> eventScheduleEntities)
        {
            List<Event> result = new List<Event>();

            foreach(IEventEntity eventEntity in eventEntities)
            {
                Event @event = LoadEventModel(eventEntity, eventScheduleEntities);

                result.Add(@event);
            }

            return result;
        }

        private IEventScheduleEntity LoadEventScheduleEntity(EventSchedule model)
        {
            if (model != null)
            {
                return new EventScheduleEntity
                {
                    Id = model.Id,
                    EventId = model.EventId,
                    Start = model.Start,
                    End = model.End
                };
            }

            return null;
        }

        private List<EventSchedule> LoadEventScheduleModels(IList<IEventScheduleEntity> entities)
        {
            List<EventSchedule> result = new List<EventSchedule>();

            foreach(IEventScheduleEntity entity in entities)
            {
                result.Add(new EventSchedule
                {
                    Id = entity.Id,
                    EventId = entity.EventId,
                    Start = entity.Start,
                    End = entity.End
                });
            }

            return result;
        }

        private readonly IEventDataClient eventDataClient;
        private readonly ILogger<EventStore> logger;
    }
}
