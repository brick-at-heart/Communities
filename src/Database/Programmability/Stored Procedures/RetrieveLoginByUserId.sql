CREATE PROCEDURE [dbo].[RetrieveLoginByUserId]
(
	@userId	BIGINT
)
AS
BEGIN

	UPDATE [dbo].[Login]
	SET [LastAccess] = GetUtcDate()
	WHERE [UserId] = @userId;

	SELECT
		L.[ProviderId],
		L.[ProviderDisplayName],
		L.[ProviderKey]
	FROM [dbo].[Login] L
		INNER JOIN [dbo].[User] U
			ON L.[UserId] = U.[Id]
	WHERE L.[UserId] = @userId;

END