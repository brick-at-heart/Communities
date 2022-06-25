using BrickAtHeart.Communities.Data.Entity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Data
{
    public interface ICommunityDataClient
    {
        Task CreateCommunityAsync(ICommunityEntity comunityEntity, CancellationToken cancellationToken = new());

        Task DeleteCommunityAsync(ICommunityEntity communityEntity, CancellationToken cancellationToken = new());

        Task<IList<ICommunityEntity>> RetrieveCommunitiesByUserIdAsync(long userId, CancellationToken cancellationToken = new());

        Task<ICommunityEntity> RetrieveCommunityByCommunityIdAsync(long communityId, CancellationToken cancellationToken = new());

        Task<ICommunityEntity> RetrieveCommunityByFullNameAsync(string normalizedFullName, CancellationToken cancellationToken = new());

        Task<ICommunityEntity> RetrieveCommunityByShortNameAsync(string normalizedShortName, CancellationToken cancellationToken = new());
        
        Task<IList<ICommunityEntity>> RetrieveCommunitiesByJoinTypeAsync(byte joinTypes, CancellationToken cancellationToken = new());

        Task UpdateCommunityAsync(ICommunityEntity communityEntity, CancellationToken cancellationToken = new());
    }
}