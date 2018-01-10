namespace RAD20162017.Migrations.AttendDb
{
    using Models.AttendDb;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RAD20162017.Models.AttendDb.AttendDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\AttendDb";
        }

        protected override void Seed(RAD20162017.Models.AttendDb.AttendDbContext context)
        {
            //SeedSubjects(context);
            //SeedAttendance(context);
        }

        private void SeedAttendance(AttendDbContext context)
        {
            List<Student> selectedStudents = new List<Student>();
            selectedStudents = GetStudents(context);

            List<Subject> selectedSubjects = new List<Subject>();
            selectedSubjects = GetSubjects(context);

            foreach (var s in selectedStudents)
            {
                context.Attendances.AddOrUpdate(a => a.StudentID,
                    new Attendance
                    {
                        AttendanceDate = DateTime.Now,
                        Present = true,
                        StudentID = s.StudentID,
                        student = s,
                        subject = selectedSubjects.FirstOrDefault(),
                        SubjectID = selectedSubjects.FirstOrDefault().SubjectID

                    }
                    );
                context.SaveChanges();
            }
        }

        private void SeedSubjects(AttendDbContext context)
        {
            context.Subjects.AddOrUpdate(s => s.SubjectID,
              new Subject
              {
                  SubjectName = "Calculus",
                  students = new List<Student> {
                                new Student { FirstName = "Mary", SecondName = "Jane" },
                                new Student { FirstName = "Boby", SecondName = "Bobberson" },
                           },
                  lecturers = new List<Lecturer>
                           {
                                new Lecturer { LecturerName = "Professor X" }
                           }
              }
           );

            context.Subjects.AddOrUpdate(s => s.SubjectID,
               new Subject
               {
                   SubjectName = "Algebra",
                   students = new List<Student> {
                                        new Student { FirstName = "Catherine", SecondName = "Janeway" },
                                        new Student { FirstName = "Jean-Luc", SecondName = "Picard" },
                                                    },
                   lecturers = new List<Lecturer> {
                                        new Lecturer { LecturerName = "Professor Snake" }
                                }
               }
            );


            context.Subjects.AddOrUpdate(s => s.SubjectID,
               new Subject
               {
                   SubjectName = "Trigonometry",
                   students = new List<Student> {
                                            new Student { FirstName = "Axl", SecondName = "Rose" }
                            }
               }
            );


            context.Subjects.AddOrUpdate(s => s.SubjectID,
               new Subject
               {
                   SubjectName = "Calculus"
               }
            );
        }

        private List<Student> GetStudents(AttendDbContext context)
        {
            var randomSetStudent = context.Students.Select(s => new { s.StudentID, r = Guid.NewGuid() });
            List<string> subset = randomSetStudent.OrderBy(s => s.r)
                .Select(s => s.StudentID.ToString()).Take(4).ToList();
            return context.Students.Where(s => subset.Contains(s.StudentID.ToString())).ToList();
        }

        private List<Subject> GetSubjects(AttendDbContext context)
        {
            var randomSetSubject = context.Subjects.Select(s => new { s.SubjectID, r = Guid.NewGuid() });
            List<string> subset = randomSetSubject.OrderBy(s => s.r)
                .Select(s => s.SubjectID.ToString()).Take(2).ToList();

            return context.Subjects.Where(s => subset.Contains(s.SubjectID.ToString())).ToList();
        }



    }
}
