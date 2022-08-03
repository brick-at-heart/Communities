CREATE TABLE [dbo].[EventSchedule]
(
	[Id]			BIGINT			NOT NULL IDENTITY (1, 1),
	[EventId]		BIGINT			NOT NULL,
	[Start]			DATETIMEOFFSET	NOT NULL,
	[End]			DATETIMEOFFSET	NOT NULL,
	[Created]       DATETIMEOFFSET  NOT NULL CONSTRAINT [DF_EventSchedule_Created] DEFAULT GetUtcDate(),
	[Updated]		DATETIMEOFFSET	NULL,
    [LastAccess]    DATETIMEOFFSET  NULL,
	CONSTRAINT [PK_EventSchedule] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_EventSchedule_Event] FOREIGN KEY ([EventId]) REFERENCES [Event]([Id]),
	CONSTRAINT [CK_StartBeforeEnd] CHECK ([Start] <= [End])
)
