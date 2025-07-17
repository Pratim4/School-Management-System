using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Web;

namespace School_Management_System.Models
{
    public class Notice
    {
        public int NoticeId { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Description { get; set; }

        public string ImagePath { get; set; }
        
        public string ImageFileName { get; set; }

        
    }
}