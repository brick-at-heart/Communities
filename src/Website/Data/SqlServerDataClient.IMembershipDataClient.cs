using BrickAtHeart.Communities.Data.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Data
{
    public partial class SqlServerDataClient : IMembershipDataClient
    {
        public async Task CreateMembershipAsync(IMembershipEntity membershipEntity, CancellationToken cancellationToken = new ())
        {
            logger.LogInformation("Entered CreateMembershipAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[CreateMembership]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter idParameter = new SqlParameter("@id", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(idParameter);
            logger.LogTrace($"Parameter {idParameter.ParameterName} of type {idParameter.SqlDbType} with direction {idParameter.Direction}");

            SqlParameter userIdParameter = new SqlParameter("@userId", SqlDbType.BigInt) { Value = membershipEntity.UserId };
            command.Parameters.Add(userIdParameter);
            logger.LogTrace($"Parameter {userIdParameter.ParameterName} of type {userIdParameter.SqlDbType} has value {userIdParameter.Value}");

            SqlParameter communityIdParameter = new SqlParameter("@communityId", SqlDbType.BigInt) { Value = membershipEntity.CommunityId };
            command.Parameters.Add(communityIdParameter);
            logger.LogTrace($"Parameter {communityIdParameter.ParameterName} of type {communityIdParameter.SqlDbType} has value {communityIdParameter.Value}");

            SqlParameter isActiveParameter = new SqlParameter("@isActive", SqlDbType.Bit) { Value = membershipEntity.IsActive };
            command.Parameters.Add(isActiveParameter);
            logger.LogTrace($"Parameter {isActiveParameter.ParameterName} of type {isActiveParameter.SqlDbType} has value {isActiveParameter.Value}");

            SqlParameter slackMembershipIdParameter = new SqlParameter("@slackMemberId", SqlDbType.NVarChar, 256) { Value = string.IsNullOrWhiteSpace(membershipEntity.SlackMemberId) ? DBNull.Value : membershipEntity.SlackMemberId };
            command.Parameters.Add(slackMembershipIdParameter);
            logger.LogTrace($"Parameter {slackMembershipIdParameter.ParameterName} of type {slackMembershipIdParameter.SqlDbType} has value {slackMembershipIdParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
                membershipEntity.Id = (long)idParameter.Value;
                logger.LogInformation("Successfully Leaving CreateMembershipAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in CreateMembershipAsync");

            }
        }

            public Task DeleteMembershipAsync(IMembershipEntity membershipEntity, CancellationToken cancellationToken = new ())
        {
            throw new System.NotImplementedException();
        }

        public Task<IMembershipEntity> RetrieveMembershipByMembershipIdAsync(long membershipId, CancellationToken cancellationToken = new())
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<IMembershipEntity>> RetrieveMembershipsByCommunityIdAsync(long communityId, CancellationToken cancellationToken = new())
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<IMembershipEntity>> RetrieveMembershipsByUserIdAsync(long userId, CancellationToken cancellationToken = new())
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateMembershipAsync(IMembershipEntity membershipEntity, CancellationToken cancellationToken = new())
        {
            throw new System.NotImplementedException();
        }
    }
}