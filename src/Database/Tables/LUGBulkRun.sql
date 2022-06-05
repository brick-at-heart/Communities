CREATE TABLE [dbo].[LUGBulkRun]
(
	[Id]                    BIGINT          NOT NULL    IDENTITY(1, 1),
    [Name]                  NVARCHAR(64)    NOT NULL, 
    [NormalizedName]        NVARCHAR(64)    NOT NULL, 
    [CatalogId]             BIGINT          NOT NULL, 
    [CommunityId]           BIGINT          NOT NULL, 
    [EurosPerPersonMaximum] MONEY           NOT NULL, 
    [MinimumEurosRequired]  MONEY           NOT NULL,
    [Created]               DATETIMEOFFSET  NOT NULL CONSTRAINT [DF_LUGBulkRun_Added] DEFAULT GetUtcDate(),
    [Updated]               DATETIMEOFFSET  NULL,
    [LastAccess]            DATETIMEOFFSET  NULL,
    CONSTRAINT [PK_LUGBulkRun] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LUGBulkRun_Catalog_CatalogId] FOREIGN KEY (CatalogId) REFERENCES [Catalog]([Id]),
    CONSTRAINT [FK_LUGBulkRun_Community_CommunityId] FOREIGN KEY (CommunityId) REFERENCES [Community]([Id])
);
GO

CREATE UNIQUE INDEX [IX_LUGBulkRun_NormalizedName] ON [LUGBulkRun]([NormalizedName]);
GO

CREATE INDEX [IX_LUGBulkRun_CatalogId] ON [LUGBulkRun]([CatalogId]);
GO

CREATE INDEX [IX_LUGBulkRun_Community_CommunityId] ON [LUGBulkRun]([CommunityId]);
GO