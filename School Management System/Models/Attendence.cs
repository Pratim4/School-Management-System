using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace School_Management_System.Models
{
    public class Attendence
    {
        private List<Student> studentList;
        private List<Department> departmentList;

        public int AttendenceId { get; set; }
        public DateTime? Date { get; set; }
        public bool IsPresent { get; set; }

        public int? StudentId { get; set; }
        public virtual Student Student { get; set; }

        [NotMapped()]
        public List<Student> StudentList { get => studentList; set => studentList = value; }

        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        
        [NotMapped()]
        public List<Department> DepartmentList { get => departmentList; set => departmentList = value; }
        
        [NotMapped()]
        public Student Studentmodel { get; set; }

    }
}