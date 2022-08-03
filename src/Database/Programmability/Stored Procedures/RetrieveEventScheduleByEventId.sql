CREATE PROCEDURE [dbo].[RetrieveEventScheduleByEventId]
(
	@id	BIGINT
)
AS
BEGIN

	UPDATE [dbo].[EventSchedule]
	SET [LastAccess] = GetUtcDate()
	WHERE [EventId] = @id;

	SELECT
		ES.[Id],
		ES.[EventId],
		ES.[Start],
		ES.[End]
	FROM [dbo].[EventSchedule] ES
	WHERE [EventId] = @id;

END
