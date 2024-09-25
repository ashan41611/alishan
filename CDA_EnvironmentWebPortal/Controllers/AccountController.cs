


using CDA_EnvironmentWebPortal.Data.Repository;
using CDA_EnvironmentWebPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace CDA_EnvironmentWebPortal.Controllers;

//[AllowAnonymous]
public class AccountController(
   
    IGenericRepository genericRepository
    ) :  Controller
{
    private readonly IGenericRepository _genericRepository= genericRepository;

 

 


    public IActionResult Login()
    {
        UserLoginViewModel model = new();
        return View(model);
    }

    [HttpPost]
   public async Task<IActionResult> Login(UserLoginViewModel model)
{
    if (ModelState.IsValid)
    {
        var UserResult= await _genericRepository.UserExists(model.Email);
            if (!UserResult)
            {
                ModelState.AddModelError("message", "Invalid Username");
                ViewData["Password"] = model.Password;
                return View(model);
            }
        var user = await _genericRepository.Login(model.Email, model.Password);

        if (user == null)
        {
            ModelState.AddModelError("message", "Invalid Password.");
            // Store the password in ViewData to retain it in the view
            ViewData["Password"] = model.Password;
            return View(model);
        }

        return RedirectToAction("GetEmployeeDutyRoster", "employee");
    }

    // Store the password in ViewData to retain it in the view
    ViewData["Password"] = model.Password;
    return View(model);
}

   
    public async Task<IActionResult> Logout()
    {
        //await signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }

}
  
