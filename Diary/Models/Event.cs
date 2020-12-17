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
        public string Start_Date_Event { get; set; }
        public string End_Date_Event { get; set; }
    }
}
