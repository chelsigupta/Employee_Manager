using EmployeeManager.Models;
using System;
using System.Collections.Generic;

namespace EmployeeManager.Services
{
    public interface IPositionService : IDisposable
    {
        IEnumerable<Position> GetPositions();
        Position GetPositionByID(int id);
        void InsertPosition(Position position);
        void Save();
    }
}