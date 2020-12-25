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
                        Status = 1,
                        Photo = "https://life.pravda.com.ua/images/doc/e/0/e03b103-albert-724x480.jpg"
                    },
                    new Employee
                    {
                        Fname = "Ivan",
                        Lname = "Ivanov",
                        Sname = "Ivanovych",
                        Position = "Programmer",
                        Department = "Developers",
                        Status = 1,
                        Photo = "https://life.pravda.com.ua/images/doc/e/0/e03b103-albert-724x480.jpg"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}