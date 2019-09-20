using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using EmployeeManager.DAL;

namespace EmployeeManager.Services
{
    public class EmployeeService : IEmployeeService, IDisposable
    {
        private EmployeeManagerContext context;
        private bool disposed = false;
        public EmployeeService(EmployeeManagerContext context)
        {
            this.context = context;
        }
        public IEnumerable<Employee> GetEmployees()
        {
            return context.Employees
                .Include(e => e.Department)
                .Include(e => e.Position)
                .Include(e => e.Manager)
                .ToList();
        }
        public Employee GetEmployeeByID(int id)
        {
            return context.Employees
                .Include(e => e.Department)
                .Include(e => e.Position)
                .Include(e => e.Manager)
                .Where(e => e.EmployeeID == id)
                .First();
        }
        public void InsertEmployee(Employee employee)
        {
            context.Employees.Add(employee);
        }
        public void DeleteEmployee(int id)
        {
            Employee employeeToDelete = new Employee() { EmployeeID = id };
            context.Entry(employeeToDelete).State = EntityState.Deleted;
        }
        public void UpdateEmployee(Employee employee)
        {
            var employeeToUpdate = context.Employees.Find(employee.EmployeeID);
            employeeToUpdate.Position = employee.Position;
            employeeToUpdate.Department = employee.Department;
            employeeToUpdate.Manager = employee.Manager;
            context.Entry(employeeToUpdate).State = EntityState.Modified;
        }
        public void TerminateEmployee(int id)
        {
            Employee employeeToTerminate = context.Employees.Find(id);
            employeeToTerminate.Status = Status.TERMINATED;
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