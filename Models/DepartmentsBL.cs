using Microsoft.EntityFrameworkCore;
using MVC_Project.ViewModels;

namespace MVC_Project.Models
{
    public class DepartmentsBL
    {
        SystemDbContext context = new SystemDbContext();
                    DepatmentsWithStudentsInfo DbStudents = new DepatmentsWithStudentsInfo();

        public DepatmentsWithStudentsInfo GetAll()
        {
           List <Departments> DepartmentsList = context.Departments.ToList();
            List<Students> studentsList = context.Students.ToList();
            DbStudents.Students = studentsList;
            DbStudents.Departments = DepartmentsList;
         return DbStudents;
        }
        public Departments GetById(int id)
        {
            Departments department = context.Departments.FirstOrDefault(x => x.Id == id);
            List<Students> studentsList = context.Students.Where(D=>D.Departmentid==id).ToList();
            department.Students= studentsList;
            return department;
        }
    }
}
