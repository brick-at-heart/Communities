CREATE PROCEDURE [dbo].[RetrieveUserByLogin]
(
	@providerId		VARCHAR(100),
	@providerKey	VARCHAR(100)
)
AS
BEGIN

	-- Update Last Access on User
	UPDATE U
	SET [LastAccess] = GetUtcDate()
	FROM [dbo].[User] U
		INNER JOIN [dbo].[Login] L
			ON U.[Id] = L.[UserId]
	WHERE L.[ProviderId] = @providerId AND
		  L.[ProviderKey] = @providerKey;

	-- Return Desired User
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
		U.[DateOfBirth]
	FROM [dbo].[User] U
		INNER JOIN [dbo].[Login] L
			ON U.[Id] = L.[UserId]
	WHERE L.[ProviderId] = @providerId AND
		  L.[ProviderKey] = @providerKey;

END