using SMTP.WebUI.Interfaces;
using SMTP.WebUI.Models;
using SMTP.WebUI.Models.Entities_Models;
using System.Linq;

namespace SMTP.WebUI.Repositories
{
    public class User_EmployeeRepository : IUser_Employee<User_Employee, ApplicationUser, Employee>
    {
        ApplicationDbContext db;
        public User_EmployeeRepository()
        {
            db = new ApplicationDbContext();
        }
        public void Create(ApplicationUser t2, Employee t3)
        {
            User_Employee user = new User_Employee();
            user.EmployeeId = t3.EmployeeId;
            user.UserId = t2.Id;
            db.Users_Employees.Add(user);
            db.SaveChanges();
        }

        public void Delete(ApplicationUser ap, Employee employee)
        {
            var item = from ue in db.Users_Employees
                       where ue.UserId == ap.Id && ue.EmployeeId == employee.EmployeeId
                       select ue;
            var userEmployee = item.FirstOrDefault();
            if (userEmployee != null)
            {
                db.Users_Employees.Remove(userEmployee);
            }

            db.SaveChanges();
        }

      
      
    }
}