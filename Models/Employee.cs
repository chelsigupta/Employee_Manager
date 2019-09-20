using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Models
{
    public enum Status
    {
        ACTIVE,TERMINATED
    }
    public enum Shift
    {
        MORNING,EVENING
    }
    public enum Permission
    {
        USER,ADMIN
    }

    public class Employee
    {
        [Required]
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public Position Position { get; set; }
        public Department Department { get; set; }
        public Employee Manager { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set; }
        public Shift Shift { get; set; }
        public string Photo { get; set; }
        public string Color { get; set; }
        public Permission Permission { get; set; }

        public virtual ICollection<Employee> Subordinates { get; set; }
    }
}