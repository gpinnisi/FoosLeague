CREATE TABLE [dbo].[Matches]
(
	[Id]            UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [DateTime]      DATETIME NOT NULL,
    [Description]   NVARCHAR(500)	 NOT NULL DEFAULT ''
)
