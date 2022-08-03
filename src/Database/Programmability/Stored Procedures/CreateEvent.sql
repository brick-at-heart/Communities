CREATE PROCEDURE [dbo].[CreateEvent]
(
	@id		BIGINT	OUTPUT,
	@communityId	BIGINT,
	@name			NVARCHAR(256),
	@status			TINYINT,
	@description	NVARCHAR(2048),
	@location		NVARCHAR(512)
)
AS
BEGIN

	INSERT INTO [dbo].[Event]
	(
		[CommunityId],
		[Name],
		[Status],
		[Description],
		[Location]
	)
	VALUES
	(
		@communityId,
		@name,
		@status,
		@description,
		@location
	)

	SET @id = SCOPE_IDENTITY();
END