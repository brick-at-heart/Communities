CREATE PROCEDURE [dbo].[RetrieveRolesByCommunityId]
(
	@communityId	BIGINT
)
AS
BEGIN

	UPDATE [dbo].[Role]
	SET LastAccess = GetUtcDate()
	FROM [dbo].[Role] R
		INNER JOIN [dbo].[Community] C
		 ON R.[CommunityId] = C.[Id]
	WHERE C.[Id] = @communityId;

	SELECT
		R.[Id],
		R.[RoleName],
		R.[NormalizedRoleName],
		R.[CommunityId],
		R.[IsDefault]
	FROM [dbo].[Role] R
		INNER JOIN [dbo].[Community] C
		 ON R.[CommunityId] = C.[Id];

END