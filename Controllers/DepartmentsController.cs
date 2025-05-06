using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using MVC_Project.Repositories;
using MVC_Project.ViewModels;

namespace MVC_Project.Controllers
{
    public class DepartmentsController : Controller
    {
        IDepartmentsRepository departmentsRepository;
        IStudentsRepository studentsRepository;
        SystemDbContext context;
        public DepartmentsController(IDepartmentsRepository departmentsRepository , IStudentsRepository studentsRepository )
        {
            context = new SystemDbContext();
            this.departmentsRepository = departmentsRepository;
            this.studentsRepository = studentsRepository;
        }

        public IActionResult ShowAll()
        {
            DepatmentsWithStudentsInfo DpStudents = departmentsRepository.GetAllWithStudents();
            return View("ShowAll", DpStudents);
        }
        public IActionResult ShowById(int id)
        {
          Departments Deparment = departmentsRepository.GetById(id);
            
            return View("ShowById", Deparment);
        }
        public IActionResult Add()
        {
            return View("Add");
        }
        public IActionResult SaveAdd(Departments DepartmentFromReq)
        {
            if (ModelState.IsValid == true)
            {
                departmentsRepository.SaveAdd(DepartmentFromReq);
                return RedirectToAction("ShowAll");
            }
            return View("Add", DepartmentFromReq);
        }
        public IActionResult WarningDelete(int id)
        {
            Departments DeleteDepartment = departmentsRepository.GetById(id);
            if (DeleteDepartment != null)
            {
                return View("WarningDelete", DeleteDepartment);
            }
            return View("ShowAll");
        }
        public IActionResult SaveDelete(Departments DepartmentFromReq)
        {
            var Department = context.Departments.Find(DepartmentFromReq.Id);
            if (Department != null)
            {
                departmentsRepository.SaveDelete(DepartmentFromReq);
            }
            return RedirectToAction(nameof(ShowAll));
        }
        public IActionResult Edit(int id)
        {
            Departments department = departmentsRepository.GetById(id);
            return View("Edit",department);
        }
        public IActionResult SaveEdit(Departments DepartmentFromReq)
        {
            if (ModelState.IsValid==true)
            {
                departmentsRepository.SaveEdit(DepartmentFromReq);
                return RedirectToAction(nameof(ShowAll));
            }
            else
            {
                return View("Edit",DepartmentFromReq);
            }
        }
    }
}
