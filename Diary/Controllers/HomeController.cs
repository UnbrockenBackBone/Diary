using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Diary.Models;

namespace Diary.Controllers
{
    public class HomeController : Controller
    {
        MobileContext db;
        public HomeController(MobileContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Show()
        {
            return View(db.Employee.ToList());
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(string Fname, string Lname, string Sname, string Position, string Department, int Hourly_Rate, int Many_hours_worked, string Photo)
        {
            db.Employee.AddRange(
                 new Employee
                 {
                     Fname = Fname,
                     Lname = Lname,
                     Sname = Sname,
                     Position = Position,
                     Department = Department,
                     Event = "",
                     Status = "",
                     Hourly_Rate = Hourly_Rate,
                     Many_hours_worked = Many_hours_worked,
                     Photo = Photo,
                    AdmModUse = 0
                 }); ;
            db.SaveChanges();
            return View();
        }
        public IActionResult PagePeople()
        {
            return View();
        }
        public IActionResult RedugEmployee()
        {
            return View();
        }
    }
}
