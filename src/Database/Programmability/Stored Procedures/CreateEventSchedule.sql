CREATE PROCEDURE [dbo].[CreateEventSchedule]
(
	@id			BIGINT	OUTPUT,
	@eventId	BIGINT,
	@start		DATETIMEOFFSET,
	@end		DATETIMEOFFSET
)
AS
BEGIN

	INSERT INTO [dbo].[EventSchedule]
	(
		[EventId],
		[Start],
		[End]
	)
	VALUES
	(
		@eventId,
		@start,
		@end
	)

	SET @id = SCOPE_IDENTITY();

END