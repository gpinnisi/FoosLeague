using System.ComponentModel.DataAnnotations.Schema;
using FoosLeague.Data.Entities;

namespace FoosLeague.Data.Views;

[Table("vwRankedPlayerMatches")]
public class RankedPlayerMatchesView
{
    public Guid MatchId { get; set; }
    public Guid PlayerId { get; set; }  
    public Role Role { get; set; }
    public bool IsWinner { get; set; }

    public Match? Match { get; set; }
    public Player? Player { get; set; }
}
