using EmployeeManager.Models;
using System;
using System.Collections.Generic;

namespace EmployeeManager.Services
{
    public interface IDepartmentService : IDisposable
    {
        IEnumerable<Department> GetDepartments();
        Department GetDepartmentByID(int id);
        void InsertDepartment(Department department);
        void Save();

    }
}