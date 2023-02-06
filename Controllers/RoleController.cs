using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelfieSmile.Models;
using SelfieSmile.ViewModels;

namespace SelfieSmile.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly UserManager<AppUser> userManager;

        public RoleController(RoleManager<IdentityRole> _RoleManager, UserManager<AppUser> _usermanager)
        {
            userManager = _usermanager;
            RoleManager = _RoleManager;
        }

        public IActionResult Index()
        {
            List<ViewModelRole> rolesToView = new();

            foreach (var role in RoleManager.Roles)
            {
                ViewModelRole v = new();
                v.RoleName = role.Name;
                v.Id=role.Id;
                rolesToView.Add(v);
            }

            return View(rolesToView);
        }


        public IActionResult AddRole()
        {
            ViewModelRole role = new();
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole([include: RoleName] ViewModelRole role)
        {
            ModelState.Remove("Id"); 
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new();
                identityRole.Name = role.RoleName;
                IdentityResult res = await RoleManager.CreateAsync(identityRole);
                if (res.Succeeded)
                {
                    RoleManager.CreateAsync(identityRole);

                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    foreach (var error in res.Errors)
                        ModelState.AddModelError("", error.Description);
                }

            }

            return View(role);
        }

        public ActionResult Edit(string? Id)
        {
            var role= RoleManager.Roles.FirstOrDefault(x=>x.Id==Id);
            if (Id != role.Id)
            {
                return Content("invalid role ID");
            }
            ViewModelRole r = new() { RoleName = role.Name, Id = role.Id };
            return View(r);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(ViewModelRole r)
        {
            if(ModelState.IsValid)
            {
                IdentityRole idenrole = RoleManager.Roles.FirstOrDefault(x=>x.Id==r.Id);
                idenrole.Name = r.RoleName;
                IdentityResult res= await RoleManager.UpdateAsync(idenrole);
                if (res.Succeeded)
                {

                    await RoleManager.UpdateAsync(idenrole);
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    foreach(var error in res.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }

        public async Task<ActionResult> Delete(string? id)
        {

            if (await RoleManager.FindByIdAsync(id)==null)
            {
                return Content("there's No Item to delete");
            }
            IdentityRole iden = RoleManager.Roles.FirstOrDefault(x => x.Id == id);

            await RoleManager.DeleteAsync(iden);

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> UsersRolesIndex(string search)
        {
            List<ViewModelUserwithRole> users = new();
            //var x= RoleManager.Roles.Select(x => x.Nam);
            var x = RoleManager.Roles.ToList().SelectMany(x => x.Name);
            ViewData["roles"] = RoleManager.Roles.OrderBy(x=>x.Name).ToList().Select(x =>new SelectListItem { Value = x.Name.ToString(), Text = x.Name}).ToList();
            if(String.IsNullOrEmpty(search)) { 
            foreach (var user in userManager.Users)
            {
                ViewModelUserwithRole viewModel = new();
                viewModel.UserName = user.UserName;
                viewModel.Email = user.Email;
                var singlerole = (List<string>)await userManager.GetRolesAsync(user);
                viewModel.Role = singlerole.FirstOrDefault();
                users.Add(viewModel);
            }
            }else
            {
                ViewModelUserwithRole searcheduser = new();
                AppUser iden =await userManager.FindByEmailAsync(search);
                users.Clear();
                searcheduser.UserName=iden.UserName; searcheduser.Email = iden.Email;
                var singlerole = (List<string>)await userManager.GetRolesAsync(iden);
                searcheduser.Role = singlerole.FirstOrDefault();
                users.Add(searcheduser);
            }
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveChanges([Bind(Prefix ="item")]ViewModelUserwithRole viewModel)
        {
            AppUser IdenUser=await userManager.FindByEmailAsync(viewModel.Email);
            IdenUser.Email = viewModel.Email;
            var oldRole = await userManager.GetRolesAsync(IdenUser);

            if (oldRole.FirstOrDefault() != viewModel.Role)
            {
                if (viewModel.Role == "Admin")
                {
                    await userManager.AddToRoleAsync(IdenUser, viewModel.Role);
                    await userManager.AddToRoleAsync(IdenUser, "Doctor");
                    await userManager.UpdateAsync(IdenUser);
                    return RedirectToAction("UsersRolesIndex");

                }
               await userManager.RemoveFromRoleAsync(IdenUser, oldRole.FirstOrDefault());
               await userManager.AddToRoleAsync(IdenUser, viewModel.Role);
               await userManager.UpdateAsync(IdenUser);
                return RedirectToAction("UsersRolesIndex");
            }

            return  RedirectToAction("UsersRolesIndex");
        }


    }
}
