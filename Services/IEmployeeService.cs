using System;
using System.Collections.Generic;
using EmployeeManager.Models;

namespace EmployeeManager.Services
{
    public interface IEmployeeService : IDisposable
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployeeByID(int id);
        void InsertEmployee(Employee employee);
        void DeleteEmployee(int id);
        void UpdateEmployee(Employee employee);
        void TerminateEmployee(int id);
        void Save();
    }
}