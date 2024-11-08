using Domain.Entities.SecurityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.SecurityModels;

namespace AdminDashboard.Controllers
{
    public class AdminController(UserManager<User> userManager, SignInManager<User> signInManager) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Login(LoginDTO loginDTO)
        {
            if(!ModelState.IsValid)
            {
                return View(loginDTO);
            }

            var user = await userManager.FindByEmailAsync(loginDTO.Email);

            if (user != null)
            {
                var result=await signInManager.CheckPasswordSignInAsync(user, loginDTO.Password,false);
                if(!result.Succeeded ||await userManager.IsInRoleAsync(user,"Admin"))
                {
                    return View(loginDTO);
                }
                await signInManager.SignInAsync(user,false);
                return RedirectToAction("Index", "Home");
            }


            return View(loginDTO);
        }
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }



    }
}
