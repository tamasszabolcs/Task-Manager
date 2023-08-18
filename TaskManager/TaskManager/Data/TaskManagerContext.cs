using TaskManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace TaskManager.Data;
public class TaskManagerContext:DbContext
{
    public TaskManagerContext(DbContextOptions options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Task1> Tasks { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Status> Statuses { get; set; }
}
