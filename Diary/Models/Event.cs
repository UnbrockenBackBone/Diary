using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diary.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name_Event { get; set; }
        public DateTime Start_Date_Event { get; set; }
        public DateTime End_Date_Event { get; set; }
    }
}
