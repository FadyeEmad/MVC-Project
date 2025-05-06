using MVC_Project.Models;
using MVC_Project.ViewModels;

namespace MVC_Project.Repositories
{
    public interface IDepartmentsRepository
    {
        public List<Departments> GetAll();

        public DepatmentsWithStudentsInfo GetAllWithStudents();

        public Departments GetById(int id);

        public void SaveAdd(Departments Department);

        public void SaveDelete(Departments Department);
        public void SaveEdit(Departments Department);

    }
}
