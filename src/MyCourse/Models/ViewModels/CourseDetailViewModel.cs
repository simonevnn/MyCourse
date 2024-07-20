namespace MyCourse.Models.ViewModels
{
    public class CourseDetailViewModel : CourseViewModel    // Sfruttiamo l'ereditarietà per aggiungere altri elementi nella pagina di dettaglio
    {
        public string Description { get; set; }
        public List<LessonViewModel> Lessons { get; set; }

        public TimeSpan TotalCourseDuration // Calcoliamo dinamicamente la durata totale
        {
            get => TimeSpan.FromSeconds(Lessons?.Sum(l => l.Duration.TotalSeconds) ?? 0); 
        }
    }
}
