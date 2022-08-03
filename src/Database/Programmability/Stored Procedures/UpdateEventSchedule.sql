CREATE PROCEDURE [dbo].[UpdateEventSchedule]
(
	@id		BIGINT,
	@start	DATETIMEOFFSET,
	@end	DATETIMEOFFSET
)
AS
BEGIN

	UPDATE [dbo].[EventSchedule]
	SET [Start] = @start,
		[End] = @end,
		[Updated] = GetUtcDate()
	WHERE [Id] = @id;

END