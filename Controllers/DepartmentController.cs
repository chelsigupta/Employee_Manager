using System.Data;
using System.Web.Mvc;
using EmployeeManager.DAL;
using EmployeeManager.Models;
using EmployeeManager.Services;

namespace EmployeeManager.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentService departmentService;
        public DepartmentController()
        {
            this.departmentService = new DepartmentService(new EmployeeManagerContext());
        }
        public DepartmentController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        // GET: Department
        public ActionResult Index()
        {
            var departments = departmentService.GetDepartments();
            return View(departments);
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "DepartmentId")] Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    departmentService.InsertDepartment(department);
                    departmentService.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(department);
        }

        protected override void Dispose(bool disposing)
        {
            departmentService.Dispose();
            base.Dispose(disposing);
        }
    }
}
