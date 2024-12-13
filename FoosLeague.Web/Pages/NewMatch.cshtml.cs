using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoosLeague.Web.Pages;

public class NewMatchModel : PageModel
{
    public IActionResult OnGet()
    {
        return Page();
    }
}
