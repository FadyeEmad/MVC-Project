using Microsoft.EntityFrameworkCore;

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
            return context.Students.Include(s=>s.Department).FirstOrDefault(s => s.Id == id);
        }
        public void Add(Students student)
        {
            context.Students.Add(student);
            context.SaveChanges();
        }
        //public void Delete(Students student)
        //{
        //     var student1 = context.Students.Find(student.Id);
        //    if (student1 != null)
        //    {
        //        context.Students.Remove(student1);
        //        context.SaveChanges();
        //    }
        //}
    }
}
