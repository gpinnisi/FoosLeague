namespace FoosLeague.Data.Entities;
public class Match
{
    public required Guid Id { get; set; }
    public required DateTime DateTime { get; set; } 
    public required string Description { get; set; } = string.Empty;

    public IEnumerable<PlayerMatches>? PlayerMatches { get; set; }
}
