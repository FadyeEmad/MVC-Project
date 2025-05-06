using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using MVC_Project.Repositories;
using MVC_Project.ViewModels;
using System.Drawing.Printing;

namespace MVC_Project.Controllers
{
    public class CoursesController : Controller
    {
        ICoursesRepository coursesRepository;
        public CoursesController(ICoursesRepository coursesRepository)
        {
            this.coursesRepository = coursesRepository;
        }


        public IActionResult ShowAll(string? searchName , int? pageSize, int page = 1)
        {
            CourseWithStudentViewModel CF = coursesRepository.GetAllWithFilteration(searchName, pageSize, page);

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
                coursesRepository.SaveAdd(CourseFromReq);
                return RedirectToAction("ShowAll");
            }
            return View("Add",CourseFromReq);
        }
        public IActionResult WarningDelete(int id)
        {
            Courses DeleteCourse = coursesRepository.GetByID(id);
            if (DeleteCourse != null)
            {
                return View("WarningDelete", DeleteCourse);
            }
            return View("ShowAll");
        }
        public IActionResult SaveDelete(Courses CourseFromReq)
        {
            if (CourseFromReq.Name != null)
            {
                coursesRepository.SaveDelete(CourseFromReq);
            }
            return RedirectToAction(nameof(ShowAll));
        }
        public IActionResult StudentGrade(string? searchName, int? CourseId, int? pageSize, int page = 1)
        {
            CourseWithStudentViewModel CF = coursesRepository.StudentGradeInfo(searchName, CourseId, pageSize, page);

            return View("StudentGrade" , CF);
        }

        public IActionResult Edit(int id)
        {
            
            Courses oldCourse = coursesRepository.GetByID(id);
            return View("Edit", oldCourse);
        }
        public IActionResult SaveEdit(Courses CourseFromReq)
        {
            if (CourseFromReq.Name!=null)
            {
                coursesRepository.SaveEdit(CourseFromReq);
              return  RedirectToAction(nameof(ShowAll));
            }
            else
            {
            return View("Edit" , CourseFromReq);
            }
        }
    }
}
