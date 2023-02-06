using Microsoft.AspNetCore.Mvc;
using SelfieSmile.EntityDB;

namespace SelfieSmile.Controllers
{
    public class BookingController : Controller
    {
        SelfieSmileEntity Context;
        public BookingController(SelfieSmileEntity _Context) { 
        Context= _Context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
