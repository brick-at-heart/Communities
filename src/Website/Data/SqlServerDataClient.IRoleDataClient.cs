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
    public partial class SqlServerDataClient : IRoleDataClient
    {
        public async Task CreateMembershipRoleAsync(IMembershipRoleEntity membershipRoleEntity,
                                                    CancellationToken cancellationToken = new ())
        {
            logger.LogInformation("Entered CreateMembershipRoleAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[CreateMembershipRole]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter membershipRoleIdParameter = new SqlParameter("@id", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(membershipRoleIdParameter);
            logger.LogTrace($"Parameter {membershipRoleIdParameter.ParameterName} of type {membershipRoleIdParameter.SqlDbType} with direction {membershipRoleIdParameter.Direction}");

            SqlParameter membershipIdParameter = new SqlParameter("@membershipId", SqlDbType.BigInt) { Value = membershipRoleEntity.MembershipId };
            command.Parameters.Add(membershipIdParameter);
            logger.LogTrace($"Parameter {membershipIdParameter.ParameterName} of type {membershipIdParameter.SqlDbType} has value {membershipIdParameter.Value}");

            SqlParameter roleIdParameter = new SqlParameter("@roleId", SqlDbType.BigInt) { Value = membershipRoleEntity.RoleId };
            command.Parameters.Add(roleIdParameter);
            logger.LogTrace($"Parameter {roleIdParameter.ParameterName} of type {roleIdParameter.SqlDbType} has value {roleIdParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
                membershipRoleEntity.Id = (long)membershipRoleIdParameter.Value;
                logger.LogInformation("Successfully Leaving CreateMembershipRoleAsync");
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error in CreateMembershipRoleAsync");
                throw;
            }
        }

        public async Task CreateRoleAsync(IRoleEntity roleEntity,
                                          CancellationToken cancellationToken = new ())
        {
            logger.LogInformation("Entered CreateRoleAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[CreateRole]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter roleIdParameter = new SqlParameter("@id", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(roleIdParameter);
            logger.LogTrace($"Parameter {roleIdParameter.ParameterName} of type {roleIdParameter.SqlDbType} with direction {roleIdParameter.Direction}");

            SqlParameter nameParameter = new SqlParameter("@roleName", SqlDbType.NVarChar, 256) { Value = roleEntity.Name };
            command.Parameters.Add(nameParameter);
            logger.LogTrace($"Parameter {nameParameter.ParameterName} of type {nameParameter.SqlDbType} has value {nameParameter.Value}");

            SqlParameter normalizedNameParameter = new SqlParameter("@normalizedRoleName", SqlDbType.NVarChar, 256) { Value = roleEntity.NormalizedName };
            command.Parameters.Add(normalizedNameParameter);
            logger.LogTrace($"Parameter {normalizedNameParameter.ParameterName} of type {normalizedNameParameter.SqlDbType} has value {normalizedNameParameter.Value}");

            SqlParameter communityIdParameter = new SqlParameter("@communityId", SqlDbType.BigInt) { Value = roleEntity.CommunityId };
            command.Parameters.Add(communityIdParameter);
            logger.LogTrace($"Parameter {communityIdParameter.ParameterName} of type {communityIdParameter.SqlDbType} has value {communityIdParameter.Value}");

            SqlParameter isDefaultParameter = new SqlParameter("@isCommunityDefault", SqlDbType.Bit) { Value = roleEntity.IsCommunityDefault };
            command.Parameters.Add(isDefaultParameter);
            logger.LogTrace($"Parameter {isDefaultParameter.ParameterName} of type {isDefaultParameter.SqlDbType} has value {isDefaultParameter.Value}");

            SqlParameter isOwnerParameter = new SqlParameter("@isSystemGeneratedOwner", SqlDbType.Bit) {  Value = roleEntity.IsSystemGeneratedOwner};
            command.Parameters.Add(isOwnerParameter);
            logger.LogTrace($"Parameter {isOwnerParameter.ParameterName} of type {isOwnerParameter.SqlDbType} has value {isOwnerParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
                roleEntity.Id = (long)roleIdParameter.Value;
                logger.LogInformation("Successfully Leaving CreateRoleAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in CreateRoleAsync");
                throw;
            }
        }

        public async Task DeleteMembershipRoleAsync(IMembershipRoleEntity membershipRoleEntity,
                                                    CancellationToken cancellationToken = new ())
        {
            logger.LogInformation("Entered DeleteMembershipRoleAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[DeleteMembershipRole]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter membershipRoleIdParameter = new SqlParameter("@membershipRoleId", SqlDbType.BigInt) { Value = membershipRoleEntity.Id };
            command.Parameters.Add(membershipRoleIdParameter);
            logger.LogTrace($"Parameter {membershipRoleIdParameter.ParameterName} of type {membershipRoleIdParameter.SqlDbType} has value {membershipRoleIdParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
                logger.LogInformation("Successfully Leaving DeleteMembershipRoleAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in DeleteMembershipRoleAsync");
                throw;
            }
        }

        public async Task DeleteRoleAsync(IRoleEntity roleEntity,
                                          CancellationToken cancellationToken = new ())
        {
            logger.LogInformation("Entered DeleteRoleAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[DeleteRole]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter roleIdParameter = new SqlParameter("@id", SqlDbType.BigInt) { Value = roleEntity.Id };
            command.Parameters.Add(roleIdParameter);
            logger.LogTrace($"Parameter {roleIdParameter.ParameterName} of type {roleIdParameter.SqlDbType} has value {roleIdParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
                logger.LogInformation("Successfully Leaving DeleteRoleAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in DeleteRoleAsync");
                throw;
            }
        }

        public async Task<IList<IMembershipRoleEntity>> RetrieveMembershipRolesByRoleIdAsync(long roleId,
                                                                                             CancellationToken cancellationToken = new ())
        {
            logger.LogInformation("Entered RetrieveMembershipRolesByRoleIdAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[RetrieveMembershipRolesByRoleId]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter roleIdParameter = new SqlParameter("@roleId", SqlDbType.BigInt) { Value = roleId };
            command.Parameters.Add(roleIdParameter);
            logger.LogTrace($"Parameter {roleIdParameter.ParameterName} of type {roleIdParameter.SqlDbType} has value {roleIdParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
                IList<IMembershipRoleEntity> result = await LoadMembershipRoleEntities(reader, cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in RetrieveMembershipRolesByRoleIdAsync");
                IList<IMembershipRoleEntity> result = new List<IMembershipRoleEntity>();

                return result;
            }
        }

        public async Task<IList<IRightEntity>> RetrieveRightByRightIdUserIdAsync(long rightId,
                                                                                 long userId,
                                                                                 CancellationToken cancellationToken = new ())
        {
            logger.LogInformation("Entered RetrieveRightByRightIdUserIdAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[RetrieveRightByRightIdUserId]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter rightIdParameter = new SqlParameter("@rightId", SqlDbType.BigInt) { Value = rightId };
            command.Parameters.Add(rightIdParameter);
            logger.LogTrace($"Parameter {rightIdParameter.ParameterName} of type {rightIdParameter.SqlDbType} has value {rightIdParameter.Value}");

            SqlParameter userIdParameter = new SqlParameter("@userId", SqlDbType.BigInt) { Value = userId };
            command.Parameters.Add(userIdParameter);
            logger.LogTrace($"Parameter {userIdParameter.ParameterName} of type {userIdParameter.SqlDbType} has value {userIdParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
                IList<IRightEntity> result = await LoadRightEntities(reader, cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in RetrieveRightByRightIdUserIdAsync");
                IList<IRightEntity> result = new List<IRightEntity>();

                return result;
            }
        }

        public async Task<IList<IRightEntity>> RetrieveRightsByRoleIdAsync(long roleId,
                                                                           CancellationToken cancellationToken = new ())
        {
            logger.LogInformation("Entered RetrieveRightsByRoleIdAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[RetrieveRightsByRoleId]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter roleIdParameter = new SqlParameter("@roleId", SqlDbType.BigInt) { Value = roleId };
            command.Parameters.Add(roleIdParameter);
            logger.LogTrace($"Parameter {roleIdParameter.ParameterName} of type {roleIdParameter.SqlDbType} has value {roleIdParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
                IList<IRightEntity> result = await LoadRightEntities(reader, cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in RetrieveRightsByRoleIdAsync");
                IList<IRightEntity> result = new List<IRightEntity>();

                return result;
            }
        }

        public async Task<IRoleEntity> RetrieveRoleByRoleIdAsync(long roleId,
                                                                 CancellationToken cancellationToken = new ())
        {
            logger.LogInformation("Entered RetrieveRoleByRoleIdAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[RetrieveRoleByRoleId]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter roleIdParameter = new SqlParameter("@roleId", SqlDbType.BigInt) { Value = roleId };
            command.Parameters.Add(roleIdParameter);
            logger.LogTrace($"Parameter {roleIdParameter.ParameterName} of type {roleIdParameter.SqlDbType} has value {roleIdParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
                IRoleEntity result = await LoadRoleEntity(reader, cancellationToken);

                return result;
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error in RetrieveRoleByRoleIdAsync");

                return null;
            }
        }

        public async Task<IList<IRoleEntity>> RetrieveRolesByCommunityIdAsync(long communityId,
                                                                              CancellationToken cancellationToken = new ())
        {
            logger.LogInformation("Entered RetrieveRolesByCommunityIdAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[RetrieveRolesByCommunityId]", conn)
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
                IList<IRoleEntity> result = await LoadRoleEntities(reader, cancellationToken);

                return result;
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error in RetrieveRolesByCommunityIdAsync");
                IList<IRoleEntity> result = new List<IRoleEntity>();

                return result;
            }
        }

        public async Task UpdateRoleAsync(IRoleEntity roleEntity,
                                          CancellationToken cancellationToken = new ())
        {
            logger.LogInformation("Entered UpdateRoleAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[UpdateRole]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter roleIdParameter = new SqlParameter("@id", SqlDbType.BigInt) { Value = roleEntity.Id };
            command.Parameters.Add(roleIdParameter);
            logger.LogTrace($"Parameter {roleIdParameter.ParameterName} of type {roleIdParameter.SqlDbType} has value {roleIdParameter.Value}");

            SqlParameter nameParameter = new SqlParameter("@roleName", SqlDbType.NVarChar, 256) { Value = roleEntity.Name };
            command.Parameters.Add(nameParameter);
            logger.LogTrace($"Parameter {nameParameter.ParameterName} of type {nameParameter.SqlDbType} has value {nameParameter.Value}");

            SqlParameter normalizedNameParameter = new SqlParameter("@normalizedRoleName", SqlDbType.NVarChar, 256) { Value = roleEntity.NormalizedName };
            command.Parameters.Add(normalizedNameParameter);
            logger.LogTrace($"Parameter {normalizedNameParameter.ParameterName} of type {normalizedNameParameter.SqlDbType} has value {normalizedNameParameter.Value}");

            SqlParameter communityIdParameter = new SqlParameter("@communityId", SqlDbType.BigInt) { Value = roleEntity.CommunityId };
            command.Parameters.Add(communityIdParameter);
            logger.LogTrace($"Parameter {communityIdParameter.ParameterName} of type {communityIdParameter.SqlDbType} has value {communityIdParameter.Value}");

            SqlParameter isDefaultParameter = new SqlParameter("@isDefault", SqlDbType.Bit) { Value = roleEntity.IsCommunityDefault };
            command.Parameters.Add(isDefaultParameter);
            logger.LogTrace($"Parameter {isDefaultParameter.ParameterName} of type {isDefaultParameter.SqlDbType} has value {isDefaultParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
                logger.LogInformation("Successfully Leaving UpdateRoleAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in UpdateRoleAsync");
                throw;
            }
        }

        public async Task UpdateRoleRightAsync(IRightEntity rightEntity,
                                               CancellationToken cancellationToken = new ())
        {
            logger.LogInformation("Entered UpdateRoleRightAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[UpdateRoleRight]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter roleRightIdParameter = new SqlParameter("@roleRightId", SqlDbType.BigInt) { Value = rightEntity.Id };
            command.Parameters.Add(roleRightIdParameter);
            logger.LogTrace($"Parameter {roleRightIdParameter.ParameterName} of type {roleRightIdParameter.SqlDbType} has value {roleRightIdParameter.Value}");

            SqlParameter stateParameter = new SqlParameter("@state", SqlDbType.Bit) { Value = rightEntity.State == null ? DBNull.Value : rightEntity.State };
            command.Parameters.Add(stateParameter);
            logger.LogTrace($"Parameter {stateParameter.ParameterName} of type {stateParameter.SqlDbType} has value {stateParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
                logger.LogInformation("Successfully Leaving UpdateRoleRightAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in UpdateRoleRightAsync");
                throw;
            }
        }

        private async Task<List<IMembershipRoleEntity>> LoadMembershipRoleEntities(SqlDataReader reader,
                                                                                   CancellationToken cancellationToken = new ())
        {
            List<IMembershipRoleEntity> result = new List<IMembershipRoleEntity>();

            while (await reader.ReadAsync(cancellationToken))
            {
                result.Add(
                    new MembershipRoleEntity
                    {
                        Id = reader.GetInt64("Id"),
                        MembershipId = reader.GetInt64("MembershipId"),
                        RoleId = reader.GetInt64("RoleId")
                    }
                 );
            }

            return result;
        }

        private async Task<List<IRightEntity>> LoadRightEntities(SqlDataReader reader,
                                                                 CancellationToken cancellationToken = new ())
        {
            List<IRightEntity> results = new List<IRightEntity>();

            while (await reader.ReadAsync(cancellationToken))
            {
                results.Add(
                    new RightEntity(reader.GetString("RightName"))
                    {
                        Id = reader.GetInt64("Id"),
                        State = await reader.IsDBNullAsync("State", cancellationToken) ? null : reader.GetBoolean("State")
                    }
                );
            }

            return results;
        }

        private async Task<List<IRoleEntity>> LoadRoleEntities(SqlDataReader reader,
                                                               CancellationToken cancellationToken = new ())
        {
            List<IRoleEntity> results = new List<IRoleEntity>();

            while (await reader.ReadAsync(cancellationToken))
            {
                results.Add(
                    new RoleEntity
                    {
                        Id = reader.GetInt64("Id"),
                        CommunityId = reader.GetInt64("CommunityId"),
                        IsCommunityDefault = reader.GetBoolean("IsCommunityDefault"),
                        IsSystemGeneratedOwner = reader.GetBoolean("IsSystemGeneratedOwner"),
                        Name = reader.GetString("RoleName"),
                        NormalizedName = reader.GetString("NormalizedRoleName")
                    }
                );
            }

            return results;
        }

        private async Task<IRoleEntity> LoadRoleEntity(SqlDataReader reader,
                                                       CancellationToken cancellationToken = new ())
        {
            await reader.ReadAsync(cancellationToken);

            return new RoleEntity            {
                Id = reader.GetInt64("Id"),
                CommunityId = reader.GetInt64("CommunityId"),
                IsCommunityDefault = reader.GetBoolean("IsCommunityDefault"),
                IsSystemGeneratedOwner = reader.GetBoolean("IsSystemGeneratedOwner"),
                Name = reader.GetString("RoleName"),
                NormalizedName = reader.GetString("NormalizedRoleName")
            };
        }
    }
}
