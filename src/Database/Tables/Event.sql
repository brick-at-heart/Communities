CREATE TABLE [dbo].[Event]
(
	[Id]                    BIGINT          NOT NULL IDENTITY (1, 1),
	[CommunityId]			BIGINT			NOT NULL,
	[Name]					NVARCHAR(256)	NOT NULL,
	[Status]				TINYINT			NOT NULL CONSTRAINT [DF_Event_Status] DEFAULT 1,
	[Description]			NVARCHAR(2048)	NULL,
	[Location]				NVARCHAR(512)	NULL,
	[Created]               DATETIMEOFFSET  NOT NULL CONSTRAINT [DF_Event_Created] DEFAULT GetUtcDate(),
	[Updated]			    DATETIMEOFFSET	NULL,
    [LastAccess]            DATETIMEOFFSET  NULL,
    CONSTRAINT [PK_Event] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Event_Community] FOREIGN KEY ([CommunityId]) REFERENCES [Community]([Id]),
	CONSTRAINT [FK_Event_Status] FOREIGN KEY ([Status]) REFERENCES [EventStatus]([Id])
);
