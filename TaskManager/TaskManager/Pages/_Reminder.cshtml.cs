using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages;

public class ReminderModel : PageModel
{
    private readonly TaskManagerContext _context;

    public List<Task1> Tasks { get; set; }

    public ReminderModel(ILogger<IndexModel> logger, TaskManagerContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        if (User.Identity.IsAuthenticated)
        {
            int nameIdentifier = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Tasks = _context.Tasks.Where(x => x.UserId == nameIdentifier).ToList();
        }
    }

}