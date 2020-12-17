using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diary.Models
{
    public class Event_Employee
    {
        [Key]
        public int Id { get; set; }
        public int Id_Event { get; set; }
        public int Id_Employee { get; set; }
    }
}
