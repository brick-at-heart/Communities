CREATE TABLE [dbo].[User]
(
	[Id]                    BIGINT          NOT NULL IDENTITY (1, 1),
    [IsActive]              BIT             NOT NULL CONSTRAINT [DF_User_IsActive] DEFAULT 1,
    [IsApproved]            BIT             NOT NULL CONSTRAINT [DF_User_IsApproved] DEFAULT 0,
    [DisplayName]           NVARCHAR(100)   NOT NULL,
    [Email]                 NVARCHAR(320)   NOT NULL, 
    [NormalizedEmail]       NVARCHAR(320)   NOT NULL,
    [EmailConfirmed]        BIT             NOT NULL CONSTRAINT [DF_User_EmailConfirmed] DEFAULT 0,
    [GivenName]             NVARCHAR(100)   NULL,
    [SurName]               NVARCHAR(100)   NULL,
    [PhoneNumber]           NVARCHAR(24)    NULL,
    [PhoneNumberConfirmed]  BIT             NOT NULL CONSTRAINT [DF_User_PhoneNumberConfirmed] DEFAULT 0,
    [StreetAddressLine1]    NVARCHAR(100)   NULL,
    [StreetAddressLine2]    NVARCHAR(100)   NULL,
    [City]                  NVARCHAR(100)   NULL,
    [Region]                NVARCHAR(100)   NULL,
    [Country]               NVARCHAR(100)   NULL,
    [PostalCode]            NVARCHAR(100)   NULL,
    [DateOfBirth]           DATE            NOT NULL,
    [Created]               DATETIMEOFFSET  NOT NULL CONSTRAINT [DF_User_Created] DEFAULT GetUtcDate(),
	[Updated]			    DATETIMEOFFSET	NULL,
    [LastAccess]            DATETIMEOFFSET  NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_User_NormalizedEmail] ON [dbo].[User] ([NormalizedEmail]);
GO