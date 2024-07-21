using MyCourse.Models.ViewModels;

namespace MyCourse.Models.Services.Application
{
    /*
     * Serve creare un'interfaccia per poter accopiare in maniera più debole il service e il controller
     */
    public interface ICourseService
    {
        Task<List<CourseViewModel>> GetCoursesAsync(); // Queste proprietà sono implicitamente public
        Task<CourseDetailViewModel> GetCourseAsync(int id);
    }
}
