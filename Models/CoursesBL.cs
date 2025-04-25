namespace MVC_Project.Models
{
    public class CoursesBL
    {
        SystemDbContext context = new SystemDbContext();
        public List <Courses> GetAll()
        {
            List<Courses> ListCourses = context.Courses.ToList();
            return ListCourses;
        }
        public Courses GetByID(int id)
        {
            Courses course = context.Courses.FirstOrDefault(c=>c.Id==id);
            return course;
        }
    }
}
