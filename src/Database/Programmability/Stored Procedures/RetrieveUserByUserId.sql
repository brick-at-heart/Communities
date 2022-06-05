CREATE PROCEDURE [dbo].[RetrieveUserByUserId]
(
	@userId	BIGINT
)
AS
BEGIN

	UPDATE [dbo].[User]
	SET [LastAccess] = GetUtcDate()
	WHERE [Id] = @userId;

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
	WHERE U.[Id] = @userId;
	
END