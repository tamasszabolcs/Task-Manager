using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.Categories
{
    [Authorize]
    public class CategoryListModel : PageModel
    {
		private readonly TaskManagerContext _context;
		public List<Category> Categories { get; set; }

		public CategoryListModel(TaskManagerContext context)
		{
			_context = context;

		}
		public void OnGet()
        {
			if (User.Identity.IsAuthenticated)
			{

				var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				var data = (from c in _context.Categories
							select c).Where(x => x.UserId == int.Parse(nameIdentifier) || x.UserId == null).ToList();
				Categories = data.OrderBy(x => x.CategoryId).ThenBy(x => x.UserId != null).ToList();
			}
            
        }

		public IActionResult OnGetDelete(int? id)
		{
			if(id != null)
			{
				var data = (from c in _context.Categories
							select c).ToList();
				Categories = data;
				var toRemove = (from c in Categories
							where c.CategoryId == id
							select c).SingleOrDefault();
				if(toRemove != null) {
                    _context.Categories.Remove(toRemove);
                    _context.SaveChanges();
                }	
				
			}
			return RedirectToPage("CategoryList");
		}
    }
}
