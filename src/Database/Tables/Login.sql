CREATE TABLE [dbo].[Login]
(
	[ProviderId]            NVARCHAR(100)   NOT NULL,
    [ProviderKey]           NVARCHAR(100)   NOT NULL,
    [ProviderDisplayName]   NVARCHAR(100)   NOT NULL,
    [UserId]                BIGINT          NOT NULL,
    [Created]               DATETIMEOFFSET  NOT NULL CONSTRAINT [DF_Login_Created] DEFAULT GetUtcDate(),
    [LastAccess]            DATETIMEOFFSET  NULL,
    CONSTRAINT [PK_Login] PRIMARY KEY ([ProviderId] ASC, [ProviderKey] ASC),
    CONSTRAINT [FK_Login_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id])
);
GO

CREATE INDEX [IX_Login_UserId] ON [dbo].[Login] ([UserId]);
GO