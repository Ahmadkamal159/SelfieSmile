//using Microsoft.AspNetCore.Mvc;
//using SelfieSmile.EntityDB;
//using SelfieSmile.Models;

//namespace SelfieSmile.Controllers
//{
//    public class UniversityController : Controller
//    {
        
//        SelfieSmileEntity Context;
//        public UniversityController(SelfieSmileEntity _context)
//        {

//            Context = _context;
//        }

//        public IActionResult CheckName(string Name,int id)
//        {
//            if(id == 0)
//            {
//                University uni = Context.Universities.FirstOrDefault(x => x.Name == Name);

//                if (uni == null)
//                {
//                return Json(true);
//                }
//            else
//               {
//                return Json(false);
//               }
//            }
//            else
//            {
//                University uni = Context.Universities.FirstOrDefault(x => x.Name == Name);

//                if (uni== null)
//                    return Json(true);
//                else
//                {
//                    if (uni.Id == id)
//                    {
//                        return Json(true);
//                    }
//                    else { return Json(false); }
//                }
//            }

//        }

//        public IActionResult Index()
//        {
//           List<University> uni = Context.Universities.ToList();
//            List<UniversityDTO> universityDTOs=new List<UniversityDTO>();
//            foreach(var item in uni)
//            {
//                UniversityDTO UniDTO = new();
//                UniDTO.Id= item.Id;
//                UniDTO.Name= item.Name;
//                UniDTO.Location = item.Location;
//                universityDTOs.Add(UniDTO);
//            }
            
//            return View(universityDTOs);
//        }

//        public IActionResult Add()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Add([include:Name,Location] UniversityDTO university)//here I've put DTO to get data about uni only and to pass the modelstate as well
//        {


//            University uni = new();
//            uni.Name = university.Name;
//            uni.Location = university.Location;
//            if (ModelState.IsValid)
//            {
//                Context.Universities.Add(uni);
//                Context.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(university);
            
//        }

//        public IActionResult Edit(int? Id)
//        {
//            if (Id == null)
//            {
//                return Content("No University is selected");
//            }
//            else
//            {
//                University uni = Context.Universities.FirstOrDefault(x => x.Id == Id);
//                UniversityDTO dTO = new();
//                dTO.Id = uni.Id;
//                dTO.Name = uni.Name;
//                dTO.Location = uni.Location;
//                return View(dTO);
//            }
//        }
//        [HttpPost]
//        public IActionResult Edit(int? Id,UniversityDTO university)
//        {
//            if (ModelState.IsValid)
//            {
//                University uni = Context.Universities.FirstOrDefault(x => x.Id == Id);
//                uni.Name=university.Name;
//                uni.Location=university.Location;
//                Context.Universities.Update(uni);
//                Context.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(university);
//        }

//        public IActionResult Delete(int? Id)
//        {
//            if (Id == null)
//            {
//                return Content("there's No Item to delete");
//            }
//            University Uni = Context.Universities.FirstOrDefault(x => x.Id == Id);
//            Context.Universities.Remove(Uni);
//            Context.SaveChanges();
//            return RedirectToAction("Index");
           
//        }
//        public IActionResult Details(int? Id)
//        {
//            if(Id== null)
//            {
//                return Content("There's no University to show");
//            }

//            University Uni = Context.Universities.FirstOrDefault(x=>x.Id==Id);
//            UniversityDTO UniDTO = new();
//            UniDTO.Id = Uni.Id;
//            UniDTO.Name = Uni.Name;
//            UniDTO.Location = Uni.Location;
//            return View(UniDTO);
//        }

//        //public IActionResult PartialUniversityDetails(int Id)
//        //{

//        //    University uni= Context.Universities.SingleOrDefault(x => x.Id == Id);
//        //    UniversityDTO dTO= new() { Id=uni.Id,Name=uni.Name,Location=uni.Location};
            
//        //    return PartialView("Details",dTO);
//        //}
//    }
   
//}
