CREATE PROCEDURE [dbo].[RetrieveRightsByRoleId]
(
	@roleId	BIGINT
)
AS
BEGIN

	UPDATE [dbo].[RoleRight]
	SET LastAccess = GetUtcDate()
	WHERE [RoleId] = @roleId;

	SELECT
		RR.[Id],
		R.[RightName],
		RR.[State]
	FROM [dbo].[RoleRight] RR
		INNER JOIN [dbo].[Right] R
			ON RR.[RightId] = R.[Id]
	WHERE RR.[RoleId] = @roleId;

END