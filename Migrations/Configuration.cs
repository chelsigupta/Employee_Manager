namespace EmployeeManager.Migrations
{
    using EmployeeManager.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<EmployeeManager.DAL.EmployeeManagerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EmployeeManager.DAL.EmployeeManagerContext context)
        {
            var departments = new List<Department>
            {
                new Department { Name = "Utilities"},
                new Department { Name = "Services"},
                new Department { Name = "PowerDialer"},
                new Department { Name = "PlayBooks"},
                new Department { Name = "Asana"}
            };
            departments.ForEach(d => context.Departments.AddOrUpdate(p => p.Name, d));
            context.SaveChanges();

            var positions = new List<Position>
            {
                new Position { Name = "Java Engineer"},
                new Position { Name = "C# Engineer"},
                new Position { Name = "DevOps Engineer"},
                new Position { Name = "Systems Engineer"},
                new Position { Name = "Lead Software Engineer"}
            };
            positions.ForEach(po => context.Positions.AddOrUpdate(p => p.Name, po));
            context.SaveChanges();

            var employees = new List<Employee>
            {
                new Employee
                {
                    Name = "None", StartDate = new DateTime(2008,5,1),
                    EndDate = new DateTime(2020,6,4)
                },

                new Employee
                {
                    Name = "Carson Alexander", Address = "#1 485N 400E", Color = "Yellow",
                    Email = "alexcarson@ymail.com",
                    Shift = Shift.EVENING, Status = Status.ACTIVE,
                    Photo = "vvvv", PhoneNumber = "4352588932",
                    StartDate = new DateTime(2008,5,1), EndDate = new DateTime(2020,6,4),
                    Department = departments[1], Position = positions[1], Permission = Permission.ADMIN
                },

                new Employee
                {
                    Name = "Trevor Noah", Address = "#31 460N 400E", Color = "Red",
                    Email = "trevornoah@ymail.com",
                    Shift = Shift.MORNING, Status = Status.TERMINATED,
                    Photo = "xxxx", PhoneNumber = "4789320347",
                    StartDate = new DateTime(2004,4,2), EndDate = new DateTime(2010,5,3),
                    Department = departments[2], Position = positions[2], Permission = Permission.USER
                },

                new Employee
                {
                    Name = "Carl Marx", Address = "#41 4560N 500E", Color = "Green",
                    Email = "marxcarl@gmail.com",
                    Shift = Shift.EVENING, Status = Status.ACTIVE,
                    Photo = "yyyy", PhoneNumber = "4346781932",
                    StartDate = new DateTime(2014,3,5), EndDate = new DateTime(2023,6,2),
                    Department = departments[3], Position = positions[2], Permission = Permission.ADMIN
                },

                new Employee
                {
                    Name = "Andrew Smith", Address = "#71 2000N 300E", Color = "Blue",
                    Email = "smithandrew@ymail.com",
                    Shift = Shift.MORNING, Status = Status.TERMINATED,
                    Photo = "zzzz", PhoneNumber = "5438921092",
                    StartDate = new DateTime(2006,3,2), EndDate = new DateTime(2011,4,2),
                    Department = departments[0], Position = positions[2], Permission = Permission.USER
                },

                new Employee
                {
                    Name = "Maria Johnson", Address = "#3 4600N 500E", Color = "Pink",
                    Email = "jonmaria@gmail.com",
                    Shift = Shift.EVENING, Status = Status.ACTIVE,
                    Photo = "cccc", PhoneNumber = "2347523456",
                    StartDate = new DateTime(2018,4,1), EndDate = new DateTime(2025,7,2),
                    Department = departments[4], Position = positions[0], Permission = Permission.USER
                }
            };
            employees[5].Manager = employees[4];
            employees[4].Manager = employees[3];
            employees[3].Manager = employees[1];
            employees[2].Manager = employees[1];
            employees[1].Manager = employees[0];
            employees.ForEach(e => context.Employees.AddOrUpdate(p => p.Name, e));
            context.SaveChanges();

            var activities = new List<Activity>
            {
                new Activity { Name = "Hired", Timestamp = new DateTime(2008,4,1), Employee = employees[1]},
                new Activity { Name = "Hired", Timestamp = new DateTime(2004,3,2), Employee = employees[2]},
                new Activity { Name = "Terminated", Timestamp = new DateTime(2010,6,3), Employee = employees[2]},
                new Activity { Name = "Hired", Timestamp = new DateTime(2014,2,5), Employee = employees[3]},
                new Activity { Name = "Hired", Timestamp = new DateTime(2006,2,2), Employee = employees[4]},
                new Activity { Name = "Terminated", Timestamp = new DateTime(2011,5,2), Employee = employees[4]},
                new Activity { Name = "Hired", Timestamp = new DateTime(2018,3,1), Employee = employees[5]}
            };
            activities.ForEach(d => context.Activities.AddOrUpdate(p => p.Name, d));
            context.SaveChanges();
        }
    }
}