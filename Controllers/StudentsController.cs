using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;
using MVC_Project.ViewModels;

namespace MVC_Project.Controllers
{
    public class StudentsController : Controller
    {
            StudentsBL studentsBL = new StudentsBL();
        //Students/ShowAll
        public IActionResult ShowAll()
        {
            List<Students> ListOfStudents = studentsBL.GetAll();
            return View("ShowAll" , ListOfStudents);
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
            return View("Add");
        }
        [HttpPost]
        public IActionResult SaveAdd(Students StudentFromReq)
        {
            if(StudentFromReq.Name != null && StudentFromReq.Age != 0)
            {
                studentsBL.Add(StudentFromReq);
               return RedirectToAction(nameof(ShowAll));
            }
            return View("Add" , StudentFromReq);
        }
        //public IActionResult Delete(Students StudentFromReq)
        //{
        //    studentsBL.Delete(StudentFromReq);
        //    return RedirectToAction(nameof(ShowAll));

        //}
    }
}
