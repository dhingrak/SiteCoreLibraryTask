using Microsoft.AspNetCore.Mvc;
using SiteCoreLibraryTask.Models;
using SiteCoreLibraryTask.Services;

namespace SiteCoreLibraryTask.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Create()
        {
            return View();
        }

        //GET
        public IActionResult Register()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User obj)
        {
            if (ModelState.IsValid)
            {
                _userService.CreateUser(obj);
                return RedirectToAction("UserList");
            }

            return View(obj);

        }


        [HttpGet]
        public IActionResult UserList()
        {
            AllModels model = new AllModels();


            model.userList = _userService.GetUsers().ToList();

            return View(model);
        }
    }
}
