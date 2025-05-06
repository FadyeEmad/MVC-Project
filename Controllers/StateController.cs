using Microsoft.AspNetCore.Mvc;

namespace MVC_Project.Controllers
{
    public class StateController : Controller
    {
        public IActionResult SetSession(string Name)
        {
            HttpContext.Session.SetString("Name", Name);
            HttpContext.Session.SetInt32("Age", 20);
            return Content("Data Saved");
        }
        public IActionResult GetSession()
        {
           string Name = HttpContext.Session.GetString("Name");
           int? Age = HttpContext.Session.GetInt32("Age");
            return Content($"Name Is {Name} , Age = {Age}");
        }
        public IActionResult SetCookie(string Name  )
        {
            CookieOptions options = new CookieOptions();
            options.Expires= DateTime.Now.AddDays(1);
            HttpContext.Response.Cookies.Append("Name" , Name);
            HttpContext.Response.Cookies.Append("Age" , "20" , options);
            return Content("Cookie Done");
        }
        public IActionResult GetCookie()
        {
            string Name = HttpContext.Request.Cookies["Name"];
           string Age = HttpContext.Request.Cookies["Age"];
            return Content($"Name Is {Name} , Age = {Age}");

        }
    }
}
