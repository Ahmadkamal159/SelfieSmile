using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SelfieSmile.Models;
using SelfieSmile.ViewModels;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace SelfieSmile.Controllers
{
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public UserManager<AppUser> UserManager { get; }
        public SignInManager<AppUser> SignInManager { get; }

        public AccountController(UserManager<AppUser> _user,SignInManager<AppUser> _signInManager, RoleManager<IdentityRole>_roleManager)
        {
            UserManager = _user;
            SignInManager = _signInManager;
            roleManager = _roleManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            ViewModelRegisteration viewModelRegisteration = new ViewModelRegisteration();
            return View(viewModelRegisteration);
        }

        [HttpPost]
        [RequestSizeLimit(3000000)]
        public async Task<IActionResult> Register(ViewModelRegisteration viewModelRegisteration)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new();
                user.UserName = viewModelRegisteration.UserName.ToLower();
                user.FirstName = viewModelRegisteration.FirstName;
                user.LastName = viewModelRegisteration.LastName;
                user.Email = viewModelRegisteration.Email;
                user.PhoneNumber = viewModelRegisteration.PhoneNumber;
                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];
                    using(var picstream=new MemoryStream())
                    {
                        await file.CopyToAsync(picstream);
                        user.ProfilePicture = picstream.ToArray();
                    }
                }
                IdentityResult result = await UserManager.CreateAsync(user, viewModelRegisteration.Password);
                IdentityResult res = await UserManager.AddToRoleAsync(user,"Guest");
                if (result.Succeeded && res.Succeeded)
                {                     
                    await SignInManager.SignInAsync(user, false);
                    
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    foreach(var error in res.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(viewModelRegisteration);
        }


        public IActionResult Login(string ReturnUrl = "~/Home/Index")
        {
            ViewData["redirect"] = ReturnUrl;
            return View();
        }
        
        
        [HttpPost]

        public async Task<IActionResult> Login(ViewmodelLogin viewmodelLogin,string ReturnUrl = "~/Home/Index")
        {
            ModelState.Remove("ReturnUrl");
            if (ModelState.IsValid)
            {
                AppUser userbyemail = await UserManager.FindByEmailAsync(viewmodelLogin.UserName);

                AppUser userbyusername = await UserManager.FindByNameAsync(viewmodelLogin.UserName.ToLower());
                if (userbyusername != null)
                {
                    SignInResult result= await SignInManager.PasswordSignInAsync(userbyusername, viewmodelLogin.Password, viewmodelLogin.RememberMe,false);
                    if (result.Succeeded)
                    {
                        return LocalRedirect(ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "invalid username or password");
                    }
                }
                else if( userbyemail != null)
                {
                    SignInResult result = await SignInManager.PasswordSignInAsync(userbyemail, viewmodelLogin.Password, viewmodelLogin.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return LocalRedirect(ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "invalid username or password");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "invalid username or password");
                }
            }
            return View(viewmodelLogin);
        }

        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
