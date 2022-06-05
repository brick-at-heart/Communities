CREATE TABLE [dbo].[Community]
(
	[Id]					BIGINT			NOT NULL IDENTITY (1, 1),
	[FullName]				NVARCHAR(256)	NOT NULL,
	[JoinType]			    TINYINT			NOT NULL,
	[NormalizedFullName]	NVARCHAR(256)	NOT NULL,
	[NormalizedShortName]	NVARCHAR(64)	NOT NULL,
	[ShortName]				NVARCHAR(64)	NOT NULL,
	[SlackWorkspaceId]		NVARCHAR(256)	NULL,
    [Created]				DATETIMEOFFSET  NOT NULL CONSTRAINT [DF_Community_Created] DEFAULT GetUtcDate(),
	[Updated]				DATETIMEOFFSET	NULL,
    [LastAccess]			DATETIMEOFFSET  NULL,
	CONSTRAINT [PK_Community] PRIMARY KEY ([Id] ASC),
	CONSTRAINT [FK_Community_JoinType_JoinTypeId] FOREIGN KEY (JoinType) REFERENCES [JoinType] ([Id])
);
GO

CREATE INDEX [IX_Community_NormalizedFullName] ON [dbo].[Community] ([NormalizedFullName]);
GO

CREATE INDEX [IX_Community_NormalizedShortName] ON [dbo].[Community] ([NormalizedShortName]);
GO