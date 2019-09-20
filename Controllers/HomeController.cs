using System.Web.Mvc;

namespace EmployeeManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Employee Manager";

            return View();
        }
    }
}