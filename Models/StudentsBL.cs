namespace MVC_Project.Models
{
    public class StudentsBL
    {
        SystemDbContext context = new SystemDbContext();
        public List <Students> GetAll()
        {
           List<Students> StudentsList = context.Students.ToList();
           return StudentsList;
        }
        public Students GetById(int id)
        {
            return context.Students.FirstOrDefault(s => s.Id == id);
        }
    }
}
