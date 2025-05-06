using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using MVC_Project.ViewModels;

namespace MVC_Project.Repositories
{
    public class CoursesRepository : ICoursesRepository
    {
        SystemDbContext context;
        public CoursesRepository()
        {
            context = new SystemDbContext();
        }
        public List<Courses> GetAll()
        {
            List<Courses> ListCourses = context.Courses.ToList();
            return ListCourses;
        }
        public CourseWithStudentViewModel GetAllWithFilteration(string? searchName, int? pageSize, int page = 1)
        {
            int finalPageSize = pageSize ?? 10;
            var AllCourses = this.GetAll();
            if (!string.IsNullOrEmpty(searchName))
            {
                AllCourses = AllCourses
                    .Where(s => s.Name.ToLower().Contains(searchName.ToLower()))
                    .ToList();
            }
            int TotalCourses = AllCourses
                .Count();
            var CoursesOnPage = AllCourses
                .Skip((page - 1) * finalPageSize)
                .Take(finalPageSize)
                .ToList();
            CourseWithStudentViewModel CF = new CourseWithStudentViewModel()
            {
                Courses = CoursesOnPage,
                SearchName = searchName,
                CurrentPage = page,
                CourseCount = TotalCourses,
                SelectedPageSize = finalPageSize,
                TotalPages = (int)Math.Ceiling((double)TotalCourses / finalPageSize)
            };
            return CF;
        }
        public Courses GetByID(int id)
        {
            Courses course = context.Courses.FirstOrDefault(c => c.Id == id);
            return course;
        }
        public void SaveAdd(Courses Course)
        {
            context.Add(Course);
            context.SaveChanges();
        }
        public void SaveDelete(Courses Course)
        {
            context.Remove(Course);
            context.SaveChanges();
            int maxId = context.Courses.Any() ? context.Courses.Max(d => d.Id) : 0;
            context.Database.ExecuteSqlRaw($"DBCC CHECKIDENT ('Students', RESEED, {maxId})");
        }
        public void SaveEdit(Courses Course)
        {
            context.Update(Course);
            context.SaveChanges();
        }
        public CourseWithStudentViewModel StudentGradeInfo(string? searchName, int? CourseId, int? pageSize, int page = 1)
        {
            int finalPageSize = pageSize ?? 10;
            var CWS = context.StuCrsRes
                           .Include(sc => sc.Student)
                           .Include(sc => sc.Course)
                           .Select(sc => new StudemtsWithGradeViewModel
                           {
                               CourseId = sc.Courseid,
                               StudentName = sc.Student.Name,
                               Degree = sc.Grade,
                               CourseTitle = sc.Course.Name
                               ,
                               DegreeColor = sc.Grade >= 60 ? "green" : "red"
                           }).ToList(); if (!string.IsNullOrEmpty(searchName))
            {
                CWS = CWS
                    .Where(s => s.StudentName.ToLower().Contains(searchName.ToLower()))
                    .ToList();
            }
            if (CourseId != null && CourseId > 0)
            {
                CWS = CWS
                    .Where(s => s.CourseId == CourseId)
                    .ToList();
            }
            int TotalCourses = CWS
                .Count();
            var CoursesOnPage = CWS
                .Skip((page - 1) * finalPageSize)
                .Take(finalPageSize)
                .ToList();
            CourseWithStudentViewModel CF = new CourseWithStudentViewModel()
            {
                Courses = this.GetAll(),
                Students = CoursesOnPage,
                SearchName = searchName,
                CurrentPage = page,
                CourseCount = TotalCourses,
                SelectedPageSize = finalPageSize,
                TotalPages = (int)Math.Ceiling((double)TotalCourses / finalPageSize)
            };
            return CF;
        }
    }
}
