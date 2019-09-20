using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeManager.DAL;
using EmployeeManager.Models;

namespace EmployeeManager.Services
{
    public class PositionService : IPositionService, IDisposable
    {
        private EmployeeManagerContext context;
        private bool disposed = false;
        public PositionService(EmployeeManagerContext context)
        {
            this.context = context;
        }
        public IEnumerable<Position> GetPositions()
        {
            return context.Positions.ToList();
        }
        public Position GetPositionByID(int id)
        {
            return context.Positions.Find(id);
        }
        public void InsertPosition(Position position)
        {
            context.Positions.Add(position);
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