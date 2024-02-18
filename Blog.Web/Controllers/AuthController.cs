using Blog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(Register registerViewModel)
    {
        var user = new IdentityUser
        {
            UserName = registerViewModel.UserName,
            Email = registerViewModel.Email,
        };

        var idenityResult = await _userManager.CreateAsync(user, registerViewModel.Password);

        if (idenityResult.Succeeded)
        {
            var addRolesResult = await _userManager.AddToRoleAsync(user, "User");
            
            if (addRolesResult.Succeeded)
            {
                ViewBag.Notification = new Notification
                {
                    Type = Enums.NotificationType.Success,
                    Message = "User registered successfully"
                };

                return View();
            }
        }

        ViewBag.Notification = new Notification
        {
            Type = Enums.NotificationType.Success,
            Message = "Something went wrong"
        };

        return View();

    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string returnUrl, Login loginViewModel)
    {
        var signInResult = await _signInManager.PasswordSignInAsync(
            loginViewModel.UserName, loginViewModel.Password, false, false);

        if (signInResult.Succeeded)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return RedirectToPage(returnUrl);
            }

            return View("../Home/Index");
        }

        ViewBag.Notification = new Notification
        {
            Type = Enums.NotificationType.Error,
            Message = "Unable to login"
        };

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return View("../Home/Index");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
