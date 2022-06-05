CREATE PROCEDURE [dbo].[CreateLogin]
(
	@providerId				VARCHAR(100),
	@providerDisplayName	VARCHAR(100),
	@providerKey			VARCHAR(100),
	@userId					BIGINT
)
AS
BEGIN

	INSERT INTO [dbo].[Login]
	(
		[ProviderId],
		[ProviderKey],
		[ProviderDisplayName],
		[UserId],
		[Created]
	)
	VALUES
	(
		@providerId,
		@providerKey,
		@providerDisplayName,
		@userId,
		GetUtcDate()
	);

END