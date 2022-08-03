CREATE PROCEDURE [dbo].[RetrieveEventByCommunityId]
(
	@id	BIGINT
)
AS
BEGIN

	UPDATE [dbo].[Event]
	SET [LastAccess] = GetUtcDate()
	WHERE [CommunityId] = @id;

	SELECT
		E.[Id],
		E.[CommunityId],
		E.[Name],
		E.[Status],
		E.[Description],
		E.[Location]
	FROM [dbo].[Event] E
	WHERE E.[CommunityId] = @id;

END
