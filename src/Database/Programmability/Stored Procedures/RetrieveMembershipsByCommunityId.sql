CREATE PROCEDURE [dbo].[RetrieveMembershipsByCommunityId]
(
	@communityId	BIGINT
)
AS
BEGIN

	UPDATE M
	SET [LastAccess] = GetUtcDate()
	FROM [dbo].[Membership] M
	WHERE M.[CommunityId] = @communityId;

	SELECT
		M.[Id],
		M.[UserId],
		M.[CommunityId],
		M.[IsActive],
		M.[IsCurrent],
		M.[IsPrimary],
		M.[SlackMemberId],
		U.[DisplayName],
		U.[GivenName],
		U.[SurName]
	FROM [dbo].[Membership] M
		INNER JOIN [dbo].[User] U
			ON M.[UserId] = U.[Id]
	WHERE M.[CommunityId] = @communityId;

END