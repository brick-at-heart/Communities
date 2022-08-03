using BrickAtHeart.Communities.Data.Entity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Data
{
    public interface IEventDataClient
    {
        Task CreateEventAsync(IEventEntity eventEntity,
                              CancellationToken cancellationToken = new());

        Task CreateEventScheduleAsync(IEventScheduleEntity eventScheduleEntity,
                                      CancellationToken cancellationToken = new());

        Task DeleteEventAsync(long eventId,
                              CancellationToken ctancellationToken = new());

        Task DeleteEventScheduleASync(long eventScheduleId,
                                      CancellationToken cancellationToken = new());

        Task<IList<IEventEntity>> RetrieveEventByCommunityIdAsync(long communityId,
                                                                  CancellationToken cancellationToken = new());

        Task<IEventEntity> RetrieveEventByEventIdAsync(long eventId,
                                                       CancellationToken cancellationToken = new());

        Task<IList<IEventScheduleEntity>> RetrieveEventScheduleByCommunityIdAsync(long communityId,
                                                                                  CancellationToken cancellationToken = new());

        Task<IList<IEventScheduleEntity>> RetrieveEventScheduleByEventIdAsync(long eventId,
                                                                              CancellationToken cancellationToken = new());

        Task UpdateEventAsync(IEventEntity eventEntity,
                              CancellationToken cancellationToken = new());

        Task UpdateEventScheduleAsync(IEventScheduleEntity eventScheduleEntity,
                                      CancellationToken cancellationToken = new());
    }
}
