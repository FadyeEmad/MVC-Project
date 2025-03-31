using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult ShowAll()
        {
            StudentsBL studentsBL = new StudentsBL();
            List<Students> ListOfStudents = studentsBL.GetAll();
            return View("ShowAll" , ListOfStudents);
        }
        public IActionResult ShowDetails(int id)
        {
            StudentsBL studentsBL = new StudentsBL();
            Students student = studentsBL.GetById(id);
            return View("ShowDetails", student);
        }
    }
}
