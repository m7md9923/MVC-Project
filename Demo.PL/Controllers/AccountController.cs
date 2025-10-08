using Demo.DAL.Models.IdentityModule;
using Demo.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers;

public class AccountController(UserManager<ApplicationUser> _userManager) : Controller
{
    #region Register

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(registerViewModel);
        }

        var user = (new ApplicationUser()
        {
            UserName = registerViewModel.Email,
            FirstName = registerViewModel.FirstName,
            LastName = registerViewModel.LastName,
            Email = registerViewModel.Email
        });
        var result = _userManager.CreateAsync(user, registerViewModel.Password).Result;
        // .Result ==> Convert Process from async to synchronous 
        if (result.Succeeded)
        {
            return RedirectToAction("Login");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(String.Empty, error.Description);
        }
        return View(registerViewModel);
    }
    
    #endregion
    
}