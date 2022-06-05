CREATE PROCEDURE [dbo].[UpdateMembership]
(
	@id				BIGINT,
	@isActive		BIT,
	@isCurrent		BIT,
	@isPrimary		BIT,
	@slackMemberId	NVARCHAR(256)
)
AS
BEGIN

	DECLARE @TRUE	BIT	= 1;
	DECLARE @FALSE	BIT = 0;

	DECLARE @User TABLE
	(
		Id BIGINT
	)

	UPDATE [dbo].[Membership]
	SET [IsActive] = @isActive,
		[IsCurrent] = @isCurrent,
		[IsPrimary] = @isPrimary,
		[SlackMemberId] = @slackMemberId,
		[Updated] = GetUtcDate()
	OUTPUT inserted.[UserId] INTO @User
	WHERE [Id] = @id;

	IF (@isPrimary = @TRUE AND @isActive = @TRUE)
		BEGIN

			UPDATE M
			SET [IsPrimary] = @FALSE
			FROM [dbo].[Membership] M
				INNER JOIN @User U
					ON M.[UserId] = U.[Id]
			WHERE M.[Id] <> @id;

		END

	IF (@isCurrent = @TRUE AND @isActive = @TRUE)
		BEGIN

			UPDATE M
			SET [IsCurrent] = @FALSE
			FROM [dbo].[Membership] M
				INNER JOIN @User U
					ON M.[UserId] = U.[Id]
			WHERE M.[Id] <> @id;

		END

	IF (@isActive = @FALSE)
		BEGIN

			UPDATE M
			SET [IsCurrent] = @FALSE,
				[IsPrimary] = @FALSE
			FROM [dbo].[Membership] M
			WHERE [Id] = @id;

			-- If the primary membership was de-activated, set the earliest membership as primary.
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

			-- If the current membership was de-activated, set the earliest membership as current.
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

END