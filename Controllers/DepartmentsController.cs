using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using MVC_Project.ViewModels;

namespace MVC_Project.Controllers
{
    public class DepartmentsController : Controller
    {
        DepartmentsBL departmentsBL = new DepartmentsBL();
        StudentsBL studentsBL = new StudentsBL();
        SystemDbContext context= new SystemDbContext();
        public IActionResult ShowAll()
        {
            DepatmentsWithStudentsInfo DpStudents = departmentsBL.GetAllWithStudents();
            return View("ShowAll", DpStudents);
        }
        public IActionResult ShowById(int id)
        {
          Departments Deparment = departmentsBL.GetById(id);
            //DepatmentsWithStudentsInfo DpStudents = new DepatmentsWithStudentsInfo();
            //DpStudents.Students=studentsBL.GetAll();
            //DpStudents.MgrName=Deparment.MgrName;
            //DpStudents.Name=Deparment.Name;
            //DpStudents.Id=Deparment.Id;

            
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
                context.Add(DepartmentFromReq);
                context.SaveChanges();
                return RedirectToAction("ShowAll");
            }
            return View("Add", DepartmentFromReq);
        }
        public IActionResult WarningDelete(int id)
        {
            Departments DeleteDepartment = departmentsBL.GetById(id);
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
                context.Departments.Remove(Department);
                context.SaveChanges();
                int maxId = context.Departments.Any() ? context.Departments.Max(d => d.Id) : 0;
                context.Database.ExecuteSqlRaw($"DBCC CHECKIDENT ('Departments', RESEED, {maxId})");
            }
            return RedirectToAction(nameof(ShowAll));
        }
        public IActionResult Edit(int id)
        {
            Departments department = departmentsBL.GetById(id);
            return View("Edit",department);
        }
        public IActionResult SaveEdit(Departments DepartmentFromReq)
        {
            Departments OldDepartment= context.Departments.Find(DepartmentFromReq.Id);
            if (ModelState.IsValid==true)
            {
                OldDepartment.Name = DepartmentFromReq.Name;
                OldDepartment.MgrName = DepartmentFromReq.MgrName;
                context.SaveChanges();
                return RedirectToAction(nameof(ShowAll));
            }
            else
            {
                return View("Edit",DepartmentFromReq);
            }
        }
    }
}
