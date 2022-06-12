using BrickAtHeart.Communities.Data.Entity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Data
{
    public partial class SqlServerDataClient : IMembershipDataClient
    {
        public Task CreateMembershipAsync(IMembershipEntity membershipEntity, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteMembershipAsync(IMembershipEntity membershipEntity, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICommunityEntity> RetrieveCurrentUserGroupAsync(long userId, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IMembershipEntity> RetrieveMembershipByMembershipIdAsync(long membershipId, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<ICommunityEntity>> RetrieveMembershipsByUserGroupIdAsync(long userGroupId, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<IMembershipEntity>> RetrieveMembershipsByUserIdAsync(long userId, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<ICommunityEntity>> RetrieveUserGroupsByUserIdAsync(long userId, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateMembershipAsync(IMembershipEntity membershipEntity, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}