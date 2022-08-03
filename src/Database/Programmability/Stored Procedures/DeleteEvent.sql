CREATE PROCEDURE [dbo].[DeleteEvent]
(
	@id	BIGINT
)
AS
BEGIN

	DELETE
	FROM [dbo].[EventSchedule]
	WHERE [EventId] = @id;

	DELETE
	FROM [dbo].[Event]
	WHERE [Id] = @id;

END