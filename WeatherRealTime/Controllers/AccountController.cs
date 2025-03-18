using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherRealTime.Models;

namespace WeatherRealTime.Controllers;

public class AccountController(IIdentityService _identityService) : Controller
{
    [HttpGet]
    public ActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel(){ ReturnUrl = returnUrl });
    }
    
    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel model)
    {

        if (!_identityService.AreCredentialsValid(model.Username, model.Password))
        {
            return View();
        }

        var claims = new List<Claim>()
        {
            new Claim("sub", model.Username)
        };
        claims.AddRange(_identityService.GetUserClaims(model.Username));
        var claimsIdentity = new ClaimsIdentity(claims, "password", "name", "role");
        var principal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync(principal);
        if (!string.IsNullOrEmpty(model.ReturnUrl))
        {
            return LocalRedirect(model.ReturnUrl);
        }

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [Authorize]
    [HttpGet]
    public ActionResult UserInfo()
    {
        return View(new UserInfoModel(){ Claims = User.Claims });
    }

    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }
}