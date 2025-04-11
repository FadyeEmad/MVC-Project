using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using MVC_Project.ViewModels;

namespace MVC_Project.Controllers
{
    public class DepartmentsController : Controller
    {
        DepartmentsBL departmentsBL = new DepartmentsBL();
        StudentsBL studentsBL = new StudentsBL();
        public IActionResult ShowAll()
        {
            
            DepatmentsWithStudentsInfo DpStudents = departmentsBL.GetAll();
            //DpStudents.Departments = DeparmentsList;
            //DpStudents.Students=studentsBL.GetAll();
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
    }
}
