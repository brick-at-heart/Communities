using BrickAtHeart.Communities.Data;
using BrickAtHeart.Communities.Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Models
{
    public class CommunityStore
    {
        public CommunityStore(ICommunityDataClient communityDataClient,
                              ILookupNormalizer normalizer,
                              ILogger<CommunityStore> logger)
        {
            this.communityDataClient = communityDataClient;
            this.normalizer = normalizer;
            this.logger = logger;
        }

        public async Task CreateCommunityAsync(Community community, CancellationToken cancellationToken = new ())
        {
            logger.LogInformation("Entered CreateCommunityAsync");

            ICommunityEntity communityEntity = LoadEntity(community);

            if (communityEntity != null)
            {
                await communityDataClient.CreateCommunityAsync(communityEntity, cancellationToken);
                community.Id = communityEntity.Id;
                logger.LogInformation("Successfully Leaving CreateAsync");
            }
            else
            {
                logger.LogWarning("Community entity was found to be null during Community creation.");
            }
        }

        public async Task<Community> RetrieveCommunityByCommunityIdAsync(long communityId, CancellationToken cancellationToken = new())
        {
            ICommunityEntity entity = await communityDataClient.RetrieveCommunityByCommunityIdAsync(communityId, cancellationToken);

            return LoadModel(entity);
        }

        public async Task<Community> RetrieveCommunityByFullNameAsync(string fullName, CancellationToken cancellationToken = new ())
        {
            fullName = normalizer.NormalizeName(fullName);

            ICommunityEntity entity = await communityDataClient.RetrieveCommunityByFullNameAsync(fullName, cancellationToken);

            return LoadModel(entity);
        }

        public async Task<IList<Community>> RetrieveCommunitiesByJoinTypesAsync(byte joinTypes, CancellationToken cancellationToken = new ())
        {
            IList<ICommunityEntity> communities =  await communityDataClient.RetrieveCommunitiesByJoinTypeAsync(joinTypes, cancellationToken);

            return LoadModels(communities);
        }

        public async Task<Community> RetrieveCommunityByShortNameAsync(string shortName, CancellationToken cancellationToken = new CancellationToken())
        {
            shortName = normalizer.NormalizeName(shortName);

            ICommunityEntity entity = await communityDataClient.RetrieveCommunityByShortNameAsync(shortName, cancellationToken);

            return LoadModel(entity);
        }

        public async Task<IList<Community>> RetrieveCommunitiesByUserIdAsync(long userId, CancellationToken cancellationToken = new ())
        {
            IList<ICommunityEntity> communities = await communityDataClient.RetrieveCommunitiesByUserIdAsync(userId, cancellationToken);

            return LoadModels(communities);
        }

        public async Task UpdateCommunityAsync(Community community, CancellationToken cancellationToken = new ())
        {
            await communityDataClient.UpdateCommunityAsync(LoadEntity(community), cancellationToken);
        }

        private ICommunityEntity LoadEntity(Community community)
        {
            if (community != null)
            {
                return new CommunityEntity
                {
                    Id = community.Id,
                    FullName = community.FullName,
                    JoinType = (byte)community.JoinType,
                    NormalizedFullName = normalizer.NormalizeName(community.FullName),
                    NormalizedShortName = normalizer.NormalizeName(community.ShortName),
                    ShortName = community.ShortName,
                    SlackWorkspaceId = community.SlackWorkspaceId
                };
            }

            return null;
        }

        private Community LoadModel(ICommunityEntity entity)
        {
            if (entity != null)
            {
                return new Community
                {
                    Id = entity.Id,
                    FullName = entity.FullName,
                    JoinType = (CommunityJoinType)entity.JoinType,
                    NormalizedFullName = entity.NormalizedFullName,
                    NormalizedShortName = entity.NormalizedShortName,
                    ShortName = entity.ShortName,
                    SlackWorkspaceId = entity.SlackWorkspaceId
                };
            }

            return null;
        }

        private IList<Community> LoadModels(IList<ICommunityEntity> entities)
        {
            IList<Community> list = new List<Community>();

            foreach(ICommunityEntity entity in entities)
            {
                list.Add(LoadModel(entity));
            }

            return list;
        }

        private readonly ICommunityDataClient communityDataClient;
        private readonly ILookupNormalizer normalizer;
        private readonly ILogger<CommunityStore> logger;
    }
}