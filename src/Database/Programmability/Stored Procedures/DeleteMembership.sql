CREATE PROCEDURE [dbo].[DeleteMembership]
(
	@id	BIGINT
)
AS
BEGIN

	DECLARE @TRUE	BIT	= 1;

	DELETE
	FROM [dbo].[Membership]
	WHERE [Id] = @id;

	-- If the primary membership was deleted, set the earliest membership as primary.
	UPDATE M
	SET [IsPrimary] = @TRUE,
		[Updated] = GetUtcDate()
	FROM [dbo].[Membership] M
		INNER JOIN (SELECT ME.[UserId], Min(ME.[Created]) AS 'Created'
					FROM [dbo].[Membership] ME
					WHERE ME.[UserId] IN (SELECT [UserId]
										  FROM [dbo].[Membership] MP
										  GROUP BY MP.[UserId]
										  HAVING Max(Cast(MP.[IsPrimary] AS INT)) < 1)
					GROUP BY ME.[UserId]) X
			ON M.[UserId] = X.[UserId] AND M.[Created] = X.[Created];

	-- If the current membership was deleted, set the earliest membership as current.
	UPDATE M
	SET [IsCurrent] = @TRUE,
		[Updated] = GetUtcDate()
	FROM [dbo].[Membership] M
		INNER JOIN (SELECT ME.[UserId], Min(ME.[Created]) AS 'Created'
					FROM [dbo].[Membership] ME
					WHERE ME.[UserId] IN (SELECT [UserId]
										  FROM [dbo].[Membership] MP
										  GROUP BY MP.[UserId]
										  HAVING Max(Cast(MP.[IsCurrent] AS INT)) < 1)
					GROUP BY ME.[UserId]) X
			ON M.[UserId] = X.[UserId] AND M.[Created] = X.[Created];

END