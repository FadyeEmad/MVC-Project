using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using MVC_Project.ViewModels;

namespace MVC_Project.Repositories
{
    public class DepartmentsRepository : IDepartmentsRepository
    {
        SystemDbContext context;
        DepatmentsWithStudentsInfo DbStudents;
        public DepartmentsRepository()
        {
            context = new SystemDbContext();
            DbStudents = new DepatmentsWithStudentsInfo();
        }
        

        public List<Departments> GetAll()
        {
            List<Departments> DepartmentsList = context.Departments.ToList();
            return DepartmentsList;
        }
        public DepatmentsWithStudentsInfo GetAllWithStudents()
        {
            List<Departments> DepartmentsList = context.Departments.ToList();
            List<Students> studentsList = context.Students.ToList();
            DbStudents.Students = studentsList;
            DbStudents.Departments = DepartmentsList;
            return DbStudents;
        }
        public Departments GetById(int id)
        {
            Departments department = context.Departments.FirstOrDefault(x => x.Id == id);
            List<Students> studentsList = context.Students.Where(D => D.Departmentid == id).ToList();
            department.Students = studentsList;
            return department;
        }
        public void SaveAdd(Departments Department)
        {
            context.Add(Department);
            context.SaveChanges();
        }
        public void SaveDelete(Departments Department)
        {
            context.Remove(Department);
            context.SaveChanges();
            int maxId = context.Departments.Any() ? context.Departments.Max(d => d.Id) : 0;
            context.Database.ExecuteSqlRaw($"DBCC CHECKIDENT ('Students', RESEED, {maxId})");
        }
        public void SaveEdit(Departments Department)
        {
            context.Update(Department);
            context.SaveChanges();
        }

    }
}
