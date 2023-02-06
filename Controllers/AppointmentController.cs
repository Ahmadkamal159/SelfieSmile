//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using SelfieSmile.EntityDB;
//using SelfieSmile.Models;
//using SelfieSmile.ViewModels;

//namespace SelfieSmile.Controllers
//{
//    public class AppointmentController : Controller
//    {
//        SelfieSmileEntity Context;
//        UserManager<AppUser> userManager;
//        RoleManager<IdentityRole> RoleManager;
//        public AppointmentController(SelfieSmileEntity _context, UserManager<AppUser> _user, RoleManager<IdentityRole> _roleManager)
//        {
//            Context = _context;
//            userManager = _user;
//            RoleManager = _roleManager;
//        }

//        public async Task<IActionResult> Index()
//        {
            
//            //var doctors = await userManager.GetUsersInRoleAsync("Doctor");
//            //ViewData["doctorsNames"] =doctors.Select(x => new SelectListItem { Value = x.UserName, Text = x.FirstName.ToString() + " " + x.LastName.ToString() }).ToList();
//            List<Appointment> appointments = Context.Appointments.ToList();
//            List<AppUser> appUsers = userManager.Users.ToList();
//            var AllAppointments = from AppUser in appUsers
//                                 join Appointment in appointments on AppUser.Id equals Appointment.ReservedToDoctorId
//                                 select new
//                                 {
//                                     Date = Appointment.AddAppointmentDate,
//                                     DoctorName = AppUser.FirstName + " " + AppUser.LastName,
//                                     Status = Appointment.IsReserved,
//                                     DoctorUserName = AppUser.UserName
//                                 };

//            List<ViewModelAppointment> viewModelAppointments = new();

//            foreach (var appoint in AllAppointments)
//            {
//                ViewModelAppointment vmapp = new();
//                vmapp.AddAppointmentDate = appoint.Date;
//                vmapp.ReservedToDoctorName = appoint.DoctorName;
//                vmapp.IsReserved = appoint.Status;
//                viewModelAppointments.Add(vmapp);
//            }
//            return View(viewModelAppointments);
//        }



//        public async Task<IActionResult> SelectDoctorForAppointment()
//        {
//            var Doctors =await userManager.GetUsersInRoleAsync("Doctor");
//            List<ViewModelRegisteration> vmdoctors = new();
//            foreach(var doc in Doctors)
//            {
//                ViewModelRegisteration vmdoc = new();
//                vmdoc.FirstName = doc.FirstName;
//                vmdoc.LastName  = doc.LastName;
//                vmdoc.UserName  = doc.UserName;
//                vmdoctors.Add(vmdoc);
//            }
//            ViewData["doctorsNames"] = vmdoctors.Select(x => new SelectListItem { Value =x.UserName , Text = x.FirstName.ToString() + " " + x.LastName.ToString() }).ToList();
//            return View(vmdoctors);
//        }


//        public async Task<IActionResult> DoctorAppointments(string UserName)
//        {
//            if (userManager.FindByNameAsync(UserName) != null)
//            {
//                List<Appointment> appointments = Context.Appointments.ToList();
//                List<AppUser> appUsers = userManager.Users.ToList();
//                var AllAPointments = from AppUser in appUsers
//                                     join Appointment in appointments on AppUser.Id equals Appointment.ReservedToDoctorId
//                                     select new
//                                     {
//                                         Date = Appointment.AddAppointmentDate,
//                                         DoctorName = AppUser.FirstName + " " + AppUser.LastName,
//                                         Status = Appointment.IsReserved,
//                                         DoctorUserName = AppUser.UserName
//                                     };
//                List<ViewModelAppointment> vmApps = new();

//                foreach (var appoint in AllAPointments.Where(x => x.DoctorUserName == UserName))
//                {
//                    ViewModelAppointment vmapp = new();
//                    vmapp.AddAppointmentDate = appoint.Date;
//                    vmapp.ReservedToDoctorName = appoint.DoctorName;
//                    vmapp.IsReserved = appoint.Status;
//                    vmApps.Add(vmapp);

//                }
//                return View(vmApps);
//            }
//            return Content("no results To Show");
//        }

//        public async Task<IActionResult> AddAppointment()
//        {

//            return View();
//        }


//    }
//}
