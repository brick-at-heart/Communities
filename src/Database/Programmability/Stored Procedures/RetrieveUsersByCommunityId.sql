CREATE PROCEDURE [dbo].[RetrieveUsersByCommunityId]
(
	@communityId	BIGINT
)
AS
BEGIN

	UPDATE U
	SET [LastAccess] = GetUtcDate()
	FROM [dbo].[Community] C
		INNER JOIN [dbo].[Membership] M
			ON C.[Id] = M.[CommunityId]
		INNER JOIN [dbo].[User] U
			ON M.[UserId] = U.[Id]
	WHERE C.[Id] = @communityId;

	SELECT
		U.[Id],
		U.[IsActive],
		U.[IsApproved],
		U.[DisplayName],
		U.[Email],
		U.[EmailConfirmed],
		U.[NormalizedEmail],
		U.[GivenName],
		U.[SurName],
		U.[PhoneNumber],
		U.[PhoneNumberConfirmed],
		U.[StreetAddressLine1],
		U.[StreetAddressLine2],
		U.[City],
		U.[Region],
		U.[Country],
		U.[PostalCode],
		U.[DateOfBirth],
		U.[TimeZone]
	FROM [dbo].[Community] C
		INNER JOIN [dbo].[Membership] M
			ON C.[Id] = M.[CommunityId]
		INNER JOIN [dbo].[User] U
			ON M.[UserId] = U.[Id]
	WHERE C.[Id] = @communityId;

END