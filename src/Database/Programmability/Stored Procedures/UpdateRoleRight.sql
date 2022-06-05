CREATE PROCEDURE [dbo].[UpdateRoleRight]
(
	@roleRightId	BIGINT,
	@state			BIT
)
AS
BEGIN

	UPDATE [dbo].[RoleRight]
	SET	[State] = @state,
		[Updated] = GetUtcDate()
	WHERE [Id] = @roleRightId;

END