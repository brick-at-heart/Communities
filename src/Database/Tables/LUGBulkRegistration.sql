CREATE TABLE [dbo].[LUGBulkRegistration]
(
	[Id]            BIGINT          NOT NULL,
	[LUGBulkRunId]  BIGINT          NOT NULL, 
    [MembershipId]  BIGINT          NOT NULL, 
    [GivenName]     NVARCHAR(50)    NOT NULL, 
    [SurName]       NVARCHAR(50)    NOT NULL, 
    [EmailAddress]  NVARCHAR(320)   NOT NULL, 
    [DateOfBirth]   DATE            NOT NULL, 
    [DateofConsent] DATE            NOT NULL,
    [Created]       DATETIMEOFFSET  NOT NULL CONSTRAINT [DF_LUGBulkRegistration_Added] DEFAULT GetUtcDate(),
    [Updated]       DATETIMEOFFSET  NULL,
    [LastAccess]    DATETIMEOFFSET  NULL,
    CONSTRAINT [PK_LUGBulkRegistration] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LUGBulkRegistration_LUGBulkRun_LUGBulkRunId] FOREIGN KEY ([LUGBulkRunId]) REFERENCES [LUGBulkRun]([Id]),
    CONSTRAINT [FK_LUGBulkRegistration_Membership_MembershipId] FOREIGN KEY ([MembershipId]) REFERENCES [Membership]([Id])
);
GO

CREATE INDEX [IX_LUGBulkRegistration_LUGBulkRunId] ON [LUGBulkRegistration]([LUGBulkRunId]);
GO

CREATE INDEX [IX_LUGBulkRegistration_MembershipId] ON [LUGBulkRegistration]([MembershipId]);
GO