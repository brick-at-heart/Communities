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
    public partial class SqlServerDataClient : IUserDataClient
    {
        public async Task CreateLoginAsync(ILoginEntity loginEntity, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered CreateLoginAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[CreateLogin]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter providerIdParameter = new SqlParameter("@providerId", SqlDbType.NVarChar, 100) { Value = loginEntity.ProviderId };
            command.Parameters.Add(providerIdParameter);
            logger.LogTrace($"Parameter {providerIdParameter.ParameterName} of type {providerIdParameter.SqlDbType} has value {providerIdParameter.Value}");

            SqlParameter providerKeyParameter = new SqlParameter("@providerKey", SqlDbType.NVarChar, 100) { Value = loginEntity.ProviderKey };
            command.Parameters.Add(providerKeyParameter);
            logger.LogTrace($"Parameter {providerKeyParameter.ParameterName} of type {providerKeyParameter.SqlDbType} has value {providerKeyParameter.Value}");

            SqlParameter providerDisplayNameParameter = new SqlParameter("@providerDisplayName", SqlDbType.NVarChar, 100) { Value = loginEntity.ProviderDisplayName };
            command.Parameters.Add(providerDisplayNameParameter);
            logger.LogTrace($"Parameter {providerDisplayNameParameter.ParameterName} of type {providerDisplayNameParameter.SqlDbType} has value {providerDisplayNameParameter.Value}");

            SqlParameter userIdParameter = new SqlParameter("@userId", SqlDbType.BigInt) { Value = loginEntity.UserId };
            command.Parameters.Add(userIdParameter);
            logger.LogTrace($"Parameter {userIdParameter.ParameterName} of type {userIdParameter.SqlDbType} has value {userIdParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
                logger.LogInformation("Successfully Leaving CreateLoginAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in CreateLoginAsync");
            }
        }

        public async Task CreateUserAsync(IUserEntity userEntity, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered CreateUserAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[CreateUser]", conn)
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

            SqlParameter displayNameParameter = new SqlParameter("@displayName", SqlDbType.NVarChar, 100) { Value = userEntity.DisplayName };
            command.Parameters.Add(displayNameParameter);
            logger.LogTrace($"Parameter {displayNameParameter.ParameterName} of type {displayNameParameter.SqlDbType} has value {displayNameParameter.Value}");

            SqlParameter emailParameter = new SqlParameter("@email", SqlDbType.NVarChar, 320) { Value = userEntity.Email };
            command.Parameters.Add(emailParameter);
            logger.LogTrace($"Parameter {emailParameter.ParameterName} of type {emailParameter.SqlDbType} has value {emailParameter.Value}");

            SqlParameter normalizedEmailParameter = new SqlParameter("@normalizedEmail", SqlDbType.NVarChar, 320) { Value = userEntity.NormalizedEmail };
            command.Parameters.Add(normalizedEmailParameter);
            logger.LogTrace($"Parameter {normalizedEmailParameter.ParameterName} of type {normalizedEmailParameter.SqlDbType} has value {normalizedEmailParameter.Value}");

            SqlParameter emailConfirmedParameter = new SqlParameter("@emailConfirmed", SqlDbType.Bit) { Value = userEntity.EmailConfirmed };
            command.Parameters.Add(emailConfirmedParameter);
            logger.LogTrace($"Parameter {emailConfirmedParameter.ParameterName} of type {emailConfirmedParameter.SqlDbType} has value {emailConfirmedParameter.Value}");

            SqlParameter givenNameParameter = new SqlParameter("@givenName", SqlDbType.NVarChar, 100) { Value = userEntity.GivenName ?? "" };
            command.Parameters.Add(givenNameParameter);
            logger.LogTrace($"Parameter {givenNameParameter.ParameterName} of type {givenNameParameter.SqlDbType} has value {givenNameParameter.Value}");

            SqlParameter surNameParameter = new SqlParameter("@surName", SqlDbType.NVarChar, 100) { Value = userEntity.SurName ?? "" };
            command.Parameters.Add(surNameParameter);
            logger.LogTrace($"Parameter {surNameParameter.ParameterName} of type {surNameParameter.SqlDbType} has value {surNameParameter.Value}");

            SqlParameter phoneNumberParameter = new SqlParameter("@phoneNumber", SqlDbType.NVarChar, 24) { Value = userEntity.PhoneNumber ?? "" };
            command.Parameters.Add(phoneNumberParameter);
            logger.LogTrace($"Parameter {phoneNumberParameter.ParameterName} of type {phoneNumberParameter.SqlDbType} has value {phoneNumberParameter.Value}");

            SqlParameter phoneNumberConfirmedParameter = new SqlParameter("@phoneNumberConfirmed", SqlDbType.Bit) { Value = userEntity.PhoneNumberConfirmed };
            command.Parameters.Add(phoneNumberConfirmedParameter);
            logger.LogTrace($"Parameter {phoneNumberConfirmedParameter.ParameterName} of type {phoneNumberConfirmedParameter.SqlDbType} has value {phoneNumberConfirmedParameter.Value}");

            SqlParameter streetAddressLineOneParameter = new SqlParameter("@streetAddressLine1", SqlDbType.NVarChar, 100) { Value = userEntity.StreetAddressLine1 ?? "" };
            command.Parameters.Add(streetAddressLineOneParameter);
            logger.LogTrace($"Parameter {streetAddressLineOneParameter.ParameterName} of type {streetAddressLineOneParameter.SqlDbType} has value {streetAddressLineOneParameter.Value}");

            SqlParameter streetAddressLineTwoParameter = new SqlParameter("@streetAddressLine2", SqlDbType.NVarChar, 100) { Value = userEntity.StreetAddressLine2 ?? "" };
            command.Parameters.Add(streetAddressLineTwoParameter);
            logger.LogTrace($"Parameter {streetAddressLineTwoParameter.ParameterName} of type {streetAddressLineTwoParameter.SqlDbType} has value {streetAddressLineTwoParameter.Value}");

            SqlParameter cityParameter = new SqlParameter("@city", SqlDbType.NVarChar, 100) { Value = userEntity.City ?? "" };
            command.Parameters.Add(cityParameter);
            logger.LogTrace($"Parameter {cityParameter.ParameterName} of type {cityParameter.SqlDbType} has value {cityParameter.Value}");

            SqlParameter regionParameter = new SqlParameter("@region", SqlDbType.NVarChar, 100) { Value = userEntity.Region ?? "" };
            command.Parameters.Add(regionParameter);
            logger.LogTrace($"Parameter {regionParameter.ParameterName} of type {regionParameter.SqlDbType} has value {regionParameter.Value}");

            SqlParameter countryParameter = new SqlParameter("@country", SqlDbType.NVarChar, 100) { Value = userEntity.Country ?? "" };
            command.Parameters.Add(countryParameter);
            logger.LogTrace($"Parameter {countryParameter.ParameterName} of type {countryParameter.SqlDbType} has value {countryParameter.Value}");

            SqlParameter postalCodeParameter = new SqlParameter("@postalCode", SqlDbType.NVarChar, 100) { Value = userEntity.PostalCode ?? "" };
            command.Parameters.Add(postalCodeParameter);
            logger.LogTrace($"Parameter {postalCodeParameter.ParameterName} of type {postalCodeParameter.SqlDbType} has value {postalCodeParameter.Value}");

            SqlParameter dateOfBirthParameter = new SqlParameter("@dateOfBirth", SqlDbType.Date) { Value = userEntity.DateOfBirth };
            command.Parameters.Add(dateOfBirthParameter);
            logger.LogTrace($"Parameter {dateOfBirthParameter.ParameterName} of type {dateOfBirthParameter.SqlDbType} has value {dateOfBirthParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
                userEntity.Id = (long)idParameter.Value;
                logger.LogInformation("Successfully Leaving CreateUserAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in CreateUserAsync");
                throw;
            }
        }

        public async Task DeleteLoginAsync(ILoginEntity loginEntity, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered DeleteLoginAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[DeleteLogin]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter providerIdParameter = new SqlParameter("@providerId", SqlDbType.NVarChar, 100) { Value = loginEntity.ProviderId };
            command.Parameters.Add(providerIdParameter);
            logger.LogTrace($"Parameter {providerIdParameter.ParameterName} of type {providerIdParameter.SqlDbType} has value {providerIdParameter.Value}");

            SqlParameter providerUserKeyParameter = new SqlParameter("@providerKey", SqlDbType.NVarChar, 100) { Value = loginEntity.ProviderKey };
            command.Parameters.Add(providerUserKeyParameter);
            logger.LogTrace($"Parameter {providerUserKeyParameter.ParameterName} of type {providerUserKeyParameter.SqlDbType} has value {providerUserKeyParameter.Value}");

            SqlParameter userIdParameter = new SqlParameter("@userId", SqlDbType.BigInt) { Value = loginEntity.UserId };
            command.Parameters.Add(userIdParameter);
            logger.LogTrace($"Parameter {userIdParameter.ParameterName} of type {userIdParameter.SqlDbType} has value {userIdParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
                logger.LogInformation("Successfully Leaving DeleteLoginAsync");
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error in DeleteLoginAsync");
            }
        }

        public async Task DeleteUserAsync(IUserEntity userEntity, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered DeleteUserAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[DeleteUser]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter userIdParameter = new SqlParameter("@id", SqlDbType.BigInt) { Value = userEntity.Id };
            command.Parameters.Add(userIdParameter);
            logger.LogTrace($"Parameter {userIdParameter.ParameterName} of type {userIdParameter.SqlDbType} has value {userIdParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
                logger.LogInformation("Successfully Leaving DeleteUserAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in DeleteUserAsync");
                throw;
            }
        }

        public async Task<IList<ILoginEntity>> RetrieveLoginsByUserIdAsync(long userId, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered RetrieveLoginsByUserIdAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[RetrieveLoginByUserId]", conn)
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

                List<ILoginEntity> logins = new List<ILoginEntity>();

                while (await reader.ReadAsync(cancellationToken))
                {
                    string loginProvider = reader.GetString("ProviderId");
                    string providerDisplayName = reader.GetString("ProviderDisplayName");
                    string providerKey = reader.GetString("ProviderKey");

                    logins.Add(new LoginEntity(loginProvider, providerKey)
                    {
                        ProviderDisplayName = providerDisplayName,
                        UserId = userId
                    });
                }

                logger.LogInformation("Successfully Leaving RetrieveLoginsByUserIdAsync");
                return logins;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in RetrieveLoginsByUserIdAsync");
                throw;
            }
        }

        public async Task<IUserEntity> RetrieveUserByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered RetrieveUserByEmailAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[RetrieveUserByEmail]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter normalizedEmailParameter = new SqlParameter("@normalizedEmail", SqlDbType.NVarChar, 320) { Value = normalizedEmail };
            command.Parameters.Add(normalizedEmailParameter);
            logger.LogTrace($"Parameter {normalizedEmailParameter.ParameterName} of type {normalizedEmailParameter.SqlDbType} has value {normalizedEmailParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
                IUserEntity userEntity = await LoadUserEntity(reader, cancellationToken);

                logger.LogInformation("Successfully Leaving RetrieveUserByEmailAsync");

                return userEntity;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in RetrieveUserByEmailAsync");
                throw;
            }
        }

        public async Task<IUserEntity> RetrieveUserByLoginAsync(string providerId, string providerKey, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered RetrieveUserByLoginAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[RetrieveUserByLogin]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter providerIdParameter = new SqlParameter("@providerId", SqlDbType.NVarChar, 100) { Value = providerId };
            command.Parameters.Add(providerIdParameter);
            logger.LogTrace($"Parameter {providerIdParameter.ParameterName} of type {providerIdParameter.SqlDbType} has value {providerIdParameter.Value}");

            SqlParameter providerKeyParameter = new SqlParameter("@providerKey", SqlDbType.NVarChar, 100) { Value = providerKey };
            command.Parameters.Add(providerKeyParameter);
            logger.LogTrace($"Parameter {providerKeyParameter.ParameterName} of type {providerKeyParameter.SqlDbType} has value {providerKeyParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
                IUserEntity userEntity = await LoadUserEntity(reader, cancellationToken);

                return userEntity;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in RetrieveUserByLoginAsync");
                throw;
            }
        }

        public async Task<IUserEntity> RetrieveUserByUserIdAsync(long userId, CancellationToken cancellationToken)
        {
            logger.LogInformation("Entered RetrieveUserByLoginAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[RetrieveUserByUserId]", conn)
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
                IUserEntity userEntity = await LoadUserEntity(reader, cancellationToken);

                logger.LogInformation("Successfully Leaving RetrieveUserByLoginAsync");

                return userEntity;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in RetrieveUserByLoginAsync");
                throw;
            }
        }

        public async Task<IUserEntity> RetrieveUserByUserNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            logger.LogInformation("Entered RetrieveUserByUserNameAsync");

            try
            {
                return await RetrieveUserByEmailAsync(normalizedUserName, cancellationToken);
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in RetrieveUserByUserNameAsync");
                throw;
            }
        }

        public async Task<IList<IUserEntity>> RetrieveUsersByCommunityIdAsync(long communityId, CancellationToken cancellationToken)
        {
            logger.LogInformation("Entered RetrieveUsersByCommunityIdAsync");

            try
            {
                await using SqlConnection conn = new SqlConnection(connectionString);
                await using SqlCommand command = new SqlCommand("[dbo].[RetrieveUsersByCommunityId]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

                SqlParameter communityIdParameter = new SqlParameter("@communityId", SqlDbType.BigInt) { Value = communityId };
                command.Parameters.Add(communityIdParameter);
                logger.LogTrace($"Parameter {communityIdParameter.ParameterName} of type {communityIdParameter.SqlDbType} has value {communityIdParameter.Value}");

                await conn.OpenAsync(cancellationToken);

                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
                IList<IUserEntity> entities = await LoadUserEntities(reader, cancellationToken);

                logger.LogInformation("Successfully Leaving RetrieveUsersByCommunityIdAsync");
                return entities;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in RetrieveUsersByCommunityIdAsync");
                throw;
            }
        }

        public async Task UpdateUserAsync(IUserEntity userEntity, CancellationToken cancellationToken)
        {
            logger.LogInformation("Entered UpdateUserAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[UpdateUser]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter idParameter = new SqlParameter("@id", SqlDbType.BigInt) { Value = userEntity.Id };
            command.Parameters.Add(idParameter);
            logger.LogTrace($"Parameter {idParameter.ParameterName} of type {idParameter.SqlDbType} has value {idParameter.Value}");

            SqlParameter displayNameParameter = new SqlParameter("displayName", SqlDbType.NVarChar, 100) { Value = userEntity.DisplayName };
            command.Parameters.Add(displayNameParameter);
            logger.LogTrace($"Parameter {displayNameParameter.ParameterName} of type {displayNameParameter.SqlDbType} has value {displayNameParameter.Value}");

            SqlParameter isActiveParameter = new SqlParameter("isActive", SqlDbType.Bit) { Value = userEntity.IsActive };
            command.Parameters.Add(isActiveParameter);
            logger.LogTrace($"Parameter {isActiveParameter.ParameterName} of type {isActiveParameter.SqlDbType} has value {isActiveParameter.Value}");

            SqlParameter isApprovedParameter = new SqlParameter("isApproved", SqlDbType.Bit) { Value = userEntity.IsApproved };
            command.Parameters.Add(isApprovedParameter);
            logger.LogTrace($"Parameter {isApprovedParameter.ParameterName} of type {isApprovedParameter.SqlDbType} has value {isApprovedParameter.Value}");

            SqlParameter emailParameter = new SqlParameter("email", SqlDbType.NVarChar, 320) { Value = userEntity.Email };
            command.Parameters.Add(emailParameter);
            logger.LogTrace($"Parameter {emailParameter.ParameterName} of type {emailParameter.SqlDbType} has value {emailParameter.Value}");

            SqlParameter normalizedEmailParameter = new SqlParameter("normalizedEmail", SqlDbType.NVarChar, 320) { Value = userEntity.NormalizedEmail };
            command.Parameters.Add(normalizedEmailParameter);
            logger.LogTrace($"Parameter {normalizedEmailParameter.ParameterName} of type {normalizedEmailParameter.SqlDbType} has value {normalizedEmailParameter.Value}");

            SqlParameter emailConfirmedParameter = new SqlParameter("emailConfirmed", SqlDbType.Bit) { Value = userEntity.EmailConfirmed };
            command.Parameters.Add(emailConfirmedParameter);
            logger.LogTrace($"Parameter {emailConfirmedParameter.ParameterName} of type {emailConfirmedParameter.SqlDbType} has value {emailConfirmedParameter.Value}");

            SqlParameter givenNameParameter = new SqlParameter("givenName", SqlDbType.NVarChar, 100) { Value = userEntity.GivenName ?? "" };
            command.Parameters.Add(givenNameParameter);
            logger.LogTrace($"Parameter {givenNameParameter.ParameterName} of type {givenNameParameter.SqlDbType} has value {givenNameParameter.Value}");

            SqlParameter surNameParameter = new SqlParameter("surName", SqlDbType.NVarChar, 100) { Value = userEntity.SurName ?? "" };
            command.Parameters.Add(surNameParameter);
            logger.LogTrace($"Parameter {surNameParameter.ParameterName} of type {surNameParameter.SqlDbType} has value {surNameParameter.Value}");

            SqlParameter phoneNumberParameter = new SqlParameter("phoneNumber", SqlDbType.NVarChar, 24) { Value = userEntity.PhoneNumber ?? "" };
            command.Parameters.Add(phoneNumberParameter);
            logger.LogTrace($"Parameter {phoneNumberParameter.ParameterName} of type {phoneNumberParameter.SqlDbType} has value {phoneNumberParameter.Value}");

            SqlParameter phoneNumberConfirmedParameter = new SqlParameter("phoneNumberConfirmed", SqlDbType.Bit) { Value = userEntity.PhoneNumberConfirmed };
            command.Parameters.Add(phoneNumberConfirmedParameter);
            logger.LogTrace($"Parameter {phoneNumberConfirmedParameter.ParameterName} of type {phoneNumberConfirmedParameter.SqlDbType} has value {phoneNumberConfirmedParameter.Value}");

            SqlParameter streetAddressLineOneParameter = new SqlParameter("streetAddressLine1", SqlDbType.NVarChar, 100) { Value = userEntity.StreetAddressLine1 ?? "" };
            command.Parameters.Add(streetAddressLineOneParameter);
            logger.LogTrace($"Parameter {streetAddressLineOneParameter.ParameterName} of type {streetAddressLineOneParameter.SqlDbType} has value {streetAddressLineOneParameter.Value}");

            SqlParameter streetAddressLineTwoParameter = new SqlParameter("streetAddressLine2", SqlDbType.NVarChar, 100) { Value = userEntity.StreetAddressLine2 ?? "" };
            command.Parameters.Add(streetAddressLineTwoParameter);
            logger.LogTrace($"Parameter {streetAddressLineTwoParameter.ParameterName} of type {streetAddressLineTwoParameter.SqlDbType} has value {streetAddressLineTwoParameter.Value}");

            SqlParameter cityParameter = new SqlParameter("city", SqlDbType.NVarChar, 100) { Value = userEntity.City ?? "" };
            command.Parameters.Add(cityParameter);
            logger.LogTrace($"Parameter {cityParameter.ParameterName} of type {cityParameter.SqlDbType} has value {cityParameter.Value}");

            SqlParameter regionParameter = new SqlParameter("region", SqlDbType.NVarChar, 100) { Value = userEntity.Region ?? "" };
            command.Parameters.Add(regionParameter);
            logger.LogTrace($"Parameter {regionParameter.ParameterName} of type {regionParameter.SqlDbType} has value {regionParameter.Value}");

            SqlParameter countryParameter = new SqlParameter("country", SqlDbType.NVarChar, 100) { Value = userEntity.Country ?? "" };
            command.Parameters.Add(countryParameter);
            logger.LogTrace($"Parameter {countryParameter.ParameterName} of type {countryParameter.SqlDbType} has value {countryParameter.Value}");

            SqlParameter postalCodeParameter = new SqlParameter("postalCode", SqlDbType.NVarChar, 100) { Value = userEntity.PostalCode ?? "" };
            command.Parameters.Add(postalCodeParameter);
            logger.LogTrace($"Parameter {postalCodeParameter.ParameterName} of type {postalCodeParameter.SqlDbType} has value {postalCodeParameter.Value}");

            SqlParameter dateOfBirthParameter = new SqlParameter("dateOfBirth", SqlDbType.Date) { Value = userEntity.DateOfBirth };
            command.Parameters.Add(dateOfBirthParameter);
            logger.LogTrace($"Parameter {dateOfBirthParameter.ParameterName} of type {dateOfBirthParameter.SqlDbType} has value {dateOfBirthParameter.Value}");

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);

                logger.LogInformation("Successfully Leaving UpdateUserAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in UpdateUserAsync");
                throw;
            }
        }

        private async Task<IList<IUserEntity>> LoadUserEntities(SqlDataReader reader, CancellationToken cancellationToken = new ())
        {
            IList<IUserEntity> result = new List<IUserEntity>();
            
            if (!reader.HasRows)
            {
                return result;
            }

            while (await reader.ReadAsync(cancellationToken))
            {
                result.Add(new UserEntity()
                {
                    Id = reader.GetInt64("Id"),
                    IsActive = reader.GetBoolean("IsActive"),
                    IsApproved = reader.GetBoolean("IsApproved"),
                    DisplayName = reader.GetString("DisplayName"),
                    Email = reader.GetString("Email"),
                    NormalizedEmail = reader.GetString("NormalizedEmail"),
                    EmailConfirmed = reader.GetBoolean("EmailConfirmed"),
                    GivenName = reader.GetString("GivenName"),
                    SurName = reader.GetString("SurName"),
                    PhoneNumber = reader.GetString("PhoneNumber"),
                    PhoneNumberConfirmed = reader.GetBoolean("PhoneNumberConfirmed"),
                    StreetAddressLine1 = reader.GetString("StreetAddressLine1"),
                    StreetAddressLine2 = reader.GetString("StreetAddressLine2"),
                    City = reader.GetString("City"),
                    Region = reader.GetString("Region"),
                    Country = reader.GetString("Country"),
                    PostalCode = reader.GetString("PostalCode"),
                    DateOfBirth = reader.GetDateTime("DateOfBirth")
                });
            }

            return result;
        }

        private async Task<IUserEntity> LoadUserEntity(SqlDataReader reader, CancellationToken cancellationToken = new ())
        {
            if (!reader.HasRows)
            {
                return null;
            }

            await reader.ReadAsync(cancellationToken);
            
            return new UserEntity()
            {
                Id = reader.GetInt64("Id"),
                IsActive = reader.GetBoolean("IsActive"),
                IsApproved = reader.GetBoolean("IsApproved"),
                Email = reader.GetString("Email"),
                DisplayName = reader.GetString("DisplayName"),
                NormalizedEmail = reader.GetString("NormalizedEmail"),
                EmailConfirmed = reader.GetBoolean("EmailConfirmed"),
                GivenName = reader.GetString("GivenName"),
                SurName = reader.GetString("SurName"),
                PhoneNumber = reader.GetString("PhoneNumber"),
                PhoneNumberConfirmed = reader.GetBoolean("PhoneNumberConfirmed"),
                StreetAddressLine1 = reader.GetString("StreetAddressLine1"),
                StreetAddressLine2 = reader.GetString("StreetAddressLine2"),
                City = reader.GetString("City"),
                Region = reader.GetString("Region"),
                Country = reader.GetString("Country"),
                PostalCode = reader.GetString("PostalCode"),
                DateOfBirth = reader.GetDateTime("DateOfBirth")
            };

        }
    }
}