CREATE TABLE [dbo].[PlayerMatches]
(
    [Id]                 UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [PlayerId]           UNIQUEIDENTIFIER NOT NULL,
    [MatchId]            UNIQUEIDENTIFIER NOT NULL,
    [ScoreVariation]     INTEGER NOT NULL,
    [Role]               NVARCHAR(50) NOT NULL
 
    CONSTRAINT [fk_PlayerMatches_PlayerId] FOREIGN KEY ([PlayerId]) REFERENCES [dbo].[Players] ([Id])
    CONSTRAINT [fk_PlayerMatches_MatchId] FOREIGN KEY ([MatchId]) REFERENCES [dbo].[Matches] ([Id])
)
