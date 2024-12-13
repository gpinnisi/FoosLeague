namespace FoosLeague.Data.Entities;
public class Match
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime DateTime { get; set; } = DateTime.Now;
    public string Description { get; set; } = string.Empty;

    public IEnumerable<PlayerMatches>? PlayerMatches { get; set; }
}
