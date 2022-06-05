CREATE TABLE [dbo].[JoinType]
(
	[Id]			TINYINT NOT NULL IDENTITY (1, 1),
	[SearchType]	TINYINT NOT NULL,
	[AssignedType]	TINYINT NOT NULL,
	CONSTRAINT [PK_JoinType] PRIMARY KEY ([Id] ASC)
)
GO

CREATE INDEX [IX_JoinType_SearchType] ON [dbo].[JoinType] ([SearchType]);
GO

CREATE INDEX [IX_JoinType_AssignedType] ON [dbo].[JoinType] ([AssignedType]);
GO