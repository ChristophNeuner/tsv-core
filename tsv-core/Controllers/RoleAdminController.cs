using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using tsv_core.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace tsv_core.Controllers
{
    [Authorize(Roles = "Admins")]
    public class RoleAdminController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<AppUser> userManager;
        private DBInformation dbInformation;
        private object lockObj = new object();
        public RoleAdminController(RoleManager<IdentityRole> roleMgr, UserManager<AppUser> userMrg, DBInformation dbi)
        {
            roleManager = roleMgr;
            userManager = userMrg;
            dbInformation = dbi;
        }
        public ViewResult Index() => View(roleManager.Roles);
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create([Required]string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result
                = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(name);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "No role found");
            }
            return View("Index", roleManager.Roles);
        }

        public IActionResult Edit(string id)
        {
            lock (lockObj)
            {
                IdentityRole role = roleManager.FindByIdAsync(id).Result;
                List<AppUser> members = new List<AppUser>();
                List<AppUser> nonMembers = new List<AppUser>();
                foreach (AppUser user in userManager.Users)
                {
                    var list = dbInformation.IsUserInRole(user, role)
                    ? members : nonMembers;
                    list.Add(user);
                }
                return View(new RoleEditModel
                {
                    Role = role,
                    Members = members,
                    NonMembers = nonMembers
                });
            }

        }
        [HttpPost]
        public IActionResult Edit(RoleModificationModel model)
        {
            lock (lockObj)
            {
                IdentityResult result;
                if (ModelState.IsValid)
                {
                    foreach (string userId in model.IdsToAdd ?? new string[] { })
                    {
                        AppUser user = userManager.FindByIdAsync(userId).Result;
                        if (user != null)
                        {
                            result = userManager.AddToRoleAsync(user,
                            model.RoleName).Result;
                            if (!result.Succeeded)
                            {
                                AddErrorsFromResult(result);
                            }
                        }
                    }
                    foreach (string userId in model.IdsToDelete ?? new string[] { })
                    {
                        AppUser user = userManager.FindByIdAsync(userId).Result;
                        if (user != null)
                        {
                            result = userManager.RemoveFromRoleAsync(user,
                            model.RoleName).Result;
                            if (!result.Succeeded)
                            {
                                AddErrorsFromResult(result);
                            }
                        }
                    }
                }
                if (ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return Edit(model.RoleId);
                }
            }
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}