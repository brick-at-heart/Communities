CREATE TABLE [dbo].[RoleRight]
(
	[Id]            BIGINT          NOT NULL IDENTITY(1, 1),
    [RoleId]        BIGINT          NOT NULL, 
    [RightId]       BIGINT          NOT NULL, 
    [State]         BIT             NULL, 
    [Created]       DATETIMEOFFSET  NOT NULL CONSTRAINT [DF_RoleRight_Created] DEFAULT GetUtcDate(), 
    [Updated]       DATETIMEOFFSET  NULL, 
    [LastAccess]    DATETIMEOFFSET  NULL,
    CONSTRAINT [PK_RoleRight] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoleRight_Role] FOREIGN KEY ([RoleId]) REFERENCES [Role]([Id]),
    CONSTRAINT [FK_RoleRight_Right] FOREIGN KEY ([RightId]) REFERENCES [Right]([Id])
);
GO

CREATE INDEX [IX_RoleRight_RoleId] ON [dbo].[RoleRight](RoleId);
GO

CREATE INDEX [IX_RoleRight_RightId] ON [dbo].[RoleRight](RightId);
GO