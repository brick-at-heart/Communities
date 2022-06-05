CREATE PROCEDURE [dbo].[RetrieveMembershipByMembershipId]
(
	@membershipId	BIGINT
)
AS
BEGIN

	UPDATE [dbo].[Membership]
	SET [LastAccess] = GetUtcDate()
	WHERE [Id] = @membershipId;

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
	WHERE M.[Id] = @membershipId;

END