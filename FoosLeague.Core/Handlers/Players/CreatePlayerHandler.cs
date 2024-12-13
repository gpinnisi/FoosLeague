using FoosLeague.Core.Commands.Players;
using FoosLeague.Core.Models.XResults;
using FoosLeague.Data.Contexts;
using FoosLeague.Data.Entities;
using MediatR;
using static FoosLeague.Core.Commands.Players.CreatePlayer;

namespace FoosLeague.Core.Handlers.Players;

public class CreatePlayerHandler(WriteContext context) : IRequestHandler<CreatePlayer, XResult<Guid>>
{
    private readonly WriteContext context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<XResult<Guid>> Handle(CreatePlayer command, CancellationToken cancellationToken)
    {
        if (context.Players.Any(x => x.Name == command.Name && x.Surname == command.Surname)) { return new PlayerAlreadyPresent(); }

        var item = new Player
        {
            Name = command.Name,
            Surname = command.Surname
        };

        context.Players.Add(item);

        await context.SaveChangesAsync(cancellationToken);

        return item.Id;
    }
}

