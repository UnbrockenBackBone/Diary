using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Diary.Models;
using System.Security.Claims;
using Diary.ViewModels;
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
        EventViewModel ev = new EventViewModel();
        public HomeController(MobileContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Events()
        {
            ev.Events = db.Event.ToList();
            ev.Event_Employees = db.Event_Employee.ToList();
            return View(ev);
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
        [Authorize(Roles = "Moderator, Admin")]
        [HttpGet]
        public IActionResult PagePeople(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.UserId = id;
            return View(db.Employee.ToList());
        }
        [Authorize(Roles = "Moderator, Admin")]
        [HttpPost]
        public IActionResult PagePeople()
        {
            return View();
        }
        #region Add
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(string Fname, string Lname, string Sname, string Position, string Department, int Status, int RoleId, string Photo )
        {
            db.Employee.AddRange(
                 new Employee
                 {
                     Fname = Fname,
                     Lname = Lname,
                     Sname = Sname,
                     Position = Position,
                     Department = Department,
                     Status = Status,
                     RoleId = RoleId, 
                     Photo = Photo,
                 }); ;
            db.SaveChanges();
            return Redirect("RedugEmployee");
        }
        #endregion

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.UserId = id;
            return View(db.Employee.ToList());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(Employee employee)
        {
            db.Employee.Update(employee);

            db.SaveChanges();
            return RedirectToAction("RedugEmployee");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null) return View(db.Employee.ToList());

            db.Employee.Remove(db.Employee.Find(id));
            db.SaveChanges();
            return RedirectToAction("RedugEmployee");
        }
        [Authorize(Roles = "Admin, Moderator")]
        [HttpGet]
        public IActionResult RedugEmployee()
        {
            return View(db.Employee.ToList());
        }
        [Authorize(Roles = "Moderator")]
        [HttpGet]
        public IActionResult Event()
        {
            return View();
        }
        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public IActionResult Event(Event eve)
        {
            db.Event.Update(eve);

            db.SaveChanges();
            return Redirect("Show");
        }
    }
}
