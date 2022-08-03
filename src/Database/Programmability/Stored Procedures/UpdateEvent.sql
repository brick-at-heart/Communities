CREATE PROCEDURE [dbo].[UpdateEvent]
(
	@id				BIGINT,
	@name			NVARCHAR(256),
	@description	NVARCHAR(2048),
	@location		NVARCHAR(512),
	@status			TINYINT
)
AS
BEGIN

	UPDATE [dbo].[Event]
	SET [Name] = @name,
		[Description] = @description,
		[Location] = @location,
		[Status] = @status,
		[Updated] = GetUtcDate()
	WHERE [Id] = @id;

END