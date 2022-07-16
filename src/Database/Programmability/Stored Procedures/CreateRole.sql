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

	DECLARE @GRANTED	BIT = 1;
	DECLARE @TRUE		BIT = 1;

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

	IF @isSystemGeneratedOwner = @TRUE
		BEGIN
			INSERT INTO [dbo].[RoleRight]
			(
				[RoleId],
				[RightId],
				[State]
			)
			SELECT
				@id,
				R.[Id],
				@GRANTED
			FROM [dbo].[Right] R
		END
	ELSE
		BEGIN
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

END