﻿using BrickAtHeart.Communities.Data.Entity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Data
{
    public interface IMembershipDataClient
    {
        Task CreateMembershipAsync(IMembershipEntity membershipEntity, CancellationToken cancellationToken = new());

        Task DeleteMembershipAsync(IMembershipEntity membershipEntity, CancellationToken cancellationToken = new());

        Task<ICommunityEntity> RetrieveCurrentUserGroupAsync(long userId, CancellationToken cancellationToken = new());

        Task<IMembershipEntity> RetrieveMembershipByMembershipIdAsync(long membershipId, CancellationToken cancellationToken = new());

        Task<IList<ICommunityEntity>> RetrieveUserGroupsByUserIdAsync(long userId, CancellationToken cancellationToken = new());

        Task<IList<ICommunityEntity>> RetrieveMembershipsByUserGroupIdAsync(long userGroupId, CancellationToken cancellationToken = new());

        Task<IList<IMembershipEntity>> RetrieveMembershipsByUserIdAsync(long userId, CancellationToken cancellationToken = new());

        Task UpdateMembershipAsync(IMembershipEntity membershipEntity, CancellationToken cancellationToken = new());
    }
}
