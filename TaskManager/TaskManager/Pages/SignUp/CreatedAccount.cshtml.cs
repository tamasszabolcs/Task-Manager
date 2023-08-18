using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TaskManager.Pages.SignUp;

public class CreatedAccountModel : PageModel
{ 
    public IActionResult OnGet()
    {
        if (User.Identity.IsAuthenticated)
            return RedirectToPage("/Index");
        else return Page();
    }
}
