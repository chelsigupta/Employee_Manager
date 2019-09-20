using EmployeeManager.Models;
using System.Collections.Generic;

namespace EmployeeManager.ViewModels
{
    public class EmployeeViewModel
    {
        public Employee Employee { get; set; }
        public IEnumerable<Position> availablePositions { get; set; }
        public IEnumerable<Department> availableDepartments { get; set; }
        public virtual IEnumerable<Employee> availableManagers { get; set; }
    }
}