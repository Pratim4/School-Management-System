using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using School_Management_System.Migrations;

namespace School_Management_System.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Student> Student { get; set; }
        public DbSet<Teacher> Teacher { get; set; }

        public System.Data.Entity.DbSet<School_Management_System.Models.Attendence> Attendences { get; set; }

        public System.Data.Entity.DbSet<School_Management_System.Models.Department> Departments { get; set; }

        public System.Data.Entity.DbSet<School_Management_System.Models.ContactForm> ContactForms { get; set; }

        public System.Data.Entity.DbSet<School_Management_System.Models.Notice> Notices { get; set; }

        public System.Data.Entity.DbSet<School_Management_System.Models.SMS> SMS { get; set; }
    }
}