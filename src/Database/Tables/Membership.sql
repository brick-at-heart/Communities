CREATE TABLE [dbo].[Membership]
(
	[Id]                BIGINT          NOT NULL IDENTITY(1, 1),
    [UserId]            BIGINT          NOT NULL, 
    [CommunityId]       BIGINT          NOT NULL, 
    [IsActive]          BIT             NOT NULL CONSTRAINT [DF_Membership_IsActive] DEFAULT 1, 
    [IsCurrent]         BIT             NOT NULL CONSTRAINT [DF_Membership_IsCurrent] DEFAULT 0, 
    [IsPrimary]         BIT             NOT NULL CONSTRAINT [DF_Membership_IsPrimary] DEFAULT 0,
    [SlackMemberId]     NVARCHAR(256)   NULL, 
    [Created]           DATETIMEOFFSET  NOT NULL CONSTRAINT [DF_Membership_Created] DEFAULT GetUtcDate(), 
    [Updated]           DATETIMEOFFSET  NULL, 
    [LastAccess]        DATETIMEOFFSET  NULL,
	CONSTRAINT [PK_Membership] PRIMARY KEY ([Id] ASC),
    CONSTRAINT [FK_Membership_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]),
	CONSTRAINT [FK_Membership_Community_CommunityId] FOREIGN KEY ([CommunityId]) REFERENCES [Community] ([Id])
);
GO

CREATE INDEX [IX_Membership_UserId] ON [dbo].[Membership] ([UserId]);
GO

CREATE INDEX [IX_Membership_CommunityId] ON [dbo].[Membership] ([CommunityId]);
GO

CREATE INDEX [IX_Membership_SlackMemberId] ON [dbo].[Membership] ([SlackMemberId]);
GO
