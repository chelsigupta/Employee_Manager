using EmployeeManager.Models;
using System;
using System.Collections.Generic;

namespace EmployeeManager.Services
{
    public interface IActivityService : IDisposable
    {
        IEnumerable<Activity> GetActivityByEmployeeID(int id);
        int GetTerminationsByYear(int year);
        void InsertActivity(Activity activity);
        void Save();
    }
}