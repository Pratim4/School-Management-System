using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace School_Management_System.Models
{
    public class Department
    {
        [Required]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}