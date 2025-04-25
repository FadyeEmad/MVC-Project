using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public void SaveAdd(Students student)
        {
            context.Students.Add(student);
            context.SaveChanges();
        }
        public void SaveDelete(Students student)
        {
            var studentDP = context.Students.Find(student.Id);
            if (studentDP != null)
            {
                context.Students.Remove(studentDP);
                context.SaveChanges();
            }
        }
        public void SaveEdit(Students student)
        {
           var studentDP = context.Students.Find(student.Id);
            if (studentDP != null)
            {
                studentDP.Age = student.Age;
                studentDP.Name = student.Name;
                studentDP.Department = student.Department;
                context.SaveChanges();
            }
        }

    }
}
