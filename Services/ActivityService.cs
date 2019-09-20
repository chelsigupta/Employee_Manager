using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeManager.DAL;
using EmployeeManager.Models;

namespace EmployeeManager.Services
{
    public class ActivityService : IActivityService, IDisposable
    {
        private EmployeeManagerContext context;
        private bool disposed = false;
        public ActivityService(EmployeeManagerContext context)
        {
            this.context = context;
        }
        public IEnumerable<Activity> GetActivityByEmployeeID(int id)
        {
            return context.Activities
                .Where(a => a.Employee.EmployeeID == id)
                .ToList();
        }
        public int GetTerminationsByYear(int year)
        {
            return context.Activities
                .Where(a => a.Timestamp.Year == year)
                .Where(a => a.Name == "Terminated")
                .Count();
        }
        public void InsertActivity(Activity activity)
        {
            context.Activities.Add(activity);
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