using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Diary.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        [Authorize(Roles = "Moderator")]
        [HttpGet]
        public IActionResult Show()
        {
            return View(db.Employee.ToList());
        }
        [Authorize(Roles = "Moderator")]
        [HttpGet]
        public IActionResult PagePeople(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.UserId = id;
            return View(db.Employee.ToList());
        }
        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public IActionResult PagePeople()
        {
            return View();
        }
        #region Add
        [Authorize(Roles = "Admin, Moderator")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        public IActionResult Add(string Fname, string Lname, string Sname, string Position, string Department, string Photo)
        {
            db.Employee.AddRange(
                 new Employee
                 {
                     Fname = Fname,
                     Lname = Lname,
                     Sname = Sname,
                     Position = Position,
                     Department = Department,
                     Status = 0,
                     Photo = Photo,
                 }); ;
            db.SaveChanges();
            return View();
        }
        #endregion

        [Authorize(Roles = "Admin, Moderator")]
        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.UserId = id;
            return View(db.Employee.ToList());
        }
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        public IActionResult Update(Employee employee)
        {
            db.Employee.Update(employee);

            db.SaveChanges();
            return RedirectToAction("Show");
        }

        #region Event
        [HttpGet]
        public IActionResult Event()
        {
           
            return View(db.Employee.ToList());
        }
        [Authorize(Roles = "Moderator")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Event(Event eEvent, Event_Employee eventEmployee)
        {
            db.Event.Update(eEvent);
            db.Event_Employee.Update(eventEmployee);

            db.SaveChanges();
            return RedirectToAction("Show");
        }
        #endregion
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null) return View(db.Employee.ToList());

            db.Employee.Remove(db.Employee.Find(id));
            db.SaveChanges();
            return RedirectToAction("Show");
        }
        [Authorize(Roles = "Admin, Moderator")]
        [HttpGet]
        public IActionResult RedugEmployee()
        {
            return View(db.Employee.ToList());
        }
        [Authorize(Roles = "Moderator")]
        [HttpGet]
        public IActionResult Moder()
        {
            return View(db.Employee.ToList());
        }
    }
}
