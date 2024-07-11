using Microsoft.AspNetCore.Mvc;

namespace MyCourse.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            return View();  // Prende la view in automatico dal file system, ma è anche possibile specificare un'altra view ad esempio con View("Detail")
        }

        public IActionResult Detail(string id) 
        {
            return View();
        }

        public IActionResult Search(string title)
        {
            return Content($"Hai cercato {title}");
        }
    }
}
