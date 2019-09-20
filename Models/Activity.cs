using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Models
{
    public class Activity
    {
        public int ActivityID { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }

        [Required]
        public Employee Employee { get; set; }
    }
}