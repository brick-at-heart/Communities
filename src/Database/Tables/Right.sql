CREATE TABLE [dbo].[Right]
(
	[Id]            BIGINT          NOT NULL,
    [RightName]     NVARCHAR(256)   NOT NULL, 
    [Created]       DATETIMEOFFSET  NOT NULL CONSTRAINT [DF_Right_Created] DEFAULT GetUtcDate(),
    [Updated]       DATETIMEOFFSET  NULL, 
    [LastAccess]    DATETIMEOFFSET  NULL,
    CONSTRAINT [PK_Right] PRIMARY KEY ([Id])
)