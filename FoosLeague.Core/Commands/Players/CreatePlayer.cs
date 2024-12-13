using FoosLeague.Core.Models.XResults;
using FoosLeague.Core.Models.XResults.Fails;
using MediatR;

namespace FoosLeague.Core.Commands.Players;

public record CreatePlayer(string Name, string Surname) : IRequest<XResult<Guid>>
{
    public class PlayerAlreadyPresent() : Fail($"Player already present") { }
}

