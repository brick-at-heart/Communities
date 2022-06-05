CREATE PROCEDURE [dbo].[CreateCatalog]
(
	@id					BIGINT OUTPUT,
	@name				NVARCHAR(64),
	@communityId		BIGINT
)
AS
BEGIN

	INSERT INTO [dbo].[Catalog]
	(
		[Name],
		[CommunityId]
	)
	VALUES
	(
		@name,
		@communityId
	);

	SET @id = SCOPE_IDENTITY();

END