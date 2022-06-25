using BrickAtHeart.Communities.Data;
using BrickAtHeart.Communities.Data.Entity;
using BrickAtHeart.Communities.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Models
{
    public class MembershipStore
    {
        public MembershipStore(IMembershipDataClient membershipDataClient,
                               ILogger<MembershipStore> logger)
        {
            this.membershipDataClient = membershipDataClient;
            this.logger = logger;
        }

        public async Task CreateMembershipAsync(Membership membership, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered CreateMembershipAsync");

            IMembershipEntity membershipEntity = LoadEntity(membership);

            try
            {
                await membershipDataClient.CreateMembershipAsync(membershipEntity, cancellationToken);
                membership.Id = membershipEntity.Id;

                logger.LogInformation("Successfully Leaving CreateMembershipAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, $"Error in CreateMembershipAsync {e.HResult}, {e.Message}");
                throw;
            }
        }

        public async Task DeleteMembershipAsync(Membership membership, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered DeleteMembershipAsync");

            IMembershipEntity membershipEntity = LoadEntity(membership);

            try
            {
                await membershipDataClient.DeleteMembershipAsync(membershipEntity, cancellationToken);

                logger.LogInformation("Successfully Leaving DeleteMembershipAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, $"Error in DeleteMembershipAsync {e.HResult}, {e.Message}");
            }
        }

        public async Task<Membership> RetrieveMembershipByMembershipIdAsync(long membershipId, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered RetrieveMembershipByMembershipIdAsync");

            try
            {
                IMembershipEntity membershipEntity = await membershipDataClient.RetrieveMembershipByMembershipIdAsync(membershipId, cancellationToken);
                Membership membership = LoadModel(membershipEntity);

                logger.LogInformation("Successfully Leaving RetrieveMembershipByMembershipIdAsync");

                return membership;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, $"Error in RetrieveMembershipByMembershipIdAsync {e.HResult}, {e.Message}");
                throw;
            }
        }

        public async Task<IList<Membership>> RetrieveMembershipsByCommunityIdAsync(long communityId, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered RetrieveMembershipsByUserGroupIdAsync");

            try
            {
                IList<IMembershipEntity> membershipEntities = await membershipDataClient.RetrieveMembershipsByCommunityIdAsync(communityId, cancellationToken);
                IList<Membership> memberships = LoadModels(membershipEntities);

                logger.LogInformation("Successfully Leaving RetrieveMembershipsByUserGroupIdAsync");

                return memberships;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, $"Error in RetrieveMembershipsByUserGroupIdAsync {e.HResult}, {e.Message}");
                throw;
            }
        }

        public async Task<IList<Membership>> RetrieveMembershipsByUserIdAsync(long userId, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered RetrieveMembershipsByUserIdAsync");

            try
            {
                IList<IMembershipEntity> membershipEntities = await membershipDataClient.RetrieveMembershipsByUserIdAsync(userId, cancellationToken);
                IList<Membership> memberships = LoadModels(membershipEntities);

                logger.LogInformation("Successfully Leaving RetrieveMembershipsByUserIdAsync");

                return memberships;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, $"Error in RetrieveMembershipsByUserIdAsync {e.HResult}, {e.Message}");
                throw;
            }
        }

        public async Task UpdateMembershipAsync(Membership membership, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered CreateMembershipAsync");

            IMembershipEntity membershipEntity = LoadEntity(membership);

            try
            {
                await membershipDataClient.UpdateMembershipAsync(membershipEntity, cancellationToken);

                logger.LogInformation("Successfully Leaving UpdateMembershipAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, $"Error in UpdateMembershipAsync {e.HResult}, {e.Message}");
            }
        }

        private IMembershipEntity LoadEntity(Membership membership)
        {
            return new MembershipEntity
            {
                Id = membership.Id,
                IsActive = membership.IsActive,
                IsCurrent = membership.IsCurrent,
                IsPrimary = membership.IsPrimary,
                CommunityId = membership.CommunityId,
                SlackMemberId = membership.SlackMemberId,
                UserId = membership.UserId
            };
        }

        private Membership LoadModel(IMembershipEntity entity)
        {
            Membership result = new Membership
            {
                Id = entity.Id,
                IsActive = entity.IsActive,
                IsCurrent = entity.IsCurrent,
                IsPrimary = entity.IsPrimary,
                CommunityId = entity.CommunityId,
                SlackMemberId = entity.SlackMemberId,
                UserId = entity.UserId
            };

            return result;
        }

        private IList<Membership> LoadModels(IList<IMembershipEntity> membershipEntities)
        {
            IList<Membership> result = new List<Membership>();

            foreach (IMembershipEntity entity in membershipEntities)
            {
                result.Add(new Membership
                {
                    Id = entity.Id,
                    IsActive = entity.IsActive,
                    IsCurrent = entity.IsCurrent,
                    IsPrimary = entity.IsPrimary,
                    CommunityId = entity.CommunityId,
                    SlackMemberId = entity.SlackMemberId,
                    UserId = entity.UserId
                });
            }

            return result;
        }

        private IMembershipDataClient membershipDataClient;
        private ILogger<MembershipStore> logger;
    }
}