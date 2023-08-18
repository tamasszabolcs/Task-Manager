using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.Statuses;
[Authorize]
public class StatusListModel : PageModel
{
		private readonly TaskManagerContext _context;
		public List<Status> Statuses { get; set; }

		public StatusListModel(TaskManagerContext context)
		{
			_context = context;

		}
		public void OnGet()
    {
        if (User.Identity.IsAuthenticated)
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var data = (from c in _context.Statuses
                        select c).Where(x => x.UserId == int.Parse(nameIdentifier) || x.UserId == null).ToList();
            Statuses = data.OrderBy(x => x.StatusId).ThenBy(x => x.UserId != null).ToList();
        }
    }

    public IActionResult OnGetDelete(int? id)
    {
        if (id != null)
        {
            var data = (from status in _context.Statuses
                        select status).ToList();
            Statuses = data;
            var toRemove = (from status in Statuses
                            where status.StatusId == id
                            select status).SingleOrDefault();

            _context.Statuses.Remove(toRemove);
            _context.SaveChanges();

        }
        return RedirectToPage("StatusList");
    }

}

