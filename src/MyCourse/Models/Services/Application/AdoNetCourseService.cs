using MyCourse.Models.Services.Infrastructure;
using MyCourse.Models.ViewModels;
using System.Data;

namespace MyCourse.Models.Services.Application
{
    public class AdoNetCourseService : ICourseService
    {
        private readonly IDatabaseAccessor db;

        public AdoNetCourseService(IDatabaseAccessor db) 
        {
            this.db = db;
        }

        public CourseDetailViewModel GetCourse(int id)
        {
            FormattableString query = @$"
                SELECT Id, Title, Description,ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency
                FROM Courses
                WHERE Id={id};
                SELECT Id, Title, Description, Duration
                FROM Lessons
                WHERE CourseId={id}
            ";

            DataSet dataSet = db.Query(query);

            // CORSI
            var courseTable = dataSet.Tables[0];
            
            if (courseTable.Rows.Count != 1)
                throw new InvalidOperationException($"Did not return exactly 1 row for Course {id}");

            var courseRow = courseTable.Rows[0];
            var courseDetailViewModel = CourseDetailViewModel.FromDataRow(courseRow);

            // LEZIONI
            var lessonDataTable = dataSet.Tables[1];    // Tramite l'array Tables prendiamo la seconda tabella del risultato (quella delle lezioni), questo appunto perché abbiamoe eseguito più di una query nel comando

            foreach(DataRow lessonRow in lessonDataTable.Rows)
            {
                LessonViewModel lessonViewModel = LessonViewModel.FromDataRow(lessonRow);
                courseDetailViewModel.Lessons.Add(lessonViewModel);
            }

            return courseDetailViewModel;
        }

        public List<CourseViewModel> GetCourses()
        {
            FormattableString query = @$"
                SELECT Id, Title, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency
                FROM Courses
            ";

            DataSet dataSet = db.Query(query);

            var dataTable = dataSet.Tables[0];
            var courseList = new List<CourseViewModel>();

            foreach(DataRow courseRow in dataTable.Rows)
            {
                var course = CourseViewModel.FromDataRow(courseRow);    // La logica di mapping viene inserita come metodo statico nella classe ViewModel
                courseList.Add(course);
            }

            return courseList;
        }
    }
}
