using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class BindController : Controller
    {
        // /Bind/Test?name=Fares&Age=11
        // /Bind/Test?name=Fares&Age=11&id=4
        // /Bind/Test/8?name=Fares&Age=11&id=4  ==> id will be (8)

        public IActionResult Test(string name , int age , int id)
        {
            return Content("OK");
        }
        // /Bind/TestObj?id=5&name=SD&MgrName=Fares
        public IActionResult TestObj(Departments Dept)
        {
            return Content("OK");
        }
    }
}
