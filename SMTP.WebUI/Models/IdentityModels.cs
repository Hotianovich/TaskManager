using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SMTP.WebUI.Models.Entities_Models;
using System.Collections;
using System.Collections.Generic;

namespace SMTP.WebUI.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Project> Projects { get; set; }
        public ICollection<User_Employee> Users_Employees { get; set; }
        public string NickName { get; set; }
        public ApplicationUser()
        {
            Projects = new List<Project>();
            Users_Employees = new List<User_Employee>();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("nick", this.NickName));
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User_Employee> Users_Employees { get; set; }
        public DbSet<Money> Moneys { get; set; }

        public ApplicationDbContext()
            : base("ProjectDB", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}