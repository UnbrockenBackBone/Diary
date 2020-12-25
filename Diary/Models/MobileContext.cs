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
        public DbSet<Event> Event { get; set; }
        public DbSet<Event_Employee> Event_Employee { get; set; }
        public DbSet<Salary> Salary { get; set; }

        public DbSet<Role> Roles { get; set; }
        public MobileContext(DbContextOptions<MobileContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public Employee FindEmail(string email, MobileContext db)
        {
            foreach (Employee item in db.Employee)
            {
                if (email == item.Email)
                {
                    return item;
                }
            }

            return null;
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    string adminRoleName = "admin";
        //    string userRoleName = "user";

        //    string adminEmail = "admin@mail.ru";
        //    string adminPassword = "123456";

        //    // добавляем роли
        //    Role adminRole = new Role { Id = 1, Name = adminRoleName };
        //    Role userRole = new Role { Id = 2, Name = userRoleName };
        //    Employee adminUser = new Employee { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

        //    modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
        //    modelBuilder.Entity<Employee>().HasData(new Employee[] { adminUser });
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
