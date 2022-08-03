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
    public partial class SqlServerDataClient : IEventDataClient
    {
        public async Task CreateEventAsync(IEventEntity eventEntity, CancellationToken cancellationToken = new())
        {
            logger.LogDebug("Entered CreateEventAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[CreateEvent]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogDebug($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter idParameter = new SqlParameter("@id", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(idParameter);
            logger.LogDebug($"Parameter {idParameter.ParameterName} of type {idParameter.SqlDbType} with direction {idParameter.Direction}");

            SqlParameter communityParameter = new SqlParameter("@communityId", SqlDbType.BigInt) { Value = eventEntity.CommunityId };
            command.Parameters.Add(communityParameter);
            logger.LogDebug($"Parameter {communityParameter.ParameterName} of type {communityParameter.SqlDbType} has value {communityParameter.Value}");

            SqlParameter nameParameter = new SqlParameter("@name", SqlDbType.NVarChar, 256) { Value = eventEntity.Name };
            command.Parameters.Add(nameParameter);
            logger.LogDebug($"Parameter {nameParameter.ParameterName} of type {nameParameter.SqlDbType} has value {nameParameter.Value}");

            SqlParameter descriptionParameter = new SqlParameter("@description", SqlDbType.NVarChar, 1024) { Value = string.IsNullOrWhiteSpace(eventEntity.Description) ? DBNull.Value : eventEntity.Description };
            command.Parameters.Add(descriptionParameter);
            logger.LogDebug($"Parameter {descriptionParameter.ParameterName} of type {descriptionParameter.SqlDbType} has value {descriptionParameter.Value}");

            SqlParameter locationParameter = new SqlParameter("@location", SqlDbType.NVarChar, 512) { Value = string.IsNullOrEmpty(eventEntity.Location) ? DBNull.Value : eventEntity.Location };
            command.Parameters.Add(locationParameter);
            logger.LogDebug($"Parameter {locationParameter.ParameterName} of type {locationParameter.SqlDbType} has value {locationParameter.Value}");

            SqlParameter statusParameter = new SqlParameter("@status", SqlDbType.TinyInt) { Value = eventEntity.Status };
            command.Parameters.Add(statusParameter);
            logger.LogDebug($"Parameter {statusParameter.ParameterName} of type {statusParameter.SqlDbType} has value {statusParameter.Value}");

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
                eventEntity.Id = (long)idParameter.Value;
                logger.LogDebug("Successfully Leaving CreateEventAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in CreateEventAsync");
                throw;
            }
        }

        public async Task CreateEventScheduleAsync(IEventScheduleEntity eventScheduleEntity, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered CreateEventScheduleAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[CreateEventSchedule]", conn)
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

            SqlParameter eventIdParameter = new SqlParameter("@eventId", SqlDbType.BigInt) { Value = eventScheduleEntity.EventId };
            command.Parameters.Add(eventIdParameter);
            logger.LogTrace($"Parameter {eventIdParameter.ParameterName} of type {eventIdParameter.SqlDbType} has value {eventIdParameter.Value}");

            SqlParameter startParameter = new SqlParameter("@start", SqlDbType.DateTimeOffset) { Value = eventScheduleEntity.Start };
            command.Parameters.Add(startParameter);
            logger.LogTrace($"Parameter {startParameter.ParameterName} of type {startParameter.SqlDbType} has value {startParameter.Value}");

            SqlParameter endParameter = new SqlParameter("@end", SqlDbType.DateTimeOffset) { Value = eventScheduleEntity.End };
            command.Parameters.Add(endParameter);
            logger.LogTrace($"Parameter {endParameter.ParameterName} of type {endParameter.SqlDbType} has value {endParameter.Value}");

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
                eventScheduleEntity.Id = (long)idParameter.Value;
                logger.LogInformation("Successfully Leaving CreateEventScheduleAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in CreateEventScheduleAsync");
                throw;
            }
        }

        public async Task DeleteEventAsync(long eventId, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered DeleteEventAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[DeleteEvent]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter idParameter = new SqlParameter("@id", SqlDbType.BigInt) { Value = eventId };
            command.Parameters.Add(idParameter);
            logger.LogTrace($"Parameter {idParameter.ParameterName} of type {idParameter.SqlDbType} has value {idParameter.Value}");

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
                logger.LogInformation("Successfully Leaving DeleteEventAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in DeleteEventAsync");
                throw;
            }
        }

        public async Task DeleteEventScheduleASync(long eventScheduleId, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered DeleteEventScheduleAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[DeleteEventSchedule]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter idParameter = new SqlParameter("@id", SqlDbType.BigInt) { Value = eventScheduleId };
            command.Parameters.Add(idParameter);
            logger.LogTrace($"Parameter {idParameter.ParameterName} of type {idParameter.SqlDbType} has value {idParameter.Value}");

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
                logger.LogInformation("Successfully Leaving DeleteEventScheduleAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in DeleteEventScheduleAsync");
                throw;
            }
        }

        public async Task<IList<IEventEntity>> RetrieveEventByCommunityIdAsync(long communityId, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered RetrieveEventByCommunityIdAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[RetrieveEventByCommunityId]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter idParameter = new SqlParameter("@id", SqlDbType.BigInt) { Value = communityId };
            command.Parameters.Add(idParameter);
            logger.LogTrace($"Parameter {idParameter.ParameterName} of type {idParameter.SqlDbType} has value {idParameter.Value}");

            if (cancellationToken.IsCancellationRequested)
            {
                return new List<IEventEntity>();
            }

            await conn.OpenAsync(cancellationToken);

            try
            {
                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
                List<IEventEntity> result = LoadEventEntities(reader);

                logger.LogInformation("Successfully Leaving RetrieveEventByCommunityIdAsync");
                return result;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in RetrieveEventByCommunityIdAsync");
                throw;
            }
        }

        public async Task<IEventEntity> RetrieveEventByEventIdAsync(long eventId, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered RetrieveEventByEventIdAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[RetrieveEventByEventId]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter idParameter = new SqlParameter("@id", SqlDbType.BigInt) { Value = eventId };
            command.Parameters.Add(idParameter);
            logger.LogTrace($"Parameter {idParameter.ParameterName} of type {idParameter.SqlDbType} has value {idParameter.Value}");

            if (cancellationToken.IsCancellationRequested)
            {
                return new EventEntity();
            }

            await conn.OpenAsync(cancellationToken);

            try
            {
                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
                IEventEntity result = LoadEventEntity(reader);

                logger.LogInformation("Successfully Leaving RetrieveEventByEventIdAsync");
                return result;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in RetrieveEventByEventIdAsync");
                throw;
            }
        }

        public async Task<IList<IEventScheduleEntity>> RetrieveEventScheduleByCommunityIdAsync(long communityId, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered RetrieveEventScheduleByCommunityIdAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[RetrieveEventScheduleByCommunityId]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter idParameter = new SqlParameter("@id", SqlDbType.BigInt) { Value = communityId };
            command.Parameters.Add(idParameter);
            logger.LogTrace($"Parameter {idParameter.ParameterName} of type {idParameter.SqlDbType} has value {idParameter.Value}");

            if (cancellationToken.IsCancellationRequested)
            {
                return new List<IEventScheduleEntity>();
            }

            await conn.OpenAsync(cancellationToken);

            try
            {
                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
                List<IEventScheduleEntity> result = LoadEventScheduleEntities(reader);

                logger.LogInformation("Successfully Leaving RetrieveEventScheduleByCommunityIdAsync");
                return result;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in RetrieveEventScheduleByCommunityIdAsync");
                throw;
            }
        }

        public async Task<IList<IEventScheduleEntity>> RetrieveEventScheduleByEventIdAsync(long eventId, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered RetrieveEventScheduleByEventIdAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[RetrieveEventScheduleByEventId]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter idParameter = new SqlParameter("@id", SqlDbType.BigInt) { Value = eventId };
            command.Parameters.Add(idParameter);
            logger.LogTrace($"Parameter {idParameter.ParameterName} of type {idParameter.SqlDbType} has value {idParameter.Value}");

            if (cancellationToken.IsCancellationRequested)
            {
                return new List<IEventScheduleEntity>();
            }

            await conn.OpenAsync(cancellationToken);

            try
            {
                SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);
                List<IEventScheduleEntity> result = LoadEventScheduleEntities(reader);

                logger.LogInformation("Successfully Leaving RetrieveEventScheduleByEventIdAsync");
                return result;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in RetrieveEventScheduleByEventIdAsync");
                throw;
            }
        }

        public async Task UpdateEventAsync(IEventEntity eventEntity, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered UpdateEventAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[UpdateEvent]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter idParameter = new SqlParameter("@id", SqlDbType.BigInt) { Value = eventEntity.Id };
            command.Parameters.Add(idParameter);
            logger.LogTrace($"Parameter {idParameter.ParameterName} of type {idParameter.SqlDbType} has value {idParameter.Value}");

            SqlParameter nameParameter = new SqlParameter("@name", SqlDbType.NVarChar, 256) { Value = eventEntity.Name };
            command.Parameters.Add(nameParameter);
            logger.LogTrace($"Parameter {nameParameter.ParameterName} of type {nameParameter.SqlDbType} has value {nameParameter.Value}");

            SqlParameter descriptionParameter = new SqlParameter("@description", SqlDbType.NVarChar, 1024) { Value = string.IsNullOrWhiteSpace(eventEntity.Description) ? DBNull.Value : eventEntity.Description };
            command.Parameters.Add(descriptionParameter);
            logger.LogTrace($"Parameter {descriptionParameter.ParameterName} of type {descriptionParameter.SqlDbType} has value {descriptionParameter.Value}");

            SqlParameter locationParameter = new SqlParameter("@location", SqlDbType.NVarChar, 512) { Value = string.IsNullOrEmpty(eventEntity.Location) ? DBNull.Value : eventEntity.Location };
            command.Parameters.Add(locationParameter);
            logger.LogDebug($"Parameter {locationParameter.ParameterName} of type {locationParameter.SqlDbType} has value {locationParameter.Value}");

            SqlParameter statusParameter = new SqlParameter("@status", SqlDbType.TinyInt) { Value = eventEntity.Status };
            command.Parameters.Add(statusParameter);
            logger.LogTrace($"Parameter {statusParameter.ParameterName} of type {statusParameter.SqlDbType} has value {statusParameter.Value}");

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
                logger.LogInformation("Successfully Leaving UpdateEventAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in UpdateEventAsync");
                throw;
            }
        }

        public async Task UpdateEventScheduleAsync(IEventScheduleEntity eventScheduleEntity, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered UpdateEventScheduleAsync");

            await using SqlConnection conn = new SqlConnection(connectionString);
            await using SqlCommand command = new SqlCommand("[dbo].[UpdateEventSchedule]", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            logger.LogTrace($"Preparing to call stored procedure: {command.CommandText}");

            SqlParameter idParameter = new SqlParameter("@id", SqlDbType.BigInt) { Value = eventScheduleEntity.Id };
            command.Parameters.Add(idParameter);
            logger.LogTrace($"Parameter {idParameter.ParameterName} of type {idParameter.SqlDbType} has value {idParameter.Value}");

            SqlParameter startParameter = new SqlParameter("@start", SqlDbType.DateTimeOffset) { Value = eventScheduleEntity.Start };
            command.Parameters.Add(startParameter);
            logger.LogTrace($"Parameter {startParameter.ParameterName} of type {startParameter.SqlDbType} has value {startParameter.Value}");

            SqlParameter endParameter = new SqlParameter("@end", SqlDbType.DateTimeOffset) { Value = eventScheduleEntity.End };
            command.Parameters.Add(endParameter);
            logger.LogTrace($"Parameter {endParameter.ParameterName} of type {endParameter.SqlDbType} has value {endParameter.Value}");

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            await conn.OpenAsync(cancellationToken);

            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
                logger.LogInformation("Successfully Leaving UpdateEventScheduleAsync");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error in UpdateEventScheduleAsync");
                throw;
            }
        }

        private IEventEntity LoadEventEntity(SqlDataReader reader)
        {
            if (reader.Read())
            {
                return new EventEntity
                {
                    Id = reader.GetInt64("Id"),
                    CommunityId = reader.GetInt64("CommunityId"),
                    Name = reader.GetString("Name"),
                    Status = reader.GetByte("Status"),
                    Description = reader.IsDBNull("Description")
                        ? null
                        : reader.GetString("Description"),
                    Location = reader.IsDBNull("Location")
                        ? null
                        : reader.GetString("Location")
                };
            }

            return null;
        }

        private List<IEventEntity> LoadEventEntities(SqlDataReader reader)
        {
            List<IEventEntity> entities = new List<IEventEntity>();

            while(reader.Read())
            {
                entities.Add(new EventEntity
                {
                    Id = reader.GetInt64("Id"),
                    CommunityId = reader.GetInt64("CommunityId"),
                    Name = reader.GetString("Name"),
                    Status = reader.GetByte("Status"),
                    Description = reader.IsDBNull("Description")
                        ? null
                        : reader.GetString("Description"),
                    Location = reader.IsDBNull("Location")
                        ? null
                        : reader.GetString("Location")
                });
            }

            return entities;
        }

        private List<IEventScheduleEntity> LoadEventScheduleEntities(SqlDataReader reader)
        {
            List<IEventScheduleEntity> entities = new List<IEventScheduleEntity>();

            while(reader.Read())
            {
                entities.Add(new EventScheduleEntity
                {
                    Id = reader.GetInt64("Id"),
                    EventId = reader.GetInt64("EventId"),
                    Start = reader.GetDateTimeOffset(reader.GetOrdinal("Start")),
                    End = reader.GetDateTimeOffset(reader.GetOrdinal("End"))
                });
            }

            return entities;
        }
    }
}
