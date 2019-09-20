using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using EmployeeManager.Models;
using EmployeeManager.DAL;

namespace EmployeeManager.Services
{
    public class DepartmentService : IDepartmentService, IDisposable
    {
        private EmployeeManagerContext context;
        private bool disposed = false;
        public DepartmentService(EmployeeManagerContext context)
        {
            this.context = context;
        }
        public IEnumerable<Department> GetDepartments()
        {
            return context.Departments
                .Include(d => d.Employees)
                .ToList();
        }
        public Department GetDepartmentByID(int id)
        {
            return context.Departments.Find(id);
        }
        public void InsertDepartment(Department department)
        {
            context.Departments.Add(department);
        }
        public void Save()
        {
            context.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}