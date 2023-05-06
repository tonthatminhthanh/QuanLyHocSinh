using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QLHocSinhHVT.Models
{
    [Authorize]
    public class UserRolesController : Controller
    {
        UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        // GET: UserRoles
        [Authorize(Roles = "User")]
        public async Task<ActionResult> Index()
        {
            var users = await userManager.Users.ToListAsync();
            var userRoles = new List<UserRolesViewModel>();
            foreach (var user in users)
            {
                var thisViewModel = new UserRolesViewModel();
                thisViewModel.userId = user.Id;
                thisViewModel.email = user.Email;
                thisViewModel.roles = await GetUserRoles(user);
                userRoles.Add(thisViewModel);
            }
            return View(userRoles);
        }
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Manage(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RoleSelectionModel selectionModel = new RoleSelectionModel();
            selectionModel.roles = new List<RolesViewModel>();
            selectionModel.user = new UserRolesViewModel();
            selectionModel.user.userId = user.Id;
            selectionModel.user.email = user.Email;
            selectionModel.user.roles = await GetUserRoles(user);

            var roles = await roleManager.Roles.ToListAsync();
            foreach(var r in roles)
            {
                if(selectionModel.user.roles.Contains(r.Name))
                {
                    selectionModel.roles.Add(new RolesViewModel(r.Id, r.Name) { selected = true});
                }
                else
                {
                    selectionModel.roles.Add(new RolesViewModel(r.Id, r.Name) { selected = false });
                }
            }

            return View(selectionModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Manage(RoleSelectionModel model)
        {
            var user = await userManager.FindByIdAsync(model.user.userId);
            string userId = user.Id.ToString();
            if(model.user.roles != null)
            {
                string[] rolesToRemove = model.user.roles.ToArray();
                await userManager.RemoveFromRolesAsync(userId, rolesToRemove);
            }
            
            foreach(var role in model.roles)
            {
                if(role.selected)
                    await userManager.AddToRoleAsync(userId, role.RoleName);
            }
            
            return RedirectToAction("Index");
        }

        public async Task<List<String>> GetUserRoles(ApplicationUser user)
        {
            return (List<String>) await userManager.GetRolesAsync(user.Id);
        }
    }
}