using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using AjaxControlToolkit;
using Microsoft.AspNet.Identity;
using School_Management_System.Models;

using static System.ActivationContext;

namespace School_Management_System.Controllers
{
    public class ContactFormsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
     
        

        // GET: ContactForms
        public ActionResult Index()
        {
            var contactForms = db.ContactForms.OrderByDescending(cf => cf.ContactFormId).ToList();
            return View(contactForms);
        }
        public ActionResult success()
        {
            var model = TempData["ContactForm"] as ContactForm;

            return View(model);
        }

        // GET: ContactForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactForm contactForm = db.ContactForms.Find(id);
            if (contactForm == null)
            {
                return HttpNotFound();
            }
            return View(contactForm);
        }
        [HttpPost]
       public ActionResult SaveData(string name,string Email,string subject,string message)
        {
            ContactForm model = new ContactForm
            {
                Name = name,
                Email = Email,
                Subject = subject,
                Message = message,
                IsNew = true
            };
            db.ContactForms.Add(model);
            db.SaveChanges();
            
            return RedirectToAction("Index", "Home");
        }

        // GET: ContactForms/Create
        public ActionResult Create()
        {
            db.Student.ToList();
            return View();
        }

        // POST: ContactForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                contactForm.IsNew = true;
                db.ContactForms.Add(contactForm);

                db.SaveChanges();
                TempData["ContactForm"] = contactForm;
                return RedirectToAction("success");
            }

            return View(contactForm);
        }

        // GET: ContactForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactForm contactForm = db.ContactForms.Find(id);
            if (contactForm == null)
            {
                return HttpNotFound();
            }
            return View(contactForm);
        }
        public ActionResult MarkAllMessagesAsRead()
        {
            var contactForms = db.ContactForms.Where(cf => cf.IsNew).ToList();

            foreach (var form in contactForms)
            {
                form.IsNew = false;
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }


        // POST: ContactForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactFormId,Name,Email,Subject,Message")] ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactForm);
        }
        public ActionResult GetNewMessageCount()
        {
            var newMessageCount = db.ContactForms.Count(cf => cf.IsNew);
            return Json(new { count = newMessageCount }, JsonRequestBehavior.AllowGet);
        }
        





        // GET: ContactForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactForm contactForm = db.ContactForms.Find(id);
            if (contactForm == null)
            {
                return HttpNotFound();
            }
            return View(contactForm);
        }

        // POST: ContactForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactForm contactForm = db.ContactForms.Find(id);
            db.ContactForms.Remove(contactForm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult SendReply(string Email, string replySubject, string replyMessage)
        {
           
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("timalsinapradeep5696@gmail.com", "myjz yqrf pdvk enqh"),
                    EnableSsl = true,
                };

                using (var mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress("timalsinapradeep5696@gmail.com");
                    mailMessage.To.Add(Email);
                    mailMessage.Subject = replySubject;
                    mailMessage.Body = replyMessage;
                    mailMessage.IsBodyHtml = false;

                    smtpClient.Send(mailMessage);
                }

                ViewBag.Message = "Reply sent successfully!";
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error sending reply: {ex.Message}";
            }

            
            return RedirectToAction("Index", "Home"); 
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
