CREATE PROCEDURE [dbo].[RetrieveCommunityByFullName]
(
	@normalizedFullName	NVARCHAR(256)
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
	WHERE C.[NormalizedFullName] = @normalizedFullName;

	UPDATE [dbo].[Community]
	SET [LastAccess] = GetUtcDate()
	WHERE [NormalizedFullName] = @normalizedFullName;

END