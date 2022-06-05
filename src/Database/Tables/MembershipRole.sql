CREATE TABLE [dbo].[MembershipRole]
(
	[Id]            BIGINT NOT NULL IDENTITY (1, 1), 
    [MembershipId]  BIGINT NOT NULL, 
    [RoleId]        BIGINT NOT NULL,
    [Created]       DATETIMEOFFSET  NOT NULL CONSTRAINT [DF_MembershipRole_Created] DEFAULT GetUtcDate(), 
    [Updated]       DATETIMEOFFSET  NULL, 
    [LastAccess]    DATETIMEOFFSET  NULL,
    CONSTRAINT [PK_MembershipRole] PRIMARY KEY ([Id] ASC),
    CONSTRAINT [FK_MembershipRole_Membership] FOREIGN KEY ([MembershipId]) REFERENCES [Membership]([Id]),
    CONSTRAINT [FK_MembershipRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [Role]([Id])
);
GO

CREATE INDEX [IX_MembershipRole_RoleId] ON [dbo].[MembershipRole]([RoleId]);
GO

CREATE INDEX [IX_MemebrshipRole_MembershipId] ON [dbo].[MembershipRole]([MembershipId]);
GO