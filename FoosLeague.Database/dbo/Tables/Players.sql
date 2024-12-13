CREATE TABLE [dbo].[Players]
(
	[Id]			   UNIQUEIDENTIFIER	NOT NULL PRIMARY KEY,
	[Name]			   NVARCHAR(100)		NOT NULL,
	[Surname]		   NVARCHAR(100)		NOT NULL,
	[ScoreForward]     INTEGER NOT NULL,
	[ScoreDefender]     INTEGER NOT NULL
)
