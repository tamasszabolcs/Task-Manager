using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TaskManager.Data;
using TaskManager.Models;
using TaskManager.Pages.Categories;

namespace TaskManager.Pages.Statuses;

[Authorize]
public class AddStatusModel : PageModel
{
		private readonly TaskManagerContext _context;

		[BindProperty]
		public Status NewStatus { get; set; }

		public AddStatusModel(TaskManagerContext context)
		{
			_context = context;

		}
    public IActionResult OnPost()
    {
        int nameIdentifier = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        NewStatus.UserId = nameIdentifier;

        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (_context.Statuses.Any(s => s.Name == NewStatus.Name && s.UserId == null))
        {
            TempData["error"] = "The Status Name is already in use";
            return Page();
        }

        if (_context.Statuses.Any(s => s.Name == NewStatus.Name && s.UserId == nameIdentifier))
        {
            TempData["error"] = "The Status Name is already in use";
            return Page();
        }

        _context.Statuses.Add(NewStatus);
        TempData["success"] = "Status added successfully";
        _context.SaveChanges();

        return RedirectToPage("statuslist");
    }

}
