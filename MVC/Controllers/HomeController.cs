using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ContactUS()
        {
            return View("ContactUS");
        }
        public IActionResult AboutUs()
        {
            return View("AboutUs");
        }
    }
}
