CREATE TABLE [dbo].[Catalog]
(
	[Id]					BIGINT			NOT NULL    IDENTITY(1, 1),
	[Name]					NVARCHAR(64)	NOT NULL, 
    [CommunityId]           BIGINT          NOT NULL,
    [Created]               DATETIMEOFFSET  NOT NULL CONSTRAINT [DF_Catalog_Added] DEFAULT GetUtcDate(),
    [Updated]               DATETIMEOFFSET  NULL,
    [LastAccess]            DATETIMEOFFSET  NULL,
    CONSTRAINT [PK_Catalog] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Catalog_Community_CommunityId] FOREIGN KEY ([CommunityId]) REFERENCES [Community]([Id])
);
GO

CREATE UNIQUE INDEX [IX_Catalog_Name] ON [Catalog]([Name]);
GO

CREATE INDEX [IX_Catalog_CommunityId] ON [Catalog]([CommunityId]);
GO