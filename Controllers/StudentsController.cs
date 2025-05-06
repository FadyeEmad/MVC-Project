using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using MVC_Project.Repositories;
using MVC_Project.ViewModels;

namespace MVC_Project.Controllers
{
    public class StudentsController : Controller
    {
        IStudentsRepository studentsRepository;
        IDepartmentsRepository departmentsRepository;
        public StudentsController(IStudentsRepository studentsRepository , IDepartmentsRepository departmentsRepository)
        {
            this.departmentsRepository = departmentsRepository;
            this.studentsRepository = studentsRepository;
        }

        //Students/ShowAll
        //public IActionResult ShowAll()
        //{
        //    List<Students> ListOfStudents = studentsBL.GetAll();
        //    return View("ShowAll" , ListOfStudents);
        //}
        public IActionResult ShowAll(string? searchName, int? departmentId, int? pageSize, int page = 1)
        {
            int finalPageSize = pageSize ?? 10;
            var allStudents = studentsRepository.GetAll();
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
                .Skip((page-1)* finalPageSize)
                .Take(finalPageSize)
                .ToList();
            StudentFilterViewModel SF = new StudentFilterViewModel()
            {
                Students = StudentsOnPage,
                SearchName = searchName,
                DepartmentId = departmentId,
                Departments = departmentsRepository.GetAll(),
                CurrentPage = page,
                StudentsCount= TotalStudents,
                SelectedPageSize = finalPageSize,
                TotalPages = (int)Math.Ceiling((double)TotalStudents / finalPageSize)
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
            Students student = studentsRepository.GetById(id);
            return View("ShowDetails", student);
        }
        //Students/DetailsVW?id=1
        public IActionResult DetailsVW(int id)
        {
            Students StuModel = studentsRepository.GetById(id);
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
            List<Departments> DepartmentsList = departmentsRepository.GetAll();
            SWDT.DepartmentList = DepartmentsList;
            return View("Add" ,SWDT);
        }
        [HttpPost]
        public IActionResult SaveAdd(Students StudentFromReq)
        {

            if (ModelState.IsValid== true)
            {
                studentsRepository.SaveAdd(StudentFromReq);
                TempData["NotificationAdded"] = $"Employees With Name {StudentFromReq.Name} and Id {StudentFromReq.Id} Is Added";
                return RedirectToAction(nameof(ShowAll));

            }
            var departments = departmentsRepository.GetAll();
            StudentWithDepartmentsList ReturnStudent = new StudentWithDepartmentsList
            {
                //Students student =new Students();
                StuCrsRes = StudentFromReq.StuCrsRes,
                Age = StudentFromReq.Age,
                Name = StudentFromReq.Name,
                Departmentid = StudentFromReq.Departmentid,
                DepartmentList = departments,

            };
            return View("Add" , ReturnStudent);
        }
        public IActionResult Delete(Students StudentFromReq)
        {
            studentsRepository.SaveDelete(StudentFromReq);
            return RedirectToAction(nameof(ShowAll));
        }
        public IActionResult Edit(int id)
        {
            StudentWithDepartmentsList SWDT = new StudentWithDepartmentsList();
            Students oldstudent = studentsRepository.GetById(id);
            List<Departments> DepartmentsList = departmentsRepository.GetAll() ;
            SWDT.Age = oldstudent.Age;
            SWDT.Name = oldstudent.Name;
            SWDT.Department = oldstudent.Department;
            SWDT.Departmentid = oldstudent.Departmentid;
            SWDT.StuCrsRes = oldstudent.StuCrsRes;
            SWDT.Id = oldstudent.Id;
            SWDT.DepartmentList = DepartmentsList;

            return View("Edit" , SWDT);
        }
        public IActionResult SaveEdit (StudentWithDepartmentsList StudentFromReq , int id)
        {
            if (StudentFromReq.Name != null)
            {
                Students EditStudent = studentsRepository.GetById(id);
                EditStudent.Name = StudentFromReq.Name;
                EditStudent.Age = StudentFromReq.Age;
                EditStudent.Departmentid = StudentFromReq.Departmentid;
                studentsRepository.SaveEdit(EditStudent);
                return RedirectToAction(nameof(ShowAll));
            }
            List<Departments> DepartmentsList = departmentsRepository.GetAll();
            StudentFromReq.DepartmentList = DepartmentsList;
            return View("Edit", StudentFromReq);
        }
        public IActionResult WarningDelete (int id)
        {
            Students DeleteStudent = studentsRepository.GetById(id);
            return View("WarningDelete", DeleteStudent);
        }
    }
}
