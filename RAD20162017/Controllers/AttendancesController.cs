using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RAD20162017.Models.AttendDb;

namespace RAD20162017.Controllers
{
    public class AttendancesController : Controller
    {
        private AttendDbContext db = new AttendDbContext();

        // GET: Attendances
        public ActionResult Index(string sortOrder)
        {
            //var attendances = db.Attendances.Include(a => a.student).Include(a => a.subject);
            //return View(attendances.ToList());

            ViewBag.SubjectSortParam = String.IsNullOrEmpty(sortOrder) ? "subject_desc" : "";
            //ViewBag.DateSortParam = sortOrder == "Date" ? "date_desc" : "Date";

            var attendances = from s in db.Attendances
                              //orderby s.StudentID
                              select s;
            switch (sortOrder)
            {
                case "subject_desc":
                    attendances = attendances.OrderByDescending(s => s.subject);
                    break;
                //case "Date":
                //    attendances = attendances.OrderBy(s => s.AttendanceDate);
                //    break;
                //case "date_desc":
                //    attendances = attendances.OrderByDescending(s => s.AttendanceDate);
                //    break;
                default:
                    attendances = attendances.OrderBy(s => s.subject);
                    break;
            }
            return View(attendances.ToList());
        }

        // GET: Attendances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // GET: Attendances/Create
        public ActionResult Create()
        {
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName");
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName");
            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AttendanceID,AttendanceDate,SubjectID,StudentID,Present")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", attendance.StudentID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName", attendance.SubjectID);
            return View(attendance);
        }

        // GET: Attendances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", attendance.StudentID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName", attendance.SubjectID);
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AttendanceID,AttendanceDate,SubjectID,StudentID,Present")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", attendance.StudentID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName", attendance.SubjectID);
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendance attendance = db.Attendances.Find(id);
            db.Attendances.Remove(attendance);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
