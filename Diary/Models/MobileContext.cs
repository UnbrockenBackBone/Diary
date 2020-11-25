using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diary.Models
{
    public class MobileContext: DbContext
    {
        public DbSet<Employee> Employee { get; set; }

       
    }
}
