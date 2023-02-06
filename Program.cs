using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SelfieSmile.EntityDB;
using SelfieSmile.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//add DB services and take connectionstring from json file directly
//must put it here before building the app
builder.Services.AddDbContext<SelfieSmileEntity>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"),
    x => x.UseDateOnlyTimeOnly()));

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{   
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 8;
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
    
}).AddEntityFrameworkStores<SelfieSmileEntity>().AddDefaultTokenProviders();

async Task CreateRolesandUsers(IServiceProvider serviceProvider)
{
       
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var UserManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
    // creating Creating Manager role     
    bool x = await roleManager.RoleExistsAsync("Manager");
    if (!x)
    {
        var role = new IdentityRole();
        role.Name = "Manager";
        await roleManager.CreateAsync(role);
    }

    // creating Creating Employee role     
    x = await roleManager.RoleExistsAsync("Doctor");
    if (!x)
    {
        var role = new IdentityRole();
        role.Name = "Doctor";
        await roleManager.CreateAsync(role);
    }
    x = await roleManager.RoleExistsAsync("Employee");
    if (!x)
    {
        var role = new IdentityRole();
        role.Name = "Employee";
        await roleManager.CreateAsync(role);
    }
    x = await roleManager.RoleExistsAsync("Guest");
    if (!x)
    {
        var role = new IdentityRole();
        role.Name = "Guest";
        await roleManager.CreateAsync(role);
    }
    x = await roleManager.RoleExistsAsync("Admin");
    if (!x)
    {
        // first we create Admin role    
        var role = new IdentityRole();
        role.Name = "Admin";
        await roleManager.CreateAsync(role);

        //Here we create a super user who will maintain the website                   

        var user = new AppUser();
        user.UserName = "ahmadkamal";
        user.Email = "Ahmed_elbanna159@yahoo.com";
        user.FirstName = "Ahmad";
        user.LastName = "Kamal";
        string userPWD = "Aa123456789a!";

        IdentityResult chkUser = await UserManager.CreateAsync(user, userPWD);

        //Add default User to Role Admin    
        if (chkUser.Succeeded)
        {
            var result1 = await UserManager.AddToRoleAsync(user, "Admin");
            await UserManager.AddToRoleAsync(user, "Doctor");
        }
    }

 
}
var app = builder.Build();
using var scope = app.Services.CreateScope(); 
await CreateRolesandUsers(scope.ServiceProvider);

//add DB services and take connectionstring from json file directly
//builder.Services.AddDbContext<SelfieSmileEntity>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
