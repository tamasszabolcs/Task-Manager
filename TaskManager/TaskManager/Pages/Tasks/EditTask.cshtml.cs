using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TaskManager.Pages.Tasks
{
    [Authorize]
    public class EditTaskModel : PageModel
    {

        private readonly TaskManagerContext _context;

        public EditTaskModel(TaskManagerContext context)
        {
            _context = context;

        }
        [BindProperty]
        public Task1 TaskToUpdate { get; set; }
        [BindProperty]
        public int Progress { get; set; }
        public List<SelectListItem> CategoryOptions { get; set; }
        public List<SelectListItem> StatusOptions { get; set; }



        public void OnGet(int? id)
        {
            if (id != null)
            {
                var data = (from task in _context.Tasks
                            where task.Id == id
                            select task).SingleOrDefault();
                TaskToUpdate = data;

                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                CategoryOptions = _context.Categories
                                .Where(c => c.UserId == userId)
                                .Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.CategoryId.ToString(),
                                      Text = a.Name
                                  }).ToList();

                StatusOptions = _context.Statuses
                                .Where(s => s.UserId == userId ||
                                            s.Name == "Unstarted" ||
                                            s.Name == "In Progress" ||
                                            s.Name == "Finished")
                                .Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.StatusId.ToString(),
                                      Text = a.Name
                                  }).ToList();
            }
        }


        public IActionResult OnPost()

        {
                try
            {
                // get the original Task object from the database without changing something
                var original = _context.Tasks.AsNoTracking().FirstOrDefault(t => t.Id == TaskToUpdate.Id);

                if (original == null)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    int nameIdentifier = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    TaskToUpdate.UserId = nameIdentifier;

                    _context.Entry(TaskToUpdate).State = EntityState.Modified;

                                    
                   

                    if (!_context.Tasks.Any(t => t.Id == TaskToUpdate.Id &&
                                            t.Name == TaskToUpdate.Name &&
                                            t.Description == TaskToUpdate.Description &&
                                            t.CategoryId == TaskToUpdate.CategoryId &&
                                            t.Progress == TaskToUpdate.Progress &&
                                            t.StatusId == TaskToUpdate.StatusId &&
                                            t.DueDate == TaskToUpdate.DueDate &&
                                            t.UserId == TaskToUpdate.UserId
                                            ))
                    {
                        _context.SaveChanges();
                        TempData["success"] = "The task has been successfully changed";
                    }
                }

                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                // log the exception here, for example:
                Console.WriteLine(ex.Message);
                throw;
            }
        }





    }
}