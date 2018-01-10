namespace RAD20162017.Migrations.AttendDb
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendance",
                c => new
                    {
                        AttendanceID = c.Int(nullable: false, identity: true),
                        AttendanceDate = c.DateTime(nullable: false),
                        SubjectID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                        Present = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AttendanceID)
                .ForeignKey("dbo.Student", t => t.StudentID, cascadeDelete: true)
                .ForeignKey("dbo.Subject", t => t.SubjectID, cascadeDelete: true)
                .Index(t => t.SubjectID)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        StudentID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        SecondName = c.String(),
                    })
                .PrimaryKey(t => t.StudentID);
            
            CreateTable(
                "dbo.Subject",
                c => new
                    {
                        SubjectID = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(),
                    })
                .PrimaryKey(t => t.SubjectID);
            
            CreateTable(
                "dbo.Lecturer",
                c => new
                    {
                        LecturerID = c.Int(nullable: false, identity: true),
                        LecturerName = c.String(),
                    })
                .PrimaryKey(t => t.LecturerID);
            
            CreateTable(
                "dbo.LecturerSubjects",
                c => new
                    {
                        Lecturer_LecturerID = c.Int(nullable: false),
                        Subject_SubjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Lecturer_LecturerID, t.Subject_SubjectID })
                .ForeignKey("dbo.Lecturer", t => t.Lecturer_LecturerID, cascadeDelete: true)
                .ForeignKey("dbo.Subject", t => t.Subject_SubjectID, cascadeDelete: true)
                .Index(t => t.Lecturer_LecturerID)
                .Index(t => t.Subject_SubjectID);
            
            CreateTable(
                "dbo.SubjectStudents",
                c => new
                    {
                        Subject_SubjectID = c.Int(nullable: false),
                        Student_StudentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Subject_SubjectID, t.Student_StudentID })
                .ForeignKey("dbo.Subject", t => t.Subject_SubjectID, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.Student_StudentID, cascadeDelete: true)
                .Index(t => t.Subject_SubjectID)
                .Index(t => t.Student_StudentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendance", "SubjectID", "dbo.Subject");
            DropForeignKey("dbo.Attendance", "StudentID", "dbo.Student");
            DropForeignKey("dbo.SubjectStudents", "Student_StudentID", "dbo.Student");
            DropForeignKey("dbo.SubjectStudents", "Subject_SubjectID", "dbo.Subject");
            DropForeignKey("dbo.LecturerSubjects", "Subject_SubjectID", "dbo.Subject");
            DropForeignKey("dbo.LecturerSubjects", "Lecturer_LecturerID", "dbo.Lecturer");
            DropIndex("dbo.SubjectStudents", new[] { "Student_StudentID" });
            DropIndex("dbo.SubjectStudents", new[] { "Subject_SubjectID" });
            DropIndex("dbo.LecturerSubjects", new[] { "Subject_SubjectID" });
            DropIndex("dbo.LecturerSubjects", new[] { "Lecturer_LecturerID" });
            DropIndex("dbo.Attendance", new[] { "StudentID" });
            DropIndex("dbo.Attendance", new[] { "SubjectID" });
            DropTable("dbo.SubjectStudents");
            DropTable("dbo.LecturerSubjects");
            DropTable("dbo.Lecturer");
            DropTable("dbo.Subject");
            DropTable("dbo.Student");
            DropTable("dbo.Attendance");
        }
    }
}
