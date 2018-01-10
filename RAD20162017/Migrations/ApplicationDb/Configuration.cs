namespace RAD20162017.Migrations.ApplicationDb
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using Models.AttendDb;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<RAD20162017.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ApplicationDb";
        }

        protected override void Seed(RAD20162017.Models.ApplicationDbContext context)
        {

            SeedUsers(context);
            SeedRoles(context);
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            var manager =
                 new UserManager<ApplicationUser>(
                     new UserStore<ApplicationUser>(context));

            var roleManager =
                new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(context));

            roleManager.Create(new IdentityRole { Name = "Admin" });
            roleManager.Create(new IdentityRole { Name = "Lecturer" });
            roleManager.Create(new IdentityRole { Name = "StudentRole" });

            ApplicationUser lecturer = manager.FindByEmail("L0001@mail.itsligo.ie");
            if (lecturer != null)
            {
                manager.AddToRoles(lecturer.Id, new string[] { "Lecturer" });
            }
            else
            {
                throw new Exception { Source = "Did not find the lecturer" };
            }

            ApplicationUser StudentRole = manager.FindByEmail("S0001@mail.itsligo.ie");
            if (manager.FindByEmail("S0001@mail.itsligo.ie") != null)
            {
                manager.AddToRoles(StudentRole.Id, new string[] { "StudentRole" });
            }
            else
            {
                throw new Exception { Source = "Did not find the students" };
            }

            ApplicationUser Admin = manager.FindByEmail("admin@itsligo.ie");
            if (manager.FindByEmail("admin@itsligo.ie") != null)
            {
                manager.AddToRoles(Admin.Id, new string[] { "Admin" });
            }
            else
            {
                throw new Exception { Source = "Did not find the admin" };
            }

            context.SaveChanges();
        }

        private void SeedUsers(ApplicationDbContext context)
        {
            context.Users.AddOrUpdate(u => u.Email, new ApplicationUser
                {
                    Email = "S0001@mail.itsligo.ie",
                    EmailConfirmed = true,
                    UserName = "S0001@mail.itsligo.ie",
                    PasswordHash = new PasswordHasher().HashPassword("Password1!"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                });

            context.Users.AddOrUpdate(u => u.Email, new ApplicationUser
                {
                    Email = "L0001@mail.itsligo.ie",
                    EmailConfirmed = true,
                    UserName = "L0001@mail.itsligo.ie",
                    PasswordHash = new PasswordHasher().HashPassword("Password1!"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                });

            context.Users.AddOrUpdate(u => u.Email, new ApplicationUser
            {
                Email = "admin@itsligo.ie",
                EmailConfirmed = true,
                UserName = "admin@itsligo.ie",
                PasswordHash = new PasswordHasher().HashPassword("Password1!"),
                SecurityStamp = Guid.NewGuid().ToString(),
            });

            context.SaveChanges();
        }



    }
}
