CREATE PROCEDURE [dbo].[RetrieveCatalogsByCommunityId]
(
	@communityId	BIGINT
)
AS
BEGIN

	UPDATE [dbo].[Catalog]
	SET [LastAccess] = GetUtcDate()
	WHERE [CommunityId] = @communityId;

	SELECT
		C.[Id],
		C.[Name],
		C.[CommunityId]
	FROM [dbo].[Catalog] C
	WHERE C.[CommunityId] = @communityId
	UNION
	SELECT
		-1,
		N'Global Catalog',
		-1;

END