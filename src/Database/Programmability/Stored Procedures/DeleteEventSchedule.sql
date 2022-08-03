CREATE PROCEDURE [dbo].[DeleteEventSchedule]
(
	@id	BIGINT
)
AS
BEGIN

	DELETE
	FROM [dbo].[EventSchedule]
	WHERE [Id] = @id;

END