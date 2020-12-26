using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diary.Models;

namespace Diary.ViewModels
{
    public class EventViewModel
    {
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<Event_Employee> Event_Employees {get;set;}
    }
}
