CREATE PROCEDURE [dbo].[RetrieveRightByRightIdUserId]
(
	@rightId	BIGINT, 
	@userId		BIGINT
)
AS
BEGIN

	SELECT
		RR.[Id],
		R.[RightName],
		RR.[State]
	FROM [dbo].[RoleRight] RR
		INNER JOIN [dbo].[MembershipRole] MR
			ON RR.[RoleId] = MR.[RoleId]
		INNER JOIN [dbo].[Membership] M
			ON MR.[MembershipId] = M.[Id]
		INNER JOIN [dbo].[Right] R
			ON RR.[RightId] = R.[Id]
	WHERE M.[UserId] = @userId AND
	      RR.[RightId] = @rightId;

END