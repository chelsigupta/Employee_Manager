using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using EmployeeManager.DAL;
using EmployeeManager.Models;
using EmployeeManager.Services;
using EmployeeManager.ViewModels;

namespace EmployeeManager.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeService employeeService;
        public EmployeeController()
        {
            this.employeeService = new EmployeeService(new EmployeeManagerContext());
        }
        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        // GET: Employee
        public ViewResult Index(string sortOrder, string searchString)
        {
            var employees = employeeService.GetEmployees().Skip(1);

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "Status_desc" : "Status";
            ViewBag.PositionSortParm = sortOrder == "Position" ? "Position_desc" : "Position";
            ViewBag.DepartmentSortParm = sortOrder == "Department" ? "Department_desc" : "Department";
            ViewBag.ManagerSortParm = sortOrder == "Manager" ? "Manager_desc" : "Manager";

            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(e => e.Name.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "Name_desc":
                    employees = employees.OrderByDescending(e => e.Name);
                    break;
                case "Date":
                    employees = employees.OrderBy(e => e.StartDate);
                    break;
                case "Date_desc":
                    employees = employees.OrderByDescending(e => e.StartDate);
                    break;
                case "Status":
                    employees = employees.OrderBy(e => e.Status);
                    break;
                case "Status_desc":
                    employees = employees.OrderByDescending(e => e.Status);
                    break;
                case "Position":
                    employees = employees.OrderBy(e => e.Position.Name);
                    break;
                case "Position_desc":
                    employees = employees.OrderByDescending(e => e.Position.Name);
                    break;
                case "Department":
                    employees = employees.OrderBy(e => e.Department.Name);
                    break;
                case "Department_desc":
                    employees = employees.OrderByDescending(e => e.Department.Name);
                    break;
                case "Manager":
                    employees = employees.OrderBy(e => e.Manager.Name);
                    break;
                case "Manager_desc":
                    employees = employees.OrderByDescending(e => e.Manager.Name);
                    break;
                default: //Name ascending
                    employees = employees.OrderBy(e => e.Name);
                    break;
            }
            return View(employees);
        }

        // GET: Employee/Details/5
        public ViewResult Details(int id)
        {
            Employee employee = employeeService.GetEmployeeByID(id);
            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            var context = new EmployeeManagerContext();
            var positions = new PositionService(context).GetPositions();
            var departments = new DepartmentService(context).GetDepartments();
            var managers = employeeService.GetEmployees();
            var employeeView = new EmployeeViewModel()
            {
                availablePositions = positions,
                availableDepartments = departments,
                availableManagers = managers
            };
            return View(employeeView);
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Employee, availablePositions, availableDepartments, availableManagers")] EmployeeViewModel employeeView)
        {
            var context = new EmployeeManagerContext();
            var positionService = new PositionService(context);
            var departmentService = new DepartmentService(context);
            var employeeService = new EmployeeService(context);
            employeeView.Employee.Position = positionService.GetPositionByID(employeeView.Employee.Position.PositionID);
            employeeView.Employee.Department = departmentService.GetDepartmentByID(employeeView.Employee.Department.DepartmentID);
            employeeView.Employee.Manager = employeeService.GetEmployeeByID(employeeView.Employee.Manager.EmployeeID);
          
            try
            {
                if (ModelState.IsValid)
                {
                    employeeService.InsertEmployee(employeeView.Employee);
                    employeeService.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(employeeView);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            var context = new EmployeeManagerContext();
            var positions = new PositionService(context).GetPositions();
            var departments = new DepartmentService(context).GetDepartments();
            var managers = employeeService.GetEmployees();
            Employee employee = employeeService.GetEmployeeByID(id);
            var employeeView = new EmployeeViewModel()
            {
                Employee = employee,
                availablePositions = positions,
                availableDepartments = departments,
                availableManagers = managers
            };
            return View(employeeView);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Employee, availablePositions, availableDepartments, availableManagers")] EmployeeViewModel employeeView)
        {
            var context = new EmployeeManagerContext();
            var positionService = new PositionService(context);
            var departmentService = new DepartmentService(context);
            var employeeService = new EmployeeService(context);
            employeeView.Employee.Position = positionService.GetPositionByID(employeeView.Employee.Position.PositionID);
            employeeView.Employee.Department = departmentService.GetDepartmentByID(employeeView.Employee.Department.DepartmentID);
            employeeView.Employee.Manager = employeeService.GetEmployeeByID(employeeView.Employee.Manager.EmployeeID);

            try
            {
                if (ModelState.IsValid)
                {
                    employeeService.UpdateEmployee(employeeView.Employee);
                    employeeService.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }            
            return View(employeeView);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id = 0, bool? saveChangesError=false)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Employee employee = employeeService.GetEmployeeByID(id);            
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {                
                employeeService.DeleteEmployee(id);
                employeeService.Save();
            }
            catch (DataException/* dex */)
            {
                // uncomment dex and log error. 
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        // GET: Employee/Terminate/5
        public ActionResult Terminate(int id = 0, bool? saveChangesError = false)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Termination failed. Try again, and if the problem persists see your system administrator.";
            }
            Employee employee = employeeService.GetEmployeeByID(id);
            return View(employee);
        }

        // POST: Employee/Terminate/5
        [HttpPost, ActionName("Terminate")]
        [ValidateAntiForgeryToken]
        public ActionResult Terminate(int id)
        {
            try
            {
                employeeService.TerminateEmployee(id);
                employeeService.Save();
            }
            catch (DataException/* dex */)
            {
                // uncomment dex and log error. 
                return RedirectToAction("Terminate", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        // GET: Employee/Activity/5
        public ViewResult Activity(int id)
        {
            var context = new EmployeeManagerContext();
            var activities = new ActivityService(context).GetActivityByEmployeeID(id);
            return View(activities);
        }

        protected override void Dispose(bool disposing)
        {
            employeeService.Dispose();
            base.Dispose(disposing);
        }
    }
}
