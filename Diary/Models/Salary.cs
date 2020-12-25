using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diary.Models
{
    public class Salary
    {
        [Key]
        public int Id { get; set; }
        public int Id_User { get; set; }
        public int Hourly_rate { get; set; }
        public int Many_hours_worked { get; set; }
        public DateTime Date { get; set; }
    }
}
