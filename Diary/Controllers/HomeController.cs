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
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Views()
        {
            return View();
        }

        public IActionResult ShowMagazine()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddEmployee(string Fname,
            string Lname,
            string Sname,
            string Position,
            string Department,
            string Event,
            string Status,
            int Hourly_Rate,
            int Many_hours_worked,
            string Foto)
        {
            return Redirect("Index");
        }
        public IActionResult RedugEmployee()
        {
            return View();
        }
        public IActionResult PagePeople()
        {
            return View();
        }

    }
}
