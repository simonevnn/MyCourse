using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Application;
using MyCourse.Models.ViewModels;

namespace MyCourse.Controllers
{
    public class CoursesController : Controller
    {
        private readonly CourseService courseService;

        public CoursesController(CourseService courseService) 
        {
            this.courseService = courseService; // Dependency Injection per far capire al controller quale servizio applicativo utilizzare
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Catalogo dei corsi";   // In questo caso il titolo della pagina in maniera statica

            List<CourseViewModel> courses = courseService.GetCourses();    // Recuperiamo la lista dei corsi dal Service
                
            return View(courses);  // View() prende la view in automatico dal file system, ma è anche possibile specificare un'altra view ad esempio con View("Detail")
        }

        public IActionResult Detail(int id) 
        {
            CourseDetailViewModel viewModel = courseService.GetCourse(id);
            
            ViewData["Title"] = viewModel.Title;    // Impostiamo il titolo della pagina web con il nome del corso
            
            return View(viewModel);
        }

        public IActionResult Search(string title)
        {
            return Content($"Hai cercato {title}");
        }
    }
}
