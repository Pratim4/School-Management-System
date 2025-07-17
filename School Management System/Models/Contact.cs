using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace School_Management_System.Models
{
    public class ContactForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactFormId { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
        public bool IsNew { get; set; }
        
        
    }
}