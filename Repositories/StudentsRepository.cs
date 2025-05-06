using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;

namespace MVC_Project.Repositories
{
    public class StudentsRepository : IStudentsRepository
    {
        SystemDbContext context;

        public StudentsRepository()
        {
            context = new SystemDbContext();
        }
        public List<Students> GetAll()
        {
            List<Students> StudentsList = context.Students.ToList();
            return StudentsList;
        }
        public Students GetById(int id)
        {
            return context.Students.Include(s => s.Department).FirstOrDefault(s => s.Id == id);
        }
        public void SaveAdd(Students student)
        {
            context.Students.Add(student);
            context.SaveChanges();
        }
        public void SaveDelete(Students student)
        {
            context.Students.Remove(student);
            context.SaveChanges();
            int maxId = context.Students.Any() ? context.Students.Max(d => d.Id) : 0;
            context.Database.ExecuteSqlRaw($"DBCC CHECKIDENT ('Students', RESEED, {maxId})");
        }
        public void SaveEdit(Students student)
        {
            context.Update(student);
            context.SaveChanges();
        }
    }
}

