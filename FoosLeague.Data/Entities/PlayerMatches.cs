using System.ComponentModel.DataAnnotations.Schema;

namespace FoosLeague.Data.Entities;

public class PlayerMatches
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PlayerId { get; set; } = Guid.NewGuid();
    public Guid MatchId { get; set; } = Guid.NewGuid();
    public int ScoreVariation { get; set; } = 0;
    [Column(TypeName = "nvarchar(50)")]
    public Role Role { get; set; } = Role.Forward;

    public Player? Player { get; set; }
    public Match? Match { get; set; }
}
