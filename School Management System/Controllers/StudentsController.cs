using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using School_Management_System.Models;

namespace School_Management_System.Controllers
{
    public class StudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Students
        public ActionResult Index()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            ViewBag.AttendenceId = new SelectList(db.Attendences, "AttendenceId","IsPresent");


            var students = db.Student.Include(a => a.Department);
            //var student = db.Student.Include(a => a.Attendence);
            return View(students.ToList());
            
            
        }

        [HttpPost]

        public ActionResult Index(Student model)
        {


            IEnumerable<Student> filteredStudent = db.Student.ToList();
            
            if (model.DepartmentId != null)
            {
                filteredStudent = filteredStudent.Where(x => x.DepartmentId == model.DepartmentId).ToList();
            }
            
            
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");

            return View(filteredStudent.ToList());
        }

            // GET: Students/Details/5
            public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            if (db.Student.Any(s => s.RollNo == student.RollNo))
            {
                ModelState.AddModelError("RollNo", "Student with this Roll No already exists.");
            }
            if (ModelState.IsValid)
            {
                db.Student.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", student.DepartmentId);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "Name", student.DepartmentId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", student.DepartmentId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Student.Find(id);
            db.Student.Remove(student);
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
