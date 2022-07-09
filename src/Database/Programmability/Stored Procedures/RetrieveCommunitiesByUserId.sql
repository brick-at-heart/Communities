CREATE PROCEDURE [dbo].[RetrieveCommunitiesByUserId]
(
	@userId		BIGINT
)
AS
BEGIN

	SELECT DISTINCT
		C.[Id],
		C.[FullName],
		C.[JoinType],
		C.[NormalizedFullName],
		C.[ShortName],
		C.[NormalizedShortName],
		C.[SlackWorkspaceId]
	FROM [dbo].[Community] C
		INNER JOIN [dbo].[Membership] M
			ON C.[Id] = M.[CommunityId]
		INNER JOIN [dbo].[User] U
			ON U.[Id] = M.[UserId]
		INNER JOIN [dbo].[MembershipRole] MR
			ON M.[Id] = MR.[MembershipId]
	WHERE U.[Id] = @userId;

	UPDATE C
	SET [LastAccess] = GetUtcDate()
	FROM [dbo].[Community] C
		INNER JOIN [dbo].[Membership] M
			ON C.[Id] = M.[CommunityId]
		INNER JOIN [dbo].[User] U
			ON U.[Id] = M.[UserId]
		INNER JOIN [dbo].[MembershipRole] MR
			ON M.[Id] = MR.[MembershipId]
	WHERE U.[Id] = @userId;

END