using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskManager.Data;
using TaskManager.Models;
using TaskManager.Pages.SignUp;

namespace TaskManager.Pages.Login;

public class LoginModel : PageModel
{

    private readonly TaskManagerContext context;
    
    [BindProperty]
    public UserLogInViewModel getUser { get; set; }
    
    public LoginModel(TaskManagerContext context)
    {
        this.context = context;
    }
    public IActionResult OnGet()
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Index");
        }
        else return Page();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        string allow = "notLoggedIn";

        if (string.IsNullOrEmpty(getUser.Username) || string.IsNullOrEmpty(getUser.Password))
        {
            ViewData["InvalidUsernameOrPassword"] = "Please complete username and password.";
            return Page();
        }

        var user = context.Users.SingleOrDefault(x => x.Username == getUser.Username);

        if (user == null)
        {
            ViewData["InvalidUsernameOrPassword"] = "The username or password is incorrect.";
            return Page();
        }
        else
        {
            var hmac = new HMACSHA512(user.SaltedPassword);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(getUser.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.HashedPassword[i])
                {
                    ViewData["InvalidUsernameOrPassword"] = "The username or password is incorrect.";
                    return Page();
                }
                if (i == computedHash.Length - 1)
                {
                    allow = "loggedIn";

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role,"user"),
                        new Claim(ClaimTypes.Name, user.Username)
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,Microsoft.AspNetCore.Authentication.Cookies.
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    HttpContext.SignInAsync(claimsPrincipal);
                }
            }
        }

        if (allow.Equals("loggedIn"))
        {
            return RedirectToPage("/Index");
        }
        else return Page();   
    }   
}
