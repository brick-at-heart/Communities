CREATE PROCEDURE [dbo].[RetrieveCommunitiesByJoinType]
(
	@joinType	TINYINT
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
		INNER JOIN [dbo].[JoinType] JT
			ON [C].[JoinType] = JT.[AssignedType]
	WHERE JT.[SearchType] = @joinType;

	UPDATE C
	SET [LastAccess] = GetUtcDate()
	FROM [dbo].[Community] C
		INNER JOIN [dbo].[JoinType] JT
			ON [C].[JoinType] = JT.[AssignedType]
	WHERE JT.[SearchType] = @joinType;

END