using AdminDashboard.Models;
using Domain.Entities.SecurityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboard.Controllers
{
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        public UserController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task <IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            var mappedUsers = users.Select( u => new UserViewModel()
            {
                Id = u.Id,
                UserName =u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                DisplayName=u.DisplayName,
                Roles =  _userManager.GetRolesAsync(u).Result

            }).ToList();
            return View(mappedUsers);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user=await _userManager.FindByIdAsync(id);

            var allRoles = await _roleManager.Roles.ToListAsync();

            var mappedUser = new UserRoleViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Roles = allRoles.Select(r => new RoleViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, r.Name).Result,
                }).ToList(),
            };
            return View(mappedUser);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserRoleViewModel userRoleVM)
        {
            var user = await _userManager.FindByIdAsync(userRoleVM.Id);
            
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoleVM.Roles)
            {
                if(userRoles.Any(r=>r==role.Name )&&!role.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user,role.Name);
                
                
                if(!userRoles.Any(r=>r==role.Name )&&role.IsSelected)
                    await _userManager.AddToRoleAsync(user,role.Name);
            }
            return RedirectToAction("Index");
        }

    }
}
