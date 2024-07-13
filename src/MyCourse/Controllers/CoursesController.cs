using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Application;
using MyCourse.Models.ViewModels;

namespace MyCourse.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            var courseService = new CourseService();    // Possiamo scrivere direttamente var per evitare di scrivere sempre il tipo

            List<CourseViewModel> courses = courseService.GetServices();    // Recuperiamo la lista dei corsi dal Service
                
            return View(courses);  // View() prende la view in automatico dal file system, ma è anche possibile specificare un'altra view ad esempio con View("Detail")
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
