using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diary.Models
{
    public class SampleData
    {
        public static void Initialize(MobileContext context)
        {
            if (!context.Employee.Any())
            {
                context.Employee.AddRange(
                    new Employee
                    {
                        Fname = "Petro",
                        Lname = "Ivanov",
                        Sname = "Ivanovych",
                        Position = "Manager",
                        Department = "Managment",
                        Event = "",
                        Status = "work",
                        Hourly_Rate = 20,
                        Many_hours_worked = 10,
                        Photo = "https://life.pravda.com.ua/images/doc/e/0/e03b103-albert-724x480.jpg",
                        AdmModUse = 0
                    },
                    new Employee
                    {
                        Fname = "Ivan",
                        Lname = "Ivanov",
                        Sname = "Ivanovych",
                        Position = "Programmer",
                        Department = "Developers",
                        Event = "",
                        Status = "work",
                        Hourly_Rate = 100,
                        Many_hours_worked = 248,
                        Photo = "https://life.pravda.com.ua/images/doc/e/0/e03b103-albert-724x480.jpg",
                        AdmModUse = 0
                    }
                );
                context.SaveChanges();
            }
        }
    }
}