using TaskManager.Models;

namespace TaskManager.Data
{
    public class DbInitializer
    {
        public static void Initialize(TaskManagerContext context)
        {
            context.Database.EnsureCreated();

            if (context.Statuses.Any() || context.Categories.Any())
            {
                return;   // DB has been seeded
            }

            var statuses = new Status[]
            {
                new Status { Name = "Unstarted" },
                new Status { Name = "In Progress" },
                new Status { Name = "Finished" }
            };
            foreach (var s in statuses)
            {
                context.Statuses.Add(s);
            }
            context.SaveChanges();

        }
    }
}
