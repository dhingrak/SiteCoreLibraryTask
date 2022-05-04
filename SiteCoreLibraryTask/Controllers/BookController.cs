using Microsoft.AspNetCore.Mvc;
using SiteCoreLibraryTask.Models;
using SiteCoreLibraryTask.Services;

namespace SiteCoreLibraryTask.Controllers
{
    public class BookController : Controller
    {

        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }


        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book obj)
        {
            if (ModelState.IsValid)
            {
                _bookService.CreateBook(obj);
                return RedirectToAction("BookList");
            }

            return View(obj);
            
        }


        [HttpGet]
        public IActionResult BookList()
        {
            AllModels model = new AllModels();
           

            model.bookList = _bookService.GetBooks().ToList();

            return View(model);
        }
    }
}
