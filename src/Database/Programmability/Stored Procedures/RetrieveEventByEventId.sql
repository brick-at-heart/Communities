CREATE PROCEDURE [dbo].[RetrieveEventByEventId]
(
	@id	BIGINT
)
AS
BEGIN

	UPDATE [dbo].[Event]
	SET [LastAccess] = GetUtcDate()
	WHERE [Id] = @id;

	SELECT
		E.[Id],
		E.[CommunityId],
		E.[Name],
		E.[Status],
		E.[Description],
		E.[Location]
	FROM [dbo].[Event] E
	WHERE E.[Id] = @id;

END

