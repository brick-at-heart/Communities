CREATE PROCEDURE [dbo].[RetrieveCommunityByShortName]
(
	@normalizedShortName	NVARCHAR(64)
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
	WHERE C.[NormalizedShortName] = @normalizedShortName;

	UPDATE [dbo].[Community]
	SET [LastAccess] = GetUtcDate()
	WHERE [NormalizedShortName] = @normalizedShortName;

END