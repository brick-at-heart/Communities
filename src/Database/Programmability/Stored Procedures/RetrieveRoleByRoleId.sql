CREATE PROCEDURE [dbo].[RetrieveRoleByRoleId]
(
	@roleId	BIGINT
)
AS
BEGIN

	UPDATE [dbo].[Role]
	SET LastAccess = GetUtcDate()
	WHERE [Id] = @roleId;

	SELECT
		R.[Id],
		R.[RoleName],
		R.[NormalizedRoleName],
		R.[CommunityId],
		R.[IsDefault]
	FROM [dbo].[Role] R
	WHERE R.[Id] = @roleId;

END