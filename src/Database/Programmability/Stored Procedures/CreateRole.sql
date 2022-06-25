CREATE PROCEDURE [dbo].[CreateRole]
(
	@id						BIGINT OUTPUT,
	@roleName				NVARCHAR(256),
	@normalizedRoleName		NVARCHAR(256),
	@communityId			BIGINT,
	@isCommunityDefault		BIT,
	@isSystemGeneratedOwner	BIT
)
AS
BEGIN

	INSERT INTO [dbo].[Role]
	(
		[RoleName],
		[NormalizedRoleName],
		[CommunityId],
		[IsCommunityDefault],
		[IsSystemGeneratedOwner]
	)
	VALUES
	(
		@roleName,
		@normalizedRoleName,
		@communityId,
		@isCommunityDefault,
		@isSystemGeneratedOwner
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