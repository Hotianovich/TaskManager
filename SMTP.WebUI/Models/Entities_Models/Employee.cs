using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SMTP.WebUI.Models.Entities_Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [MaxLength(50)]
        [Index]
        [Display(Name ="Ник")]
        public string NickName { get; set; }

        [Required]
        [EmailAddress]
        [Remote("CheckEmail", "Employee")]
        public string Email { get; set; }

        public ICollection<User_Employee> Users_Employees { get; set; }
        public ICollection<Money> Moneys { get; set; }

        public Employee()
        {
            Users_Employees = new List<User_Employee>();
            Moneys = new List<Money>();
        }
    }
}