using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.SignUp;

public class SignUp: PageModel
{
    private readonly TaskManagerContext context;

    public SignUp(TaskManagerContext context)
    {
        this.context = context;
    }

    [BindProperty]
    public UserViewModel addUserRequest { get; set; }

    public IActionResult OnGet()
    {
        if (User.Identity.IsAuthenticated)
            return RedirectToPage("/Index");
        else return Page();
    }

    public IActionResult OnPost()
    {
        string allow = "notAllowed";  

        if (string.IsNullOrEmpty(addUserRequest.Username) || string.IsNullOrEmpty(addUserRequest.Email) ||
            string.IsNullOrEmpty(addUserRequest.Password) || string.IsNullOrEmpty(addUserRequest.RepeatedPassword))
        {
            ViewData["Message"] = "Please complete the fields above.";
            return Page();
        }

        if (UsernameExists(addUserRequest.Username.ToLower()))
        {
            if (EmailExsits(addUserRequest.Email.ToLower()))
            {
                if (addUserRequest.Password.Equals(addUserRequest.RepeatedPassword))
                {
                    var hmac = new HMACSHA512();
                    
                    var user = new User
                    {
                        Username = addUserRequest.Username,
                        Email = addUserRequest.Email,
                        HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(addUserRequest.Password)),
                        SaltedPassword= hmac.Key
                    };

                    context.Users.Add(user);
                    context.SaveChanges();
                    allow = "allow";
                }
                else ViewData["Message"] = "The passwords are different.";
            } 
            else ViewData["Message"] = "This email has already exits.";
        }
        else ViewData["Message"] = "This username has already exits.";

        if (allow.Equals("allow"))
            return RedirectToPage("./CreatedAccount");
        else return Page();
    }
    private bool UsernameExists(string username)
    {
        var res = context.Users.Where(x => x.Username.Contains(username));

        if (res.Any())
            return false;
        else return true;
    }
    private bool EmailExsits(string email)
    {
        var res = context.Users.Where(x => x.Email.Contains(email));
        if (res.Any())
            return false;
        else return true;
    }   
}
