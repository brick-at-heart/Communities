CREATE PROCEDURE [dbo].[RetrieveCommunity]
AS
BEGIN

	-- This method should only ever be called, if the WebApplication is in Single LUG mode.
	-- It assumes there is one, and only one, LUG.

	SELECT
		C.[Id],
		C.[FullName],
		C.[JoinType],
		C.[NormalizedFullName],
		C.[ShortName],
		C.[NormalizedShortName],
		C.[SlackWorkspaceId]
	FROM [dbo].[Community] C

	UPDATE [dbo].[Community]
	SET [LastAccess] = GetUtcDate()

END