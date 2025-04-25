using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using MVC_Project.ViewModels;
using System.Drawing.Printing;

namespace MVC_Project.Controllers
{
    public class CoursesController : Controller
    {
        SystemDbContext context = new SystemDbContext();
        CoursesBL CoursesBL = new CoursesBL();
        StudentsBL studentsBL = new StudentsBL();

        public IActionResult ShowAll(string? searchName , int? pageSize, int page = 1)
        {
            int finalPageSize = pageSize ?? 10;
            var AllCourses = CoursesBL.GetAll();
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
            return View("ShowAll", CF);

        }
        public IActionResult Add()
        {
            return View("Add");
        }
        public IActionResult SaveAdd( Courses CourseFromReq)
        {
            if (ModelState.IsValid==true)
            {
                context.Courses.Add(CourseFromReq);
                context.SaveChanges();
                return RedirectToAction("ShowAll");
            }
            return View("Add",CourseFromReq);
        }
        public IActionResult WarningDelete(int id)
        {
            Courses DeleteCourse = CoursesBL.GetByID(id);
            if (DeleteCourse != null)
            {
                return View("WarningDelete", DeleteCourse);
            }
            return View("ShowAll");
        }
        public IActionResult SaveDelete(Courses CourseFromReq)
        {
            var Course = context.Courses.Find(CourseFromReq.Id);
            if (Course != null)
            {
                context.Courses.Remove(Course);
                context.SaveChanges();
                int maxId = context.Courses.Any() ? context.Courses.Max(c => c.Id) : 0;
                context.Database.ExecuteSqlRaw($"DBCC CHECKIDENT ('Courses', RESEED, {maxId})");
            }
            return RedirectToAction(nameof(ShowAll));
        }
        public IActionResult StudentGrade(string? searchName, int? CourseId, int? pageSize, int page = 1)
        {

            int finalPageSize = pageSize ?? 10;
            var CWS = context.StuCrsRes
                           .Include(sc => sc.Student)
                           .Include(sc => sc.Course)
                           .Select(sc => new StudemtsWithGradeViewModel
                           {
                               CourseId=sc.Courseid,
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
                Courses = CoursesBL.GetAll(),
                Students = CoursesOnPage,
                SearchName = searchName,
                CurrentPage = page,
                CourseCount = TotalCourses,
                SelectedPageSize = finalPageSize,
                TotalPages = (int)Math.Ceiling((double)TotalCourses / finalPageSize)

            };

            return View("StudentGrade" , CF);
        }

        public IActionResult Edit(int id)
        {
            
            Courses oldCourse = CoursesBL.GetByID(id);
            return View("Edit", oldCourse);
        }
        public IActionResult SaveEdit(Courses CourseFromReq)
        {
           Courses OldCourse= context.Courses.Find(CourseFromReq.Id);
            if (CourseFromReq.Name!=null)
            {
                OldCourse.Name = CourseFromReq.Name;
                OldCourse.MinDegree = CourseFromReq.MinDegree;
                OldCourse.Degree = CourseFromReq.Degree;
                OldCourse.DepartmentId = CourseFromReq.DepartmentId;
                context.SaveChanges();
              return  RedirectToAction(nameof(ShowAll));
            }
            else
            {
            return View("Edit" , CourseFromReq);
            }
        }
    }
}
