CREATE PROCEDURE [dbo].[RetrieveMembershipsByUserId]
(
	@userId	BIGINT
)
AS
BEGIN

	UPDATE M
	SET [LastAccess] = GetUtcDate()
	FROM [dbo].[Membership] M
	WHERE M.[UserId] = @userId;

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
	WHERE M.[UserId] = @userId;

END