using Microsoft.AspNetCore.Mvc;
using SiteCoreLibraryTask.Models;
using SiteCoreLibraryTask.Services;

namespace SiteCoreLibraryTask.Controllers
{
    public class RentalController : Controller
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        public IActionResult Index()
        {
            return View();
        }



        public IActionResult CreateRental()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRental(Rental obj)
        {
            if (ModelState.IsValid)
            {
                Boolean result = _rentalService.CreateRental(obj);
                if(result)
                {
                    return RedirectToAction("Index");
                }else
                {
                    ViewBag.Message = string.Format("User does not exist. Please register before renting a book");
                    return View(obj);
                }
                
            }

            return View(obj);
        }
    }
}
