using AdminDashboard.Models;
using Domain.Entities.SecurityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace AdminDashboard.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {

            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleFormViewModel role)
        {
            if (ModelState.IsValid)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role.Name.Trim());
                if (roleExists)
                {
                    ModelState.AddModelError("Name", "This Role is Already Exist");
                    return RedirectToAction("Index");
                }
                await _roleManager.CreateAsync(new IdentityRole { Name = role.Name });
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Delete(string id)
        {
            var role= await _roleManager.FindByIdAsync(id);
            if (role is not null)
            {
                await _roleManager.DeleteAsync(role);

            }
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role=await _roleManager.FindByIdAsync(id);

            var mappedRole = new RoleViewModel { Id = role.Id, Name = role.Name.Trim() };

            return View(mappedRole);
        }
		[HttpPost]
		public async Task<IActionResult> Edit(RoleViewModel roleVM)
		{
			if (ModelState.IsValid)
			{
				var roleExists = await _roleManager.RoleExistsAsync(roleVM.Name.Trim());
				if (roleExists)
				{
					ModelState.AddModelError("Name", "This Role is Already Exist");
					return RedirectToAction("Index");
				}
				var role = await _roleManager.FindByIdAsync(roleVM.Id);

				role.Name = roleVM.Name.Trim();
				await _roleManager.UpdateAsync(role);
				return RedirectToAction("Index");

			}
			return View(roleVM);
			//if (ModelState.IsValid)
			//{
			//	var roleExists = await _roleManager.RoleExistsAsync(roleVM.Name.Trim());
			//	if (roleExists)
			//	{
			//		ModelState.AddModelError("Name", "This Role is Already Exist");
			//		return View(roleVM); // Return the same view instead of redirecting
			//	}
			//	await _roleManager.CreateAsync(new IdentityRole { Name = roleVM.Name });
			//	return RedirectToAction("Index");
			//}
			//return View(roleVM); // Return view with validation errors



			//}
			//[HttpPost]
			//public async Task<IActionResult> Edit(RoleViewModel roleVM)
			//{
			//	if (ModelState.IsValid)
			//	{
			//		var role = await _roleManager.FindByIdAsync(roleVM.Id);
			//		if (role == null)
			//		{
			//			return NotFound(); // Return a 404 if the role is not found
			//		}

			//		// Check if another role with the same name exists
			//		var roleExists = await _roleManager.RoleExistsAsync(roleVM.Name.Trim());
			//		if (roleExists && role.Name != roleVM.Name.Trim())
			//		{
			//			ModelState.AddModelError("Name", "This Role already exists.");
			//			return View(roleVM); // Return the view with validation error
			//		}

			//		// Update the role name
			//		role.Name = roleVM.Name.Trim();
			//		await _roleManager.UpdateAsync(role);
			//		return RedirectToAction("Index");
			//	}

			//	return View(roleVM); // Return view with validation errors if ModelState is invalid
			//}


		}
		}
}
