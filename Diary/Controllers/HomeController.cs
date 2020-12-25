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

        #region Update
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
        #endregion

        #region Login,Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Employee user = await db.Employee.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    user = new Employee() { Email = model.Email, Password = model.Password, Fname = model.Fname, Lname = model.Lname, Sname = model.Sname};
                    Role userRole = await db.Roles.FirstOrDefaultAsync(r => r.Name == "User");
                    if (userRole != null)
                        user.Role = userRole;

                    db.Employee.Add(user);
                    await db.SaveChangesAsync();

                    await Authenticate(user); 

                    return RedirectToAction("Account", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Employee user = await db.Employee
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user); 

                    return RedirectToAction("Account", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        private async Task Authenticate(Employee user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        #endregion

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
        [HttpGet]
        public IActionResult Account()
        {

            ViewBag.User = db.FindEmail(User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value, db);
            return View();
        }
        [HttpGet]
        public IActionResult Events()
        {
            return View(db.Event.ToList());
        }
        [HttpGet]
        public IActionResult Salary(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.Id = id;
            return View(db.Salary.ToList());
        }
    }
}
