using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.Statuses
{
    public class EditStatusModel : PageModel
    {
        [BindProperty]
        public Status ToUpdate { get; set; }
        private readonly TaskManagerContext _context;

        public EditStatusModel(TaskManagerContext context)
        {
            _context = context;
        }
        public void OnGet(int? id)
        {
            if (id != null)
            {
                var data = (from status in _context.Statuses
                            where status.StatusId == id
                            select status).SingleOrDefault();
                ToUpdate = data;
            }
        }

        public IActionResult OnPost()
        {
            var toUpdate = ToUpdate;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = _context.Users.First(u => string.Equals(u.Username, User.Identity.Name)).Id;
            ToUpdate.UserId = userId;

            if (_context.Statuses.Any(s => s.Name == toUpdate.Name && s.UserId == null))
            {
                TempData["error"] = "The Status Name is already in use";
                return Page();
            }

            if (_context.Statuses.Any(s => s.Name == toUpdate.Name && s.UserId == userId))
            {
                TempData["error"] = "The Status Name is already in use";
                return Page();
            }
            _context.Attach(toUpdate).State = EntityState.Modified;
            TempData["success"] = "The status has been successfully changed";
            _context.SaveChanges();

            return RedirectToPage("StatusList");
        }
    }
}
