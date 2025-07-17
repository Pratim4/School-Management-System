using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using School_Management_System.Models;

namespace School_Management_System.Controllers
{
    public class NoticesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Notices
        public ActionResult Index()
        {
            return View(db.Notices.ToList());
        }

        // GET: Notices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notice notice = db.Notices.Find(id);
            if (notice == null)
            {
                return HttpNotFound();
            }
            return View(notice);
        }

        // GET: Notices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NoticeId,Subject,Description,ImagePath,ImageFileName")] Notice notice, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    
                    var fileName = Path.GetFileName(Image.FileName);
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);

                    
                    var path = Path.Combine(Server.MapPath("~/Content/Images"), uniqueFileName);

                    
                    Image.SaveAs(path);

                   
                    notice.ImagePath = "~/Content/Images/" + uniqueFileName;
                    notice.ImageFileName = uniqueFileName;
                }

                db.Notices.Add(notice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(notice);
        }

        // GET: Notices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notice notice = db.Notices.Find(id);
            if (notice == null)
            {
                return HttpNotFound();
            }
            TempData["CurrentImagePath"] = notice.ImagePath;
            return View(notice);
        }

        // POST: Notices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NoticeId,Subject,Description,ImagePath,ImageFileName")] Notice notice, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
            
                    if (Image != null && Image.ContentLength > 0)
                    {
                    
                        var fileName = Path.GetFileName(Image.FileName);
                        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);

                       
                        var path = Path.Combine(Server.MapPath("~/Content/Images"), uniqueFileName);

                      
                        Image.SaveAs(path);

                        
                        notice.ImagePath = "~/Content/Images/" + uniqueFileName;
                        notice.ImageFileName = uniqueFileName;
                    }
                    else
                    {
                        
                        notice.ImagePath = db.Notices.AsNoTracking().Where(n => n.NoticeId == notice.NoticeId).Select(n => n.ImagePath).FirstOrDefault();
                    }
                    db.Entry(notice).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }
            return View(notice);
        } // GET: Notices/Edit/5
     

        // POST: Notices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        // GET: Notices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notice notice = db.Notices.Find(id);
            if (notice == null)
            {
                return HttpNotFound();
            }
            return View(notice);
        }

        // POST: Notices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notice notice = db.Notices.Find(id);
            db.Notices.Remove(notice);
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
