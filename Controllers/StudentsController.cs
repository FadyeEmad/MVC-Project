using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using MVC_Project.ViewModels;

namespace MVC_Project.Controllers
{
    public class StudentsController : Controller
    {
        DepartmentsBL departmentsBL = new DepartmentsBL();
            StudentsBL studentsBL = new StudentsBL();
        SystemDbContext context = new SystemDbContext();
        //Students/ShowAll
        //public IActionResult ShowAll()
        //{
        //    List<Students> ListOfStudents = studentsBL.GetAll();
        //    return View("ShowAll" , ListOfStudents);
        //}
        public IActionResult ShowAll(string? searchName, int? departmentId, int page = 1)
        {
            int pageSize = 5;
            var allStudents = studentsBL.GetAll();
            if (!string.IsNullOrEmpty(searchName))
            {
                allStudents = allStudents
                    .Where(s => s.Name.ToLower().Contains(searchName.ToLower()))
                    .ToList();
            }
            if (departmentId != null && departmentId > 0)
            {
                allStudents =allStudents
                    .Where(s=>s.Departmentid == departmentId)
                    .ToList();
            } 
            int TotalStudents = allStudents
                .Count();
            var StudentsOnPage = allStudents
                .Skip((page-1)*pageSize)
                .Take(pageSize)
                .ToList();
            StudentFilterViewModel SF = new StudentFilterViewModel()
            {
                Students = StudentsOnPage,
                SearchName = searchName,
                DepartmentId = departmentId,
                Departments = departmentsBL.GetAll(),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)TotalStudents / pageSize)
            };
            return View("ShowAll",SF);
                
        }

        //Students/ShowDetailsWM?id=1
        public IActionResult ShowDetails(int id)
        {
            int Rate = 58;
            string status = "C";
            List <string> Skills =new List<string>() { "Leadership" , "Communication"};
            ViewData["status"] = status;
            ViewData["Rate"] = Rate;
            ViewData["Skills"] = Skills;
            Students student = studentsBL.GetById(id);
            return View("ShowDetails", student);
        }
        //Students/DetailsVW?id=1
        public IActionResult DetailsVW(int id)
        {
            Students StuModel = studentsBL.GetById(id);
            int Rate = 58;
            string status = "C";
            List<string> Skills = new List<string>() { "Leadership", "Communication" };
            StudentWithMoreInfoViewModel SWM = new StudentWithMoreInfoViewModel();
            SWM.Rate = Rate;
            SWM.Status = status;
            SWM.Skills= Skills;
            SWM.DeptName = StuModel.Department.Name;
            SWM.StudentName = StuModel.Name;
            return View ("DetailsVW", SWM);
        }
        [HttpGet]
        public IActionResult Add()
        {
            StudentWithDepartmentsList SWDT = new StudentWithDepartmentsList();
            List<Departments> DepartmentsList = departmentsBL.GetAll();
            SWDT.DepartmentLiat = DepartmentsList;
            return View("Add" ,SWDT);
        }
        [HttpPost]
        public IActionResult SaveAdd(Students StudentFromReq)
        {
            if(StudentFromReq.Name != null && StudentFromReq.Age != 0 )
            {
                studentsBL.SaveAdd(StudentFromReq);
               return RedirectToAction(nameof(ShowAll));
            }
            return View("Add" , StudentFromReq);
        }
        public IActionResult Delete(Students StudentFromReq)
        {
            studentsBL.SaveDelete(StudentFromReq);
            return RedirectToAction(nameof(ShowAll));
        }
        public IActionResult Edit(int id)
        {
            StudentWithDepartmentsList SWDT = new StudentWithDepartmentsList();
            Students oldstudent = studentsBL.GetById(id);
            List<Departments> DepartmentsList = departmentsBL.GetAll() ;
            SWDT.Age = oldstudent.Age;
            SWDT.Name = oldstudent.Name;
            SWDT.Department = oldstudent.Department;
            SWDT.Departmentid = oldstudent.Departmentid;
            SWDT.StuCrsRes = oldstudent.StuCrsRes;
            SWDT.Id = oldstudent.Id;
            SWDT.DepartmentLiat = DepartmentsList;

            return View("Edit" , SWDT);
        }
        public IActionResult SaveEdit (StudentWithDepartmentsList StudentFromReq , int id)
        {
            if (StudentFromReq.Name != null)
            {
                Students EditStudent = studentsBL.GetById(id);
                EditStudent.Name = StudentFromReq.Name;
                EditStudent.Age = StudentFromReq.Age;
                EditStudent.Departmentid = StudentFromReq.Departmentid;
                studentsBL.SaveEdit(EditStudent);
                return RedirectToAction(nameof(ShowAll));
            }
            List<Departments> DepartmentsList = departmentsBL.GetAll();
            StudentFromReq.DepartmentLiat = DepartmentsList;
            return View("Edit", StudentFromReq);
        }
        public IActionResult WarningDelete (int id)
        {
            Students DeleteStudent = studentsBL.GetById(id);
            return View("WarningDelete", DeleteStudent);
        }
    }
}
