CREATE PROCEDURE [dbo].[DeleteMembershipRole]
(
	@membershipRoleId	BIGINT
)
AS
BEGIN

	DELETE FROM [dbo].[MembershipRole]
	WHERE [Id] = @membershipRoleId;
	
END