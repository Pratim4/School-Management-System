using School_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace School_Management_System.Controllers
{
   
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var notices = db.Notices.ToList();
            return View(notices);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ContactForm()
        {
            
                if (ModelState.IsValid)
                {
                    // TODO: Handle the form submission, e.g., save to database, send email, etc.
                    ViewBag.Message = "Your message has been sent successfully!";
                    return View("Success");
                }

                    

                return View();
        }
    }
}