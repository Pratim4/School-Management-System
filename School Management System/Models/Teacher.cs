using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace School_Management_System.Models
{
    public class Teacher
    {

        public int TeacherId { get; set; }
        [Required]
        public string TeacherName { get; set; }
        [Required]
        public string TeacherDescription { get; set; }
        [Required]
        public string Email {  get; set; }
        
        public string Phone { get; set; }   

    }
}