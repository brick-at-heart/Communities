CREATE PROCEDURE [dbo].[CreateMembershipRole]
(
	@id				BIGINT OUTPUT,
	@membershipId	BIGINT,
	@roleId			BIGINT
)
AS
BEGIN

	INSERT INTO [dbo].[MembershipRole]
	(
		[MembershipId],
		[RoleId]
	)
	VALUES
	(
		@membershipId,
		@roleId
	);

	SET @id = SCOPE_IDENTITY();
	
END