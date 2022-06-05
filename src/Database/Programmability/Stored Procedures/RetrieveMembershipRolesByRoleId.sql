CREATE PROCEDURE [dbo].[RetrieveMembershipRolesByRoleId]
(
	@roleId			BIGINT
)
AS
BEGIN

	UPDATE [dbo].[MembershipRole]
	SET [LastAccess] = GetUtcDate()
	WHERE [RoleId] = @roleId;

	SELECT
		MR.[Id],
		MR.[MembershipId],
		MR.[RoleId],
		U.[DisplayName],
		U.[GivenName],
		U.[SurName]
	FROM [dbo].[MembershipRole] MR
		INNER JOIN [dbo].[Membership] M
			ON MR.[MembershipId] = M.[Id]
		INNER JOIN [dbo].[User] U
			ON M.[UserId] = U.[Id]
	WHERE MR.[RoleId] = @roleId;
	
END