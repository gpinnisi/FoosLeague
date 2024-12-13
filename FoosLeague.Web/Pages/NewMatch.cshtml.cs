using System.ComponentModel.DataAnnotations;
using FoosLeague.Core.Commands.Matches;
using FoosLeague.Data.Contexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FoosLeague.Web.Pages;

public class NewMatchModel(IMediator mediator, ReadContext context) : PageModel
{
    private readonly ReadContext _context = context;
    private readonly IMediator _mediator = mediator;
    public List<SelectListItem> Players { get; set; } = [];

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public async Task<IActionResult> OnGetAsync()
    {
        await LoadOptions();
        return Page();
    }
    private async Task LoadOptions()
    {
        var players = await _context.Players.OrderBy(x => x.Surname).ThenBy(x => x.Name).ToListAsync();
        Players = players.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = $"{x.Name} {x.Surname}" }).ToList();
        Players.Insert(0, new SelectListItem { Value = "", Text = "Select player..", Selected = true });
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            await LoadOptions();
            return Page();
        }
        var guidSet = new HashSet<Guid> { Input.PlayerDefenderTeam1, Input.PlayerForwardTeam1, Input.PlayerDefenderTeam2, Input.PlayerForwardTeam2 };
        if (guidSet.Count != 4)
        {
            ModelState.AddModelError("", "Player should be unique");
            await LoadOptions();
            return Page();
        }
        if (Input.ScoreTeam1 != 10 && Input.ScoreTeam2 != 10)
        {
            ModelState.AddModelError("", "Score not valid");
            await LoadOptions();
            return Page();
        }

        var team1 = new CreateMatch.Team { PlayerDefenderId = Input.PlayerDefenderTeam1, PlayerForwardId = Input.PlayerForwardTeam1, Score = Input.ScoreTeam1 };
        var team2 = new CreateMatch.Team { PlayerDefenderId = Input.PlayerDefenderTeam2, PlayerForwardId = Input.PlayerForwardTeam2, Score = Input.ScoreTeam2 };

        var item = new CreateMatch(team1, team2, "", Input.MatchDateTime);
        var result = await _mediator.Send(item);

        if (result.IsFailed)
        {
            TempData["ErrorMessage"] = result.Fail!.Message;
            return Page();
        }

        TempData["SuccessMessage"] = "Match registered!";

        return RedirectToPage("/Index");
    }

    public class InputModel
    {
        [Required]
        [Display(Name = "Match Date and Time")]
        public DateTime MatchDateTime { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Player Defender Team 1")]
        public Guid PlayerDefenderTeam1 { get; set; }

        [Required]
        [Display(Name = "Player Forward Team 1")]
        public Guid PlayerForwardTeam1 { get; set; }

        [Required]
        [Display(Name = "Score Team 1")]
        public int ScoreTeam1 { get; set; }

        [Required]
        [Display(Name = "Player Defender Team 2")]
        public Guid PlayerDefenderTeam2 { get; set; }

        [Required]
        [Display(Name = "Player Forward Team 2")]
        public Guid PlayerForwardTeam2 { get; set; }

        [Required]
        [Display(Name = "Score Team 2")]
        public int ScoreTeam2 { get; set; }
    }
}
