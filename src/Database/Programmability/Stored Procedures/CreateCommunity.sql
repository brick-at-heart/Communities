CREATE PROCEDURE [dbo].[CreateCommunity]
(
	@id						BIGINT OUTPUT,
	@fullName				NVARCHAR(256),
	@joinType				TINYINT,
	@normalizedFullName		NVARCHAR(256),
	@shortName				NVARCHAR(64),
	@normalizedShortName	NVARCHAR(64),
	@slackWorkspaceId		NVARCHAR(256)
)
AS
BEGIN

	INSERT INTO [dbo].[Community]
	(
		[FullName],
		[JoinType],
		[NormalizedFullName],
		[NormalizedShortName],
		[ShortName],
		[SlackWorkspaceId],
		[Created]
	)
	VALUES
	(
		@fullName,
		@joinType,
		@normalizedFullName,
		@normalizedShortName,
		@shortName,
		@slackWorkspaceId,
		GetUtcDate()
	)

	SET @id = SCOPE_IDENTITY();

END