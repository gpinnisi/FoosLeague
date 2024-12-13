using System.ComponentModel.DataAnnotations;
using FoosLeague.Core.Commands.Players;
using FoosLeague.Data.Contexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoosLeague.Web.Pages;

public class NewPlayerModel(IMediator mediator, ReadContext context) : PageModel
{
    private readonly ReadContext _context = context;
    private readonly IMediator _mediator = mediator;

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var item = new CreatePlayer(Input.Name, Input.Surname);

        var result = await _mediator.Send(item);

        if (result.IsFailed)
        {
            TempData["ErrorMessage"] = result.Fail!.Message;
            return Page();
        }

        TempData["SuccessMessage"] = "Player created!";

        return RedirectToPage("/Index");
    }

    public class InputModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; } = string.Empty;
    }
}
