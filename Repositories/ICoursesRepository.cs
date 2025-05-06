using MVC_Project.Models;
using MVC_Project.ViewModels;

namespace MVC_Project.Repositories
{
    public interface ICoursesRepository
    {
        public List<Courses> GetAll();

        public CourseWithStudentViewModel GetAllWithFilteration(string? searchName, int? pageSize, int page = 1);
        public Courses GetByID(int id);

        public void SaveAdd(Courses Course);

        public void SaveDelete(Courses Course);

        public void SaveEdit(Courses Course);

        public CourseWithStudentViewModel StudentGradeInfo(string? searchName, int? CourseId, int? pageSize, int page = 1);

    }
}
