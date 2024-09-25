﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_03.Controllers
{
    public class RoleController : Controller
    {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            public RoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
            {
                this._userManager = userManager;
                _roleManager = roleManager;
            }
            public IActionResult Create()
            {
                return View();
            }
            [HttpPost]
            public async Task<IActionResult> Create(RoleViewModel model)
            {
                if (!ModelState.IsValid) return View(model);

                var role = new IdentityRole
                {
                    Name = model.Name,
                };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded) return RedirectToAction(nameof(Index));
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);


            }
            public async Task<IActionResult> Index(string name)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    var roles = await _roleManager.Roles.Select(r => new RoleViewModel
                    {


                        Id = r.Id,
                        Name = r.Name,
                    }).ToListAsync();
                    return View(roles);
                }
                var role = await _roleManager.FindByNameAsync(name);
                if (role is null) return View(Enumerable.Empty<RoleViewModel>());

                var model = new RoleViewModel
                {

                    Id = role.Id,
                    Name = role.Name,
                };
                return View(model);
            }

            public async Task<IActionResult> Details(string id, string viewName = nameof(Details))
            {
                if (string.IsNullOrWhiteSpace(id)) return BadRequest();
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null) return View(Enumerable.Empty<RoleViewModel>());

                var model = new RoleViewModel
                {

                    Id = role.Id,
                    Name = role.Name,
                };
                return View(viewName, model);
            }
            public async Task<IActionResult> Edit(string id) => await Details(id, nameof(Edit));
            [HttpPost]
            public async Task<IActionResult> Edit(string id, RoleViewModel model)
            {
                if (id != model.Id) return BadRequest();
                if (!ModelState.IsValid) return View(model);
                try
                {
                    var role = await _roleManager.FindByIdAsync(model.Id);
                    if (role is null) return NotFound();
                    role.Name = model.Name;

                    await _roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                return View(model);
            }
            public async Task<IActionResult> Delete(string id) => await Details(id, nameof(Delete));
            [ActionName("Delete")]
            [HttpPost]
            public async Task<IActionResult> ConfirmDelete(string id)
            {

                try
                {
                    var user = await _roleManager.FindByIdAsync(id);
                    if (user is null) return NotFound();

                    await _roleManager.DeleteAsync(user);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                return View();
            }

            public async Task<IActionResult> AddOrRemove(string roleId)
            {

                var role = await _roleManager.FindByIdAsync(roleId);
                if (role is null) return NotFound();
                ViewBag.RoleId = roleId;
                var users = await _userManager.Users.ToListAsync();
                var usersInRole = new List<UserInRoleviewModel>();
                foreach (var user in users)
                {
                    var userInRole = new UserInRoleviewModel
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        IsInRole = await _userManager.IsInRoleAsync(user, role.Name)
                    };
                    usersInRole.Add(userInRole);
                }
                return View(usersInRole);
            }
            [HttpPost]
            public async Task<IActionResult> AddOrRemove(string roleId, List<UserInRoleviewModel> users)
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role is null) return NotFound();
                if (ModelState.IsValid)
                {
                    foreach (var user in users)
                    {
                        var appUser = await _userManager.FindByIdAsync(user.UserName);
                        if (appUser is null) return NotFound();
                        if (user.IsInRole && !await _userManager.IsInRoleAsync(appUser, role.Name)) ;
                        await _userManager.AddToRoleAsync(appUser, role.Name);

                        if (!user.IsInRole && !await _userManager.IsInRoleAsync(appUser, role.Name)) ;
                        await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                    }
                    return RedirectToAction(nameof(Edit), new { id = roleId });
                }
                return View(users);
            }
        }
}
