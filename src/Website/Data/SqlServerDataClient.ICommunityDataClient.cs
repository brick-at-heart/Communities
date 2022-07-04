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
    public partial class SqlServerDataClient : ICommunityDataClient
    {
        public async Task CreateCommunityAsync(ICommunityEntity userGroupEntity,
                                               CancellationToken cancellationToken = new ())
        {
            logger.LogInformation("Entered CreateCommunityAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[CreateCommunity]", conn)
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

            SqlParameter fullNameParameter = new SqlParameter("@fullName", SqlDbType.NVarChar, 256) { Value = userGroupEntity.FullName };
            command.Parameters.Add(fullNameParameter);
            logger.LogTrace($"Parameter {fullNameParameter.ParameterName} of type {fullNameParameter.SqlDbType} has value {fullNameParameter.Value}");

            SqlParameter joinTypeParameter = new SqlParameter("@joinType", SqlDbType.TinyInt) { Value = userGroupEntity.JoinType };
            command.Parameters.Add(joinTypeParameter);
            logger.LogTrace($"Parameter {joinTypeParameter.ParameterName} of type {joinTypeParameter.SqlDbType} has value {joinTypeParameter.Value}");

            SqlParameter normalizedFullNameParameter = new SqlParameter("@normalizedFullName", SqlDbType.NVarChar, 256) { Value = userGroupEntity.NormalizedFullName };
            command.Parameters.Add(normalizedFullNameParameter);
            logger.LogTrace($"Parameter {normalizedFullNameParameter.ParameterName} of type {normalizedFullNameParameter.SqlDbType} has value {normalizedFullNameParameter.Value}");

            SqlParameter shortNameParameter = new SqlParameter("@shortName", SqlDbType.NVarChar, 64) { Value = userGroupEntity.ShortName };
            command.Parameters.Add(shortNameParameter);
            logger.LogTrace($"Parameter {shortNameParameter.ParameterName} of type {shortNameParameter.SqlDbType} has value {shortNameParameter.Value}");

            SqlParameter normalizedShortNameParameter = new SqlParameter("@normalizedShortName", SqlDbType.NVarChar, 64) { Value = userGroupEntity.NormalizedShortName };
            command.Parameters.Add(normalizedShortNameParameter);
            logger.LogTrace($"Parameter {normalizedShortNameParameter.ParameterName} of type {normalizedShortNameParameter.SqlDbType} has value {normalizedShortNameParameter.Value}");

            SqlParameter slackWorkspaceIdParameter = new SqlParameter("@slackWorkspaceId", SqlDbType.NVarChar, 256) { Value = string.IsNullOrWhiteSpace(userGroupEntity.SlackWorkspaceId) ? DBNull.Value : userGroupEntity.SlackWorkspaceId };
            command.Parameters.Add(slackWorkspaceIdParameter);
            logger.LogTrace($"Parameter {slackWorkspaceIdParameter.ParameterName} of type {slackWorkspaceIdParameter.SqlDbType} has value {slackWorkspaceIdParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
                userGroupEntity.Id = (long)idParameter.Value;
                logger.LogInformation("Successfully Leaving CreateCommunityAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in CreateCommunityAsync");
                throw;
            }
        }

        public async Task DeleteCommunityAsync(ICommunityEntity communityEntity,
                                               CancellationToken cancellationToken = new ())
        {
            logger.LogInformation("Entered DeleteCommunityAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[DeleteCommunity]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter userGroupIdParameter = new SqlParameter("@id", SqlDbType.BigInt) { Value = communityEntity.Id };
            command.Parameters.Add(userGroupIdParameter);
            logger.LogTrace($"Parameter {userGroupIdParameter.ParameterName} of type {userGroupIdParameter.SqlDbType} has value {userGroupIdParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
                logger.LogInformation("Successfully Leaving DeleteCommunityAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in DeleteCommunityAsync");
                throw;
            }
        }

        public async Task<ICommunityEntity> RetrieveCommunityByFullNameAsync(string normalizedFullName,
                                                                             CancellationToken cancellationToken = new ())
        {
            logger.LogInformation("Entered RetrieveCommunityByFullNameAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[RetrieveCommunityByFullName]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter normalizedFullNameParameter = new SqlParameter("@normalizedFullName", SqlDbType.NVarChar, 256) { Value = normalizedFullName };
            command.Parameters.Add(normalizedFullNameParameter);
            logger.LogTrace($"Parameter {normalizedFullNameParameter.ParameterName} of type {normalizedFullNameParameter.SqlDbType} has value {normalizedFullNameParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
                ICommunityEntity communityEntity = await LoadCommunityEntity(reader, cancellationToken);

                logger.LogInformation("Successfully Leaving RetrieveCommunityByFullNameAsync");

                return communityEntity;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in RetrieveCommunityByFullNameAsync");
                throw;
            }
        }

        public async Task<ICommunityEntity> RetrieveCommunityByShortNameAsync(string normalizedShortName,
                                                                              CancellationToken cancellationToken = new ())
        {
            logger.LogInformation("Entered RetrieveCommunityByShortNameAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[RetrieveCommunityByShortName]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter normalizedShortNameParameter = new SqlParameter("@normalizedShortName", SqlDbType.NVarChar, 64) { Value = normalizedShortName };
            command.Parameters.Add(normalizedShortNameParameter);
            logger.LogTrace($"Parameter {normalizedShortNameParameter.ParameterName} of type {normalizedShortNameParameter.SqlDbType} has value {normalizedShortNameParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
                ICommunityEntity communityEntity = await LoadCommunityEntity(reader, cancellationToken);

                logger.LogInformation("Successfully Leaving RetrieveCommunityByShortNameAsync");

                return communityEntity;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in RetrieveCommunityByShortNameAsync");
                throw;
            }
        }

        public async Task<ICommunityEntity> RetrieveCommunityByCommunityIdAsync(long communityId,
                                                                                CancellationToken cancellationToken = new ())
        {
            logger.LogInformation("Entered RetrieveCommunityByCommunityIdAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[RetrieveCommunityByCommunityId]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter idParameter = new SqlParameter("@id", SqlDbType.BigInt) { Value = communityId };
            command.Parameters.Add(idParameter);
            logger.LogTrace($"Parameter {idParameter.ParameterName} of type {idParameter.SqlDbType} has value {idParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
                ICommunityEntity communityEntity = await LoadCommunityEntity(reader, cancellationToken);

                logger.LogInformation("Successfully Leaving RetrieveCommunityByCommunityIdAsync");

                return communityEntity;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in RetrieveCommunityByCommunityIdAsync");
                throw;
            }
        }

        public async Task<IList<ICommunityEntity>> RetrieveCommunitiesByJoinTypeAsync(byte joinTypes,
                                                                                      CancellationToken cancellationToken = new ())
        {
            logger.LogInformation("Entered RetrieveCommunitiesByJoinTypeAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[RetrieveCommunitiesByJoinType]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter joinTypeParameter = new SqlParameter("@joinType", SqlDbType.TinyInt) { Value = joinTypes };
            command.Parameters.Add(joinTypeParameter);
            logger.LogTrace($"Parameter {joinTypeParameter.ParameterName} of type {joinTypeParameter.SqlDbType} has value {joinTypeParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
                IList<ICommunityEntity> communityEntities = await LoadCommunityEntities(reader, cancellationToken);

                logger.LogInformation("Successfully Leaving RetrieveCommunitiesByJoinTypeAsync");

                return communityEntities;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in RetrieveCommunitiesByJoinTypeAsync");
                throw;
            }
        }

        public async Task<IList<ICommunityEntity>> RetrieveCommunitiesByUserIdAsync(long userId,
                                                                                    CancellationToken cancellationToken = new ())
        {
            logger.LogInformation("Entered RetrieveCommunitiesByUserIdAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[RetrieveCommunitiesByUserId]", conn)
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
                List<ICommunityEntity> communities = await LoadCommunityEntities(reader, cancellationToken);

                logger.LogInformation("Successfully Leaving RetrieveCommunitiesByUserIdAsync");
                return communities;
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error in RetrieveCommunitiesByUserIdAsync");

                return null;
            }
        }

        public async Task UpdateCommunityAsync(ICommunityEntity communityEntity,
                                               CancellationToken cancellationToken = new ())
        {
            logger.LogInformation("Entered UpdateCommunityAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[UpdateCommunity]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter idParameter = new SqlParameter("@id", SqlDbType.BigInt) { Value = communityEntity.Id };
            command.Parameters.Add(idParameter);
            logger.LogTrace($"Parameter {idParameter.ParameterName} of type {idParameter.SqlDbType} has value {idParameter.Value}");

            SqlParameter fullNameParameter = new SqlParameter("@fullName", SqlDbType.NVarChar, 256) { Value = communityEntity.FullName };
            command.Parameters.Add(fullNameParameter);
            logger.LogTrace($"Parameter {fullNameParameter.ParameterName} of type {fullNameParameter.SqlDbType} has value {fullNameParameter.Value}");

            SqlParameter joinTypeParameter = new SqlParameter("@joinType", SqlDbType.TinyInt) { Value = communityEntity.JoinType };
            command.Parameters.Add(joinTypeParameter);
            logger.LogTrace($"Parameter {joinTypeParameter.ParameterName} of type {joinTypeParameter.SqlDbType} has value {joinTypeParameter.Value}");

            SqlParameter normalizedFullNameParameter = new SqlParameter("@normalizedFullName", SqlDbType.NVarChar, 256) { Value = communityEntity.NormalizedFullName };
            command.Parameters.Add(normalizedFullNameParameter);
            logger.LogTrace($"Parameter {normalizedFullNameParameter.ParameterName} of type {normalizedFullNameParameter.SqlDbType} has value {normalizedFullNameParameter.Value}");

            SqlParameter shortNameParameter = new SqlParameter("@shortName", SqlDbType.NVarChar, 64) { Value = communityEntity.ShortName };
            command.Parameters.Add(shortNameParameter);
            logger.LogTrace($"Parameter {shortNameParameter.ParameterName} of type {shortNameParameter.SqlDbType} has value {shortNameParameter.Value}");

            SqlParameter normalizedShortNameParameter = new SqlParameter("@normalizedShortName", SqlDbType.NVarChar, 64) { Value = communityEntity.NormalizedShortName };
            command.Parameters.Add(normalizedShortNameParameter);
            logger.LogTrace($"Parameter {normalizedShortNameParameter.ParameterName} of type {normalizedShortNameParameter.SqlDbType} has value {normalizedShortNameParameter.Value}");

            SqlParameter slackWorkspaceIdParameter = new SqlParameter("@slackWorkspaceId", SqlDbType.NVarChar, 256) { Value = string.IsNullOrWhiteSpace(communityEntity.SlackWorkspaceId) ? DBNull.Value : communityEntity.SlackWorkspaceId };
            command.Parameters.Add(slackWorkspaceIdParameter);
            logger.LogTrace($"Parameter {slackWorkspaceIdParameter.ParameterName} of type {slackWorkspaceIdParameter.SqlDbType} has value {slackWorkspaceIdParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);

                logger.LogInformation("Successfully Leaving UpdateCommunityAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in UpdateCommunityAsync");
                throw;
            }
        }

        private async Task<ICommunityEntity> LoadCommunityEntity(SqlDataReader reader,
                                                                 CancellationToken cancellationToken = new ())
        {
            if (await reader.ReadAsync(cancellationToken))
            {
                return new CommunityEntity
                {
                    Id = reader.GetInt64("Id"),
                    FullName = reader.GetString("FullName"),
                    JoinType = reader.GetByte("JoinType"),
                    NormalizedFullName = reader.GetString("NormalizedFullName"),
                    ShortName = reader.GetString("ShortName"),
                    NormalizedShortName = reader.GetString("NormalizedShortName"),
                    SlackWorkspaceId = await reader.IsDBNullAsync("SlackWorkspaceId", cancellationToken)
                        ? null
                        : reader.GetString("SlackWorkspaceId")
                };
            }

            return null;
        }

        private async Task<List<ICommunityEntity>> LoadCommunityEntities(SqlDataReader reader,
                                                                         CancellationToken cancellationToken = new ())
        {
            List<ICommunityEntity> result = new List<ICommunityEntity>();

            while (await reader.ReadAsync(cancellationToken))
            {
                result.Add(new CommunityEntity
                {
                    Id = reader.GetInt64("Id"),
                    FullName = reader.GetString("FullName"),
                    JoinType = reader.GetByte("JoinType"),
                    NormalizedFullName = reader.GetString("NormalizedFullName"),
                    ShortName = reader.GetString("ShortName"),
                    NormalizedShortName = reader.GetString("NormalizedShortName"),
                    SlackWorkspaceId = await reader.IsDBNullAsync("SlackWorkspaceId", cancellationToken)
                        ? null
                        : reader.GetString("SlackWorkspaceId")
                });
            }

            return result;
        }
    }
}
