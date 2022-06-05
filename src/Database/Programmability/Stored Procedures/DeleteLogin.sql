CREATE PROCEDURE [dbo].[DeleteLogin]
(
	@providerId		VARCHAR(100),
	@providerKey	VARCHAR(100),
	@userId			BIGINT
)
AS
BEGIN

	DELETE
	FROM [dbo].[Login]
	WHERE [UserId] = @userId AND
		  [ProviderId] = @providerId AND
		  [ProviderKey] = @providerKey;

END