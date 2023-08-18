using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly TaskManagerContext _context;

        public List<SelectListItem> StatusOptions { get; set; }

        public List<SelectListItem> CategoryOptions { get; set; }

        public IndexModel(ILogger<IndexModel> logger, TaskManagerContext context)
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
                Tasks = _context.Tasks.Where(x => x.UserId == nameIdentifier).ToList();               
                Categories = _context.Categories.Where(x => x.UserId == nameIdentifier || x.UserId == null).OrderBy(x => x.CategoryId).ToList();
                Statuses = _context.Statuses.Where(x => x.UserId == nameIdentifier || x.UserId == null).OrderBy(x => x.StatusId).ToList();
                
<<<<<<< HEAD

                 StatusOptions = _context.Statuses.Select(a =>
                                 new SelectListItem
                                 {
                                     Value = a.StatusId.ToString(),
                                     Text = a.Name
                                 }).ToList();

                 CategoryOptions = _context.Categories.Select(a =>
                                 new SelectListItem
                                 {
                                     Value = a.CategoryId.ToString(),
                                     Text = a.Name
                                 }).ToList();
=======
>>>>>>> ef53d2af4683fa00f929d8ac8840a07f62f8463e
                
                return Page();
            }

            else return RedirectToPage("/Login/Login");

        
        }

         public IActionResult OnPost(Task1 task)
        {
            // get the original Task object from the database without changing something
            var original = _context.Tasks.First(t => t.Id == task.Id);

            if (original == null)
            {
                return NotFound();
            }

            original.StatusId = task.StatusId;
            _context.SaveChanges();
            TempData["success"] = "The task has been successfully changed";


            return RedirectToPage("/Index");
        }

<<<<<<< HEAD

=======
>>>>>>> ef53d2af4683fa00f929d8ac8840a07f62f8463e
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
            return RedirectToPage("/Index");
        }
    }
}