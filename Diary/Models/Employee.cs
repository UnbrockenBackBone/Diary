using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diary.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; } 
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Sname { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Event { get; set; }
        public string Status { get; set; }
        public int Hourly_Rate { get; set; }
        public int Many_hours_worked { get; set; }
        public string Photo { get; set; }
    }
}
