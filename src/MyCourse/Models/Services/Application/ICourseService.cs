using MyCourse.Models.ViewModels;

namespace MyCourse.Models.Services.Application
{
    /*
     * Serve creare un'interfaccia per poter accopiare in maniera più debole il service e il controller
     */
    public interface ICourseService
    {
        List<CourseViewModel> GetCourses(); // Queste proprietà sono implicitamente public
        CourseDetailViewModel GetCourse(int id);
    }
}
