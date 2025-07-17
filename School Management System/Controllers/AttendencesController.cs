using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using School_Management_System.Migrations;
using School_Management_System.Models;
using PagedList;
using System.Web.UI;

namespace School_Management_System.Controllers
{
    public class AttendencesController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Attendences
        public ActionResult Index(int? DepartmentId, int? StudentId, DateTime? Date, int? page)
        {
            var departments = db.Attendences.Include(s => s.Department);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");

            var attendences = db.Attendences.Include(a => a.Student);

            if (Date.HasValue)
            {
                attendences = attendences.Where(x => x.Date == Date.Value);
            }
            if (DepartmentId.HasValue)
            {
                attendences = attendences.Where(x => x.DepartmentId == DepartmentId.Value);
            }
            if (StudentId.HasValue)
            {
                attendences = attendences.Where(x => x.StudentId == StudentId.Value);
            }

            attendences = attendences.OrderByDescending(a => a.Date); // Order by date descending

            int pageSize = 10; // Number of items per page
            int pageNumber = (page ?? 1);

            var pagedAttendences = attendences.ToPagedList(pageNumber, pageSize);

            ViewBag.StudentId = new SelectList(db.Student, "StudentId", "Name");

            return View(pagedAttendences);

            //var departments = db.Attendences.Include(s => s.Department);

            //ViewBag.DepartmentId = new SelectList(db.Departments,"DepartmentId","DepartmentName");
            //var attendences = db.Attendences.Include(a => a.Student);
            //ViewBag.StudentId = new SelectList(db.Student, "StudentId", "Name");
            //        return View(attendences.ToList());
        }



        [HttpPost]
        
        public ActionResult Index(Attendence model, int? page)
        {


            //IEnumerable<Attendence> filteredAttendances=db.Attendences.ToList();
            // if (model.Date != null)
            //{
            //    filteredAttendances=filteredAttendances.Where(x => x.Date == model.Date).ToList();
            //}

            //if (model.DepartmentId!=null)
            //{
            //   filteredAttendances= filteredAttendances.Where(x => x.DepartmentId == model.DepartmentId).ToList();
            //}
            //if (model.StudentId != null)
            //{
            //   filteredAttendances = filteredAttendances.Where(x => x.StudentId == model.StudentId).ToList();
            //}
            //ViewBag.StudentId = new SelectList(db.Student, "StudentId", "Name");
            //ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");

            //return View(filteredAttendances.ToList());
            IEnumerable<Attendence> filteredAttendances = db.Attendences.Include(a => a.Student).Include(a => a.Department);

            if (model.Date != null)
            {
                filteredAttendances = filteredAttendances.Where(x => x.Date == model.Date);
            }
            if (model.DepartmentId != null)
            {
                filteredAttendances = filteredAttendances.Where(x => x.DepartmentId == model.DepartmentId);
            }
            if (model.StudentId != null)
            {
                filteredAttendances = filteredAttendances.Where(x => x.StudentId == model.StudentId);
            }

            filteredAttendances = filteredAttendances.OrderByDescending(a => a.Date); // Order by date descending

            int pageSize = 10; // Number of items per page
            int pageNumber = (page ?? 1);

            var pagedAttendances = filteredAttendances.ToPagedList(pageNumber, pageSize);

            ViewBag.StudentId = new SelectList(db.Student, "StudentId", "Name");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");

            return View(pagedAttendances);

        }


        // GET: Attendences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = db.Attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            return View(attendence);
        }

        // GET: Attendences/Create
        public ActionResult Create(string department)
        {
            var departments= db.Student.Select(s=> s.Department.DepartmentName).Distinct().ToList();
            
            ViewBag.Departments = new SelectList(departments);
            Attendence attendance = new Attendence
            {
                Studentmodel = new Models.Student(),
                StudentList = new List<Models.Student>()
            };


            attendance.StudentList=db.Student.ToList();
            if(!string.IsNullOrEmpty(department))
            {
                attendance.StudentList=db.Student.Where(x=>x.Department.DepartmentName == department).ToList();
                ViewBag.CurrentFilter=department;
            }
         



            ViewBag.StudentId = new SelectList(db.Student, "StudentId", "Name");
           return View(attendance);
        }

        // POST: Attendences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Attendence model)

        {
            if (!ModelState.IsValid)
            {
                var today = DateTime.Now.Date;


                foreach (var item in model.StudentList)
                {
                    var existingAttendance = db.Attendences
              .FirstOrDefault(a => a.StudentId == item.StudentId && a.Date == today);

                    if (existingAttendance != null)
                    {
                        ModelState.AddModelError("", $"Attendance for student with Roll No {item.RollNo} already exists for today.");
                        ViewBag.StudentId = new SelectList(db.Student, "StudentId", "Name", model.StudentId);
                        return View(model);
                    }

                    var attendence = new Attendence
                    {
                        StudentId = item.StudentId,
                        DepartmentId = item.DepartmentId,
                        Date = DateTime.Now.Date,
                        IsPresent = item.IsPresent
                    };



                    db.Attendences.Add(attendence);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }



            ViewBag.StudentId = new SelectList(db.Student, "StudentId", "Name", model.StudentId);
            return View(model);
        }




        [HttpPost]
        public ActionResult Search(Attendence model)
        {
            

            var students = db.Student.Where(s => s.DepartmentId==model.DepartmentId).ToList();

            //model.StudentList = new List<Student>();
            model.StudentList= students;
            
            ViewBag.Departments = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            return View("Create", model);
        }



        // GET: Attendences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = db.Attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.Student, "StudentId", "Name", attendence.StudentId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", attendence.DepartmentId);
            return View(attendence);
        }

        // POST: Attendences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AttendenceId,Date,IsPresent,StudentId,DepartmentId")] Attendence attendence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.Student, "StudentId", "Name", attendence.StudentId);
            ViewBag.StudentId = new SelectList(db.Student, "DepartmentId", "DepartmentName", attendence.DepartmentId);
            return View(attendence);
        }

        // GET: Attendences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = db.Attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            return View(attendence);
        }

        // POST: Attendences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendence attendence = db.Attendences.Find(id);
            db.Attendences.Remove(attendence);
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
