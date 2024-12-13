CREATE VIEW [dbo].[vwRankedPlayerMatches]
    AS 

WITH RankedPlayers AS
(
    SELECT
        MatchId,
        Role,
        PlayerId,
        ScoreVariation,
        ROW_NUMBER() OVER (PARTITION BY MatchId, Role, CASE WHEN ScoreVariation > 0 THEN 1 ELSE 0 END ORDER BY ScoreVariation DESC) AS Rank,
        CASE 
            WHEN ScoreVariation > 0 THEN 1  
            ELSE 0                          
        END AS IsWinner
    FROM
        dbo.PlayerMatches
)
SELECT MatchId,
		Role,
		PlayerId,
		ScoreVariation,
		IsWinner
FROM RankedPlayers
WHERE Rank = 1

