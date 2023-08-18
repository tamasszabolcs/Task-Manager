using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskManager.Pages
{
    public class EditCategoryModel : PageModel
    {
		private readonly TaskManagerContext _context;

		public EditCategoryModel(TaskManagerContext context)
		{
			_context = context;

		}
		[BindProperty]
		public Category UpdateCategory { get; set; }
		public void OnGet(int? id)
        {
			if(id != null)
			{
				var data = (from c in _context.Categories
							where c.CategoryId == id
							select c).SingleOrDefault();
				UpdateCategory = data;
			}
        }

		public IActionResult OnPost()
		{
            var toUpdate = UpdateCategory;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = _context.Users.First(u => string.Equals(u.Username, User.Identity.Name)).Id;
            UpdateCategory.UserId = userId;


            if (_context.Categories.Any(i => i.Name == toUpdate.Name && i.UserId == userId))
            {
                TempData["error"] = "The Category Name is already in use";
                return Page();
            }

            _context.Attach(toUpdate).State = EntityState.Modified;
            TempData["success"] = "The category has been successfully changed";
            _context.SaveChanges();

            return RedirectToPage("/Categories/CategoryList");
        }
    }
}
