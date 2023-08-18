using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages
{
    public class CardViewModel : PageModel
    {
        private readonly ILogger<CardViewModel> _logger;

        private readonly TaskManagerContext _context;

        public CardViewModel(ILogger<CardViewModel> logger, TaskManagerContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<Task1> Tasks { get; set; }
        public List<Category> Categories { get; set; }
        public List<Status> Statuses { get; set; }

        public IActionResult OnGet()
        {
       

            if (User.Identity.IsAuthenticated)
            {
                int nameIdentifier = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                Tasks = _context.Tasks.Where(x => x.UserId == nameIdentifier).OrderBy(x => x.DueDate).ToList();
                Categories = _context.Categories.Where(x => x.UserId == nameIdentifier || x.UserId == null).OrderBy(x => x.CategoryId).ToList();
                Statuses = _context.Statuses.Where(x => x.UserId == nameIdentifier || x.UserId == null).OrderBy(x => x.StatusId).ToList();

                return Page();
            }

            else return RedirectToPage("/Login/Login");



        }
        public IActionResult OnGetDelete(int? id)
        {
            if (id != null)
            {
                var data = (from t in _context.Tasks
                            select t).ToList();
                Tasks = data;
                var toRemove = (from t in Tasks
                                where t.Id == id
                                select t).SingleOrDefault();

                _context.Tasks.Remove(toRemove);
                _context.SaveChanges();

            }
            return RedirectToPage("/CardView");
        }
    }
}
