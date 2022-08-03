CREATE PROCEDURE [dbo].[RetrieveEventScheduleByCommunityId]
(
	@id	BIGINT
)
AS
BEGIN

	UPDATE ES
	SET [LastAccess] = GetUtcDate()
	FROM [dbo].[Event] E
		INNER JOIN [dbo].[EventSchedule] ES
			ON E.[Id] = ES.[EventId]
	WHERE E.[CommunityId] = @id;

	SELECT
		ES.[Id],
		ES.[EventId],
		ES.[Start],
		ES.[End]
	FROM [dbo].[Event] E
		INNER JOIN [dbo].[EventSchedule] ES
			ON E.[Id] = ES.[EventId]
	WHERE E.[CommunityId] = @id;

END