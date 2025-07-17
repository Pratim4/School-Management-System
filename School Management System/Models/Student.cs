using School_Management_System.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace School_Management_System.Models
{
    public class Student
    {

        public int StudentId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string RollNo { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }

        public int? DepartmentId { get; set; }

        public bool IsPresent { get; set; }
        public virtual Department Department { get; set; }

        public int? AttendenceId { get; set; }
     




    }
}