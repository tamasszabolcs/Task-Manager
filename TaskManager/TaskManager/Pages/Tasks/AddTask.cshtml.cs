using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.Tasks
{
    [Authorize]
    public class AddTaskModel : PageModel
    {

        private readonly TaskManagerContext _context;
        public AddTaskModel(TaskManagerContext context)
        {
            _context = context;

        }
        [BindProperty]
        public Task1 NewTask { get; set; }
        public List<Task1> Tasks { get; set; } = new List<Task1>();
        public List<SelectListItem> CategoryOptions { get; set; }
        public List<SelectListItem> StatusOptions { get; set; }
        //public List<Category> Categories { get; set; } = new List<Category>();
        //public List<Status> Statuses { get; set; } = new List<Status>();
       

       
        public void OnGet()
        {
            int nameIdentifier = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            CategoryOptions = _context.Categories
                .OrderBy(c => c.CategoryId)
                .Where(c => c.UserId == nameIdentifier || c.UserId == null)
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                })
                .ToList();

            StatusOptions = _context.Statuses
                .OrderBy(s => s.StatusId)
                .Where(s => s.UserId == nameIdentifier || s.UserId == null)
                .Select(s => new SelectListItem
                {
                    Value = s.StatusId.ToString(),
                    Text = s.Name
                })
                .ToList();

            Tasks = _context.Tasks
                .Where(t => t.UserId == nameIdentifier)
                .ToList();
            // set default due date to today
            NewTask = new Task1 { DueDate = DateTime.Today };
        }



        public IActionResult OnPost()
        {
            if (NewTask == null)
            {
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            int nameIdentifier = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            NewTask.UserId = nameIdentifier;

            _context.Tasks.Add(NewTask);
            try
            {
                _context.SaveChanges();
                TempData["success"] = "Task added succesfully";
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

    }
}