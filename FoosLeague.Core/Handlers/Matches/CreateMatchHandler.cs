using FoosLeague.Core.Commands.Matches;
using FoosLeague.Core.Models.XResults;
using FoosLeague.Data.Contexts;
using FoosLeague.Data.Entities;
using FoosLeague.Web.Service;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static FoosLeague.Web.Service.EloCalculator;

namespace FoosLeague.Core.Handlers.Matches;

public class CreateMatchHandler(WriteContext context) : IRequestHandler<CreateMatch, XResult>
{
    private readonly WriteContext context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<XResult> Handle(CreateMatch command, CancellationToken cancellationToken)
    {
        var match = new Match { Id = Guid.NewGuid(), DateTime = command.Date, Description = command.Description };
        context.Matches.Add(match);

        var team1 = await MapTeam(command.Team1);
        var team2 = await MapTeam(command.Team2);

        var res = EloCalculator.CalculatePlayerEloRanking(team1, team2, match);
        foreach ( var item in res)
        {
            context.PlayerMatches.Add(item);
        }

        team1.PlayerDefender.ScoreDefender += res.Single(r => r.Player!.Id == team1.PlayerDefender.Id).ScoreVariation;
        team1.PlayerForward.ScoreForward += res.Single(r => r.Player!.Id == team1.PlayerForward.Id).ScoreVariation;
        team2.PlayerDefender.ScoreDefender += res.Single(r => r.Player!.Id == team2.PlayerDefender.Id).ScoreVariation;
        team2.PlayerForward.ScoreForward += res.Single(r => r.Player!.Id == team2.PlayerForward.Id).ScoreVariation;

        await context.SaveChangesAsync(cancellationToken);

        return XResult.Success;
    }

    private async Task<Team> MapTeam(CreateMatch.Team team)
    {
        var playerForward = await context.Players.SingleAsync(p => p.Id == team.PlayerForwardId);
        var playerDefender = await context.Players.SingleAsync(p => p.Id == team.PlayerDefenderId);

        return new Team(PlayerDefender: playerDefender, PlayerForward: playerForward, Score: team.Score);
    }
}

