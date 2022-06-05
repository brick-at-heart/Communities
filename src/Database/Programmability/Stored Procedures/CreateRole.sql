CREATE PROCEDURE [dbo].[CreateRole]
(
	@id					BIGINT OUTPUT,
	@roleName			NVARCHAR(256),
	@normalizedRoleName	NVARCHAR(256),
	@communityId		BIGINT,
	@isDefault			BIT
)
AS
BEGIN

	INSERT INTO [dbo].[Role]
	(
		[RoleName],
		[NormalizedRoleName],
		[CommunityId],
		[IsDefault]
	)
	VALUES
	(
		@roleName,
		@normalizedRoleName,
		@communityId,
		@isDefault
	);

	SET @id = SCOPE_IDENTITY();

	INSERT INTO [dbo].[RoleRight]
	(
		[RoleId],
		[RightId]
	)
	SELECT
		@id,
		R.[Id]
	FROM [dbo].[Right] R

END