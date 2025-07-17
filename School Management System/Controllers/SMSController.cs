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
    public class SMSController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SMS
        public ActionResult Index()
        {
            return View(db.SMS.ToList());
        }

        // GET: SMS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMS sMS = db.SMS.Find(id);
            if (sMS == null)
            {
                return HttpNotFound();
            }
            return View(sMS);
        }

        // GET: SMS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SMS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SMSID,SMS_Name")] SMS sMS)
        {
            if (ModelState.IsValid)
            {
                db.SMS.Add(sMS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sMS);
        }

        // GET: SMS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMS sMS = db.SMS.Find(id);
            if (sMS == null)
            {
                return HttpNotFound();
            }
            return View(sMS);
        }

        // POST: SMS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SMSID,SMS_Name")] SMS sMS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sMS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sMS);
        }

        // GET: SMS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMS sMS = db.SMS.Find(id);
            if (sMS == null)
            {
                return HttpNotFound();
            }
            return View(sMS);
        }

        // POST: SMS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SMS sMS = db.SMS.Find(id);
            db.SMS.Remove(sMS);
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
