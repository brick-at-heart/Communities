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

        public async Task DeleteMembershipAsync(long membershipId, CancellationToken cancellationToken = new ())
        {
            logger.LogInformation("Entered DeleteMembershipAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[DeleteMembership]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter membershipIdParameter = new SqlParameter("@id", SqlDbType.BigInt) { Value = membershipId };
            command.Parameters.Add(membershipIdParameter);
            logger.LogTrace($"Parameter {membershipIdParameter.ParameterName} of type {membershipIdParameter.SqlDbType} has value {membershipIdParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
                logger.LogInformation("Successfully Leaving DeleteMembershipAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in DeleteMembershipAsync");
                throw;
            }
        }

        public async Task<IMembershipEntity> RetrieveMembershipByMembershipIdAsync(long membershipId, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered RetrieveMembershipByMembershipIdAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[RetrieveMembershipByMembershipId]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter membershipIdParameter = new SqlParameter("@membershipId", SqlDbType.BigInt) { Value = membershipId };
            command.Parameters.Add(membershipIdParameter);
            logger.LogTrace($"Parameter {membershipIdParameter.ParameterName} of type {membershipIdParameter.SqlDbType} has value {membershipIdParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
                IMembershipEntity result = await LoadMembershipEntity(reader, cancellationToken);

                return result;
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error in RetrieveMembershipByMembershipIdAsync");

                return null;
            }
        }

        public async Task<IList<IMembershipEntity>> RetrieveMembershipsByCommunityIdAsync(long communityId, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered RetrieveMembershipsByCommunityIdAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[RetrieveMembershipsByCommunityId]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter communityIdParameter = new SqlParameter("@communityId", SqlDbType.BigInt) { Value = communityId };
            command.Parameters.Add(communityIdParameter);
            logger.LogTrace($"Parameter {communityIdParameter.ParameterName} of type {communityIdParameter.SqlDbType} has value {communityIdParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
                List<IMembershipEntity> memberships = await LoadMembershipEntities(reader, cancellationToken);

                logger.LogInformation("Successfully Leaving RetrieveMembershipsByCommunityIdAsync");
                return memberships;
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error in RetrieveMembershipsByCommunityIdAsync");

                return new List<IMembershipEntity>();
            }
        }

        public async Task<IList<IMembershipEntity>> RetrieveMembershipsByUserIdAsync(long userId, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered RetrieveMembershipsByUserIdAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[RetrieveMembershipsByUserId]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter userIdParameter = new SqlParameter("@userId", SqlDbType.BigInt) { Value = userId };
            command.Parameters.Add(userIdParameter);
            logger.LogTrace($"Parameter {userIdParameter.ParameterName} of type {userIdParameter.SqlDbType} has value {userIdParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
                List<IMembershipEntity> result = await LoadMembershipEntities(reader, cancellationToken);

                logger.LogInformation("Successfully Leaving RetrieveMembershipsByUserIdAsync");
                return result;
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error in RetrieveMembershipsByUserIdAsync");

                return new List<IMembershipEntity>();
            }
        }

        public async Task UpdateMembershipAsync(IMembershipEntity membershipEntity, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered UpdateMembershipAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[UpdateMembership]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter membershipIdParameter = new SqlParameter("@id", SqlDbType.BigInt) { Value = membershipEntity.Id };
            command.Parameters.Add(membershipIdParameter);
            logger.LogTrace($"Parameter {membershipIdParameter.ParameterName} of type {membershipIdParameter.SqlDbType} has value {membershipIdParameter.Value}");

            SqlParameter isActiveParameter = new SqlParameter("@isActive", SqlDbType.Bit) { Value = membershipEntity.IsActive };
            command.Parameters.Add(isActiveParameter);
            logger.LogTrace($"Parameter {isActiveParameter.ParameterName} of type {isActiveParameter.SqlDbType} has value {isActiveParameter.Value}");

            SqlParameter isCurrentParameter = new SqlParameter("@isCurrent", SqlDbType.Bit) { Value = membershipEntity.IsCurrent };
            command.Parameters.Add(isCurrentParameter);
            logger.LogTrace($"Parameter {isCurrentParameter.ParameterName} of type {isCurrentParameter.SqlDbType} has value {isCurrentParameter.Value}");

            SqlParameter isPrimaryParameter = new SqlParameter("@isPrimary", SqlDbType.Bit) { Value = membershipEntity.IsPrimary };
            command.Parameters.Add(isPrimaryParameter);
            logger.LogTrace($"Parameter {isPrimaryParameter.ParameterName} of type {isPrimaryParameter.SqlDbType} has value {isPrimaryParameter.Value}");

            SqlParameter slackMemberIdParameter = new SqlParameter("@slackMemberId", SqlDbType.NVarChar, 256)
            {
                Value = (object)membershipEntity.SlackMemberId ?? DBNull.Value
            };
            command.Parameters.Add(slackMemberIdParameter);
            logger.LogTrace($"Parameter {slackMemberIdParameter.ParameterName} of type {slackMemberIdParameter.SqlDbType} has value {slackMemberIdParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
                logger.LogInformation("Successfully Leaving UpdateMembershipAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in UpdateMembershipAsync");
                throw;
            }
        }

        private async Task<List<IMembershipEntity>> LoadMembershipEntities(SqlDataReader reader, CancellationToken cancellationToken = new())
        {
            List<IMembershipEntity> results = new List<IMembershipEntity>();

            while (await reader.ReadAsync(cancellationToken))
            {
                results.Add(
                    new MembershipEntity
                    {
                        Id = reader.GetInt64("Id"),
                        UserId = reader.GetInt64("UserId"),
                        CommunityId = reader.GetInt64("CommunityId"),
                        IsActive = reader.GetBoolean("IsActive"),
                        IsCurrent = reader.GetBoolean("IsCurrent"),
                        IsPrimary = reader.GetBoolean("IsPrimary"),
                        SlackMemberId = await reader.IsDBNullAsync("SlackMemberId", cancellationToken)
                        ? null
                        : reader.GetString("SlackMemberId")
                    }
                );
            }

            return results;
        }

        private async Task<IMembershipEntity> LoadMembershipEntity(SqlDataReader reader, CancellationToken cancellationToken = new())
        {
            IMembershipEntity result = null;

            if (await reader.ReadAsync(cancellationToken))
            {
                result = new MembershipEntity
                {
                    Id = reader.GetInt64("Id"),
                    UserId = reader.GetInt64("UserId"),
                    CommunityId = reader.GetInt64("CommunityId"),
                    IsActive = reader.GetBoolean("IsActive"),
                    IsCurrent = reader.GetBoolean("IsCurrent"),
                    IsPrimary = reader.GetBoolean("IsPrimary"),
                    SlackMemberId = await reader.IsDBNullAsync("SlackMemberId", cancellationToken)
                        ? null
                        : reader.GetString("SlackMemberId")
                };
            }

            return result;
        }
    }
}