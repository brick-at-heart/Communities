CREATE PROCEDURE [dbo].[DeleteRole]
(
	@id	BIGINT
)
AS
BEGIN

	DELETE FROM [dbo].[MembershipRole]
	WHERE [RoleId] = @id;

	DELETE FROM [dbo].[RoleRight]
	WHERE [RoleId] = @id;

	DELETE FROM [dbo].[Role]
	WHERE [Id] = @id;

END