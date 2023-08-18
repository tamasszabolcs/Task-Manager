using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TaskManager;
using TaskManager.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();



builder.Services.AddDbContext<TaskManagerContext>(options => options.
    UseSqlite(builder.Configuration.GetConnectionString("SpringPractice")));

builder.Services.AddAuthentication(Microsoft.AspNetCore.
    Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme).
    AddCookie(x =>
    {
        x.Cookie.Name = "LogInCookie";
        x.LoginPath = "/Login/Login";
        x.LogoutPath = "/Logout/Logout";
    });
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAuthentication();
app.MapRazorPages();

// Initialize the database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TaskManagerContext>();
    DbInitializer.Initialize(dbContext);
}
app.Run();
