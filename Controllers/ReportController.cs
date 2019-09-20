using EmployeeManager.DAL;
using EmployeeManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManager.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        //GET: Report/TerminationsThisYear
        public ActionResult TerminationsThisYear()
        {
            var context = new EmployeeManagerContext();
            var currentYear = DateTime.Now.Year;
            ViewBag.TerminationCount = new ActivityService(context).GetTerminationsByYear(currentYear);
            return View();
        }

        //GET: Report/Manager
        public ActionResult Action()
        {
            return View();
        }
    }
}