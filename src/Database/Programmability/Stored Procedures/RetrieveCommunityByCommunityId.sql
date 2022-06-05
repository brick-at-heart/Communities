CREATE PROCEDURE [dbo].[RetrieveCommunityByCommunityId]
(
	@id	BIGINT
)
AS
BEGIN

	SELECT
		C.[Id],
		C.[FullName],
		C.[JoinType],
		C.[NormalizedFullName],
		C.[ShortName],
		C.[NormalizedShortName],
		C.[SlackWorkspaceId]
	FROM [dbo].[Community] C
	WHERE C.[Id] = @id;

	UPDATE [dbo].[Community]
	SET [LastAccess] = GetUtcDate()
	WHERE [Id] = @id;

END
