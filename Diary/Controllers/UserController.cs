using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Diary.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diary.Controllers
{
    public class UserController: Controller
    {
        MobileContext db;

        public UserController(MobileContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.User = db.FindEmail(User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value, db);
            return View();
        }
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
                    user = new Employee() { Email = model.Email, Password = model.Password, Fname = model.Fname, Lname = model.Lname, Sname = model.Sname };
                    Role userRole = await db.Roles.FirstOrDefaultAsync(r => r.Name == "User");
                    if (userRole != null)
                        user.Role = userRole;

                    db.Employee.Add(user);
                    await db.SaveChangesAsync();

                    await Authenticate(user);

                    return RedirectToAction("Index", "User");
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

                    return RedirectToAction("Index", "User");
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
