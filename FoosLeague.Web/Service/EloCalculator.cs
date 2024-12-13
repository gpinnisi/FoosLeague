using FoosLeague.Data.Entities;

namespace FoosLeague.Web.Service;

public class EloCalculator
{
    private static readonly int K = 30;
    // Function to calculate the Probability
    private static double Probability(int rating1, int rating2)
    {
        // Calculate and return the expected score
        return 1.0 / (1 + Math.Pow(10, (rating1 - rating2) / 400.0));
    }

    // Function to calculate Elo rating
    // K is a constant.
    // outcome determines the outcome: 1 for Player A win, 0 for Player B win, 0.5 for draw.
    private static (int Team1, int Team2) CalculateTeamEloRating(double Team1EloRating, double Team2EloRating, double outcome)
    {
        // Calculate the Winning Probability of Team 2
        var Pb = Probability((int)Team1EloRating, (int)Team2EloRating);

        // Calculate the Winning Probability of Team 1
        var Pa = Probability((int)Team2EloRating, (int)Team1EloRating);

        // Update the Elo Ratings
        Team1EloRating = Team1EloRating + K * (outcome - Pa);
        Team2EloRating = Team2EloRating + K * ((1 - outcome) - Pb);

        return (Convert.ToInt32(Team1EloRating), Convert.ToInt32(Team2EloRating));
    }

    private static int CalculateSinglePlayerElo(int PlayerElo, int TeamEloResult, int TeamEloRating)
    {
        return PlayerElo * TeamEloResult / TeamEloRating;
    }

    public static List<PlayerMatches> CalculatePlayerEloRanking(Team team1, Team team2, Match match)
    {
        var team1EloRating = team1.PlayerForward.ScoreForward + team1.PlayerDefender.ScoreDefender;
        var team2EloRating = team2.PlayerForward.ScoreForward + team2.PlayerDefender.ScoreDefender;
        var (Team1Result, Team2Result) = CalculateTeamEloRating(team1EloRating, team2EloRating, (team1.Score > team2.Score ? 1 : 0));

        var player1ForwardMatch = new PlayerMatches()
        {
            PlayerId = team1.PlayerForward.Id,
            MatchId = match.Id,
            ScoreVariation = CalculateSinglePlayerElo((team1EloRating - team1.PlayerDefender.ScoreDefender),
                                                      Team1Result,
                                                      team1EloRating),
            Role = Role.Forward
        };
        var player1DefenderMatch = new PlayerMatches()
        {
            PlayerId = team1.PlayerDefender.Id,
            MatchId = match.Id,
            ScoreVariation = CalculateSinglePlayerElo((team1EloRating - team1.PlayerForward.ScoreForward),
                                                      Team1Result,
                                                      team1EloRating),
            Role = Role.Defender
        };
        var player2ForwardMatch = new PlayerMatches()
        {
            PlayerId = team2.PlayerForward.Id,
            MatchId = match.Id,
            ScoreVariation = CalculateSinglePlayerElo((team2EloRating - team2.PlayerDefender.ScoreDefender),
                                                      Team2Result,
                                                      team2EloRating),
            Role = Role.Forward
        };
        var player2DefenderMatch = new PlayerMatches()
        {
            PlayerId = team2.PlayerDefender.Id,
            MatchId = match.Id,
            ScoreVariation = CalculateSinglePlayerElo((team2EloRating - team2.PlayerForward.ScoreForward),
                                                      Team2Result,
                                                      team2EloRating),
            Role = Role.Defender
        };

        return [player1DefenderMatch, player1ForwardMatch, player2DefenderMatch, player2ForwardMatch];
    }

    public record Team(Player PlayerForward, Player PlayerDefender, int Score);
}
