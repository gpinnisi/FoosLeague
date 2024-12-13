using System.ComponentModel.DataAnnotations.Schema;

namespace FoosLeague.Data.Entities;

[Table("Players")]
public class Player
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public int ScoreForward { get; set; } = 0;
    public int ScoreDefender { get; set; } = 0;

    public int GetScoreByRole(Role role)
    {
        return role switch
        {
            Role.Forward => ScoreForward,
            Role.Defender => ScoreDefender,
            _ => 0,
        };
    }
}
