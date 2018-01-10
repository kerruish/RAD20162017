using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RAD20162017.Models.AttendDb
{
    public class AttendDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }

        public AttendDbContext()
            : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public static AttendDbContext Create()
        {
            return new AttendDbContext();
        }

    }
}