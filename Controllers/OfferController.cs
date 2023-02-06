using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SelfieSmile.EntityDB;
using SelfieSmile.Models;
using SelfieSmile.ViewModels;

namespace SelfieSmile.Controllers
{
    
    public class OfferController : Controller
    {
        private readonly SelfieSmileEntity Context;

        public OfferController(SelfieSmileEntity _Context)
        {
            Context = _Context;
        }
        [Authorize]
        public IActionResult Index()
        {
            var offers = Context.Offers.ToList();
            //check offers expiry-date and delete expired ones
            foreach(var offer in offers)
            {
                if (offer.ExpiryDate.CompareTo(DateTime.Today) < 0)
                {
                    Context.Offers.Remove(offer);
                    Context.SaveChanges();
                }
            }
            var validoffers= Context.Offers.ToList();
            List<ViewModelOffer> viewModelOffers = new();
            foreach (var offer in validoffers)
            {
                ViewModelOffer vmoffer = new();
                vmoffer.Id = offer.Id;
                vmoffer.Title = offer.Title;
                vmoffer.Description = offer.Description;
                vmoffer.PromoCode = offer.PromoCode;
                vmoffer.ExpiryDate = offer.ExpiryDate;
                viewModelOffers.Add(vmoffer);
            }
            return View(viewModelOffers);
        }

        [Authorize(Roles ="Admin,Manager")]
        public IActionResult AddOffer()
        {
            ViewModelOffer offer = new();
            return View(offer);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult AddOffer(ViewModelOffer NewVMOffer)
        {
            if (ModelState.IsValid)
            {
                Offer contextoffer = new();
                contextoffer.Title = NewVMOffer.Title;
                contextoffer.Description = NewVMOffer.Description;
                contextoffer.PromoCode = NewVMOffer.PromoCode;
                contextoffer.ExpiryDate = NewVMOffer.ExpiryDate;
                Context.Offers.Add(contextoffer);
                Context.SaveChanges();
            }
            return View(NewVMOffer);
        }

        [Authorize(Roles = "Admin,Manager")]
        public IActionResult EditOffer(Guid?Id)
        {
            var offers=Context.Offers.ToList();
            var offer = offers.Where(x => x.Id == Id).FirstOrDefault();
            if (offer != null) {
                ViewModelOffer vmoffer = new() {Id=offer.Id,Description=offer.Description,Title=offer.Title,PromoCode=offer.PromoCode,ExpiryDate=offer.ExpiryDate };
                return View(vmoffer);
            }
            return Content("Content is not available!");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult EditOffer(ViewModelOffer vmoffer)
        {
            if (ModelState.IsValid)
            {
                var offertoedit = Context.Offers.FirstOrDefault(x => x.Id == vmoffer.Id);
                offertoedit.Title = vmoffer.Title;
                offertoedit.Description = vmoffer.Description;
                offertoedit.PromoCode = vmoffer.PromoCode;
                offertoedit.ExpiryDate = vmoffer.ExpiryDate;
                Context.Update(offertoedit);
                Context.SaveChanges();
                return RedirectToAction("Index");
            }
            return Content("Content is not available!");
        }

        [Authorize(Roles = "Admin,Manager")]
        public IActionResult DeleteOffer(Guid? Id)
        {
            var offerTodelete=Context.Offers.FirstOrDefault(x => x.Id == Id);
            if(offerTodelete != null)
            {
                Context.Offers.Remove(offerTodelete);
                Context.SaveChanges();
                return RedirectToAction("Index");

            }
            return Content("Content is not available!");
        }

    }
}
