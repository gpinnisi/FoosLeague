using FoosLeague.Data.Contexts;
using FoosLeague.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FoosLeague.Web.Pages;

public class CreditsModel(ReadContext context) : PageModel
{
    private readonly ReadContext _context = context;

    public List<RankingItemViewModel> Ranking { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        Ranking = await _context.Players.OrderBy(x => (decimal)(x.ScoreForward + x.ScoreDefender) / 2).Select(x => new RankingItemViewModel
        {
            Name = x.Name,
            Surname = x.Surname,
            Score = (int)Math.Round((decimal)(x.ScoreForward + x.ScoreDefender) / 2)
        }).ToListAsync();

        int position = 1;
        Ranking.ForEach(x => x.Position = position++);

        return Page();
    }
}
