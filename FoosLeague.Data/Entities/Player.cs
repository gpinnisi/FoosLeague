using System.ComponentModel.DataAnnotations.Schema;

namespace FoosLeague.Data.Entities;

[Table("Players")]
public class Player
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
}
