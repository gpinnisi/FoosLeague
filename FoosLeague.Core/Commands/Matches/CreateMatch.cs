using FoosLeague.Core.Models.XResults;
using FoosLeague.Core.Models.XResults.Fails;
using MediatR;
using static FoosLeague.Core.Commands.Matches.CreateMatch;

namespace FoosLeague.Core.Commands.Matches;

public record CreateMatch(Team Team1, Team Team2, string Description, DateTime Date) : IRequest<XResult>
{
    public class PlayerAlreadyPresent() : Fail($"Player already present") { }

    public record Team
    {
        public Guid PlayerForwardId { get; set; }
        public Guid PlayerDefenderId { get; set; }
        public int Score { get; set; }
    }
}
