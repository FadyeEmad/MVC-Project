using MVC_Project.Models;

namespace MVC_Project.Repositories
{
    public interface IStudentsRepository
    {
        public List<Students> GetAll();

        public Students GetById(int id);

        public void SaveAdd(Students student);

        public void SaveDelete(Students student);

        public void SaveEdit(Students student);
   
    }
}
