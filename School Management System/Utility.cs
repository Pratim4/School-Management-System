using School_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace School_Management_System
{
    public class Utility
    {
        public static List<SelectListItem> GetRoleList()
        {
           ApplicationDbContext db = new ApplicationDbContext();
            List<SelectListItem > list = new List<SelectListItem>();
            var listrole=db.Roles.ToList();
            foreach (var item in listrole)
            {
                list.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }
            return list;
        
        }

        public static List<SelectListItem> GetDepartmentList()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<SelectListItem> list = new List<SelectListItem>();
            var listrole = db.Departments.ToList();
            foreach (var item in listrole)
            {
                list.Add(new SelectListItem()
                {
                    Value = item.DepartmentId.ToString(),
                    Text = item.DepartmentName
                });
            }
            return list;
        }



    }
  




}