using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.Categories;
public class AddCategoryModel2
{
    public string Name { get; set; }
    public string Description { get; set; }
}
[Authorize]
public class AddCategoryModel : PageModel
{
    private readonly TaskManager.Data.TaskManagerContext _context;
   

    public AddCategoryModel(TaskManager.Data.TaskManagerContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();

    }

    [BindProperty]
    public AddCategoryModel2 AddCategoryModel2 { get; set; }


    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    
    public async Task<IActionResult> OnPostAsync()
    {


        int nameIdentifier = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);


        var category = new Category { Name = AddCategoryModel2.Name, Description = AddCategoryModel2.Description, UserId = nameIdentifier };
        

        if (!ModelState.IsValid || _context.Categories == null || AddCategoryModel2 == null)
        {
            return Page();
        }
        if (string.Equals(category.Name, category.Description, StringComparison.OrdinalIgnoreCase))
        {
            TempData["error"] = "Description can't be the same as Name";
            return Page();
        }
        if (_context.Categories.Any(i => i.Name == category.Name && i.UserId == nameIdentifier))
        {
            TempData["error"] = "The Category Name is already in use";
            return Page();
        }

        _context.Categories.Add(category);
          await _context.SaveChangesAsync();
          TempData["success"] = "Category added succesfully";
          return RedirectToPage("CategoryList");
        
        
    }
}
