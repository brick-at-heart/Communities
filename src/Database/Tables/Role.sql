CREATE TABLE [dbo].[Role]
(
	[Id]                        BIGINT          NOT NULL IDENTITY (1, 1), 
    [RoleName]                  NVARCHAR(256)   NOT NULL, 
    [NormalizedRoleName]        NVARCHAR(256)   NOT NULL, 
    [CommunityId]               BIGINT          NOT NULL, 
    [IsCommunityDefault]        BIT             NOT NULL CONSTRAINT [DF_Role_IsCommunityDefault] DEFAULT 0,
    [IsSystemGeneratedOwner]    BIT             NOT NULL CONSTRAINT [DF_Role_IsSystemGeneratedOwner] DEFAULT 0,
    [Created]                   DATETIMEOFFSET  NOT NULL CONSTRAINT [DF_Role_Created] DEFAULT GetUtcDate(), 
    [Updated]                   DATETIMEOFFSET  NULL, 
    [LastAccess]                DATETIMEOFFSET  NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([Id] ASC),
    CONSTRAINT [FK_Role_Community_CommunityId] FOREIGN KEY ([CommunityId]) REFERENCES [Community]([Id])
);
GO

CREATE INDEX [IX_Role_NormalizedRoleName] ON [dbo].[Role]([NormalizedRoleName]);
GO

CREATE INDEX [IX_Role_CommunityId] ON [dbo].[Role](CommunityId);
GO