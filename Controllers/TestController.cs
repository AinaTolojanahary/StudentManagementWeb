using Microsoft.AspNetCore.Mvc;

namespace StudentManagementWeb.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return Content("Test page works!");
        }
    }
}