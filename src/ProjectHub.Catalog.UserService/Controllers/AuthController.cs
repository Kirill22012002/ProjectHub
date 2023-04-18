using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectHub.Catalog.UserService.Data.Models;
using ProjectHub.Catalog.UserService.Models;

namespace ProjectHub.Catalog.UserService.Controllers;

public class AuthController : Controller
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IIdentityServerInteractionService _interactionService;

    public AuthController(
        SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager,
        IIdentityServerInteractionService interactionService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _interactionService = interactionService;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        var loginVm = new LoginViewModel
        {
            ReturnUrl = returnUrl
        };

        return View(loginVm);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginVm)
    {
        if (!ModelState.IsValid)
        {
            return View(loginVm);
        }

        var user = await _userManager.FindByEmailAsync(loginVm.Email);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "User not found");
            return View(loginVm);
        }

        var result = await _signInManager.PasswordSignInAsync(
            loginVm.Username,
            loginVm.Password,
            false,
            false);

        if (result.Succeeded)
        {
            return Redirect(loginVm.ReturnUrl);
        }

        ModelState.AddModelError(string.Empty, "Login error");
        return View(loginVm);
    }

    [HttpGet]
    public IActionResult Register(string returnUrl)
    {
        var registerVm = new RegisterViewModel
        {
            ReturnUrl = returnUrl
        };
        return View(registerVm);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerVm)
    {
        if (!ModelState.IsValid)
        {
            return View(registerVm);
        }

        var user = new AppUser
        {
            Email = registerVm.Email,
            UserName = registerVm.Username
        };

        var result = await _userManager.CreateAsync(user, registerVm.Password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return Redirect(registerVm.ReturnUrl);
        }
        ModelState.AddModelError(string.Empty, "Error occurred");
        return View(registerVm);
    }

    [HttpGet]
    public async Task<IActionResult> Logout(string logoutId)
    {
        await _signInManager.SignOutAsync();
        var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
        return Redirect(logoutRequest.PostLogoutRedirectUri);
    }
}