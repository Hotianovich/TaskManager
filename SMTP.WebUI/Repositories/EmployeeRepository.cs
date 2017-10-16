using SMTP.WebUI.Interfaces;
using SMTP.WebUI.Models;
using SMTP.WebUI.Models.Entities_Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMTP.WebUI.Repositories
{
    public class EmployeeRepository : IEmployee<Employee>
    {
        ApplicationDbContext db;
        public EmployeeRepository()
        {
            db = new ApplicationDbContext();
        }

        public bool Create(Employee t1)
        {
            var empl = db.Employees.Where(w => w.Email.Equals(t1.Email));
            var employee = empl.FirstOrDefault();
            if (employee == null)
            {
                db.Employees.Add(t1);
                db.SaveChanges();
                return true;
            }
            else
            {
                t1.EmployeeId = employee.EmployeeId;
                return false;
            }
        }

        public bool CreateAfterRegistration(string email, string nickName)
        {
            var empl = db.Employees.Where(w => w.Email.Equals(email));
            var employee = empl.FirstOrDefault();
            if (employee == null)
            {
                Employee e = new Employee();
                e.NickName = nickName;
                e.Email = email;
                db.Employees.Add(e);
                db.SaveChanges();
                return true;
            }
            else {
                return false;
            }
            
        }

        public void Delete(int id)
        {
            var item = db.Employees.Find(id);
            if (item != null)
            {
                db.Employees.Remove(item);
            }
            db.SaveChanges();
        }

        public IEnumerable<Employee> Find(Func<Employee, bool> predicate)
        {
            return db.Employees.Where(predicate).ToList();
        }

        public IEnumerable<Employee> Get(string id)
        {
            var em = from e in db.Employees
                     join ea in db.Users_Employees on e.EmployeeId equals ea.EmployeeId
                     join au in db.Users on ea.UserId equals au.Id
                     where au.Id == id
                     select e;
            return em;
        }

        public Employee GetForRegistration(string email)
        {
            var e = db.Employees.Where(w => w.Email.Equals(email));
            var employee = e.FirstOrDefault();
            return employee;
        }

        public IEnumerable<Employee> GetAll()
        {
            return db.Employees;
        }

        public void Update(Employee t1)
        {
            db.Entry<Employee>(t1).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public double Zp(Employee e, DateTime start, DateTime finish)
        {
            var sum = from m in db.Moneys
                      where m.EmployeeId == e.EmployeeId && m.DayMoney > start && m.DayMoney < finish 
                      select m.MyMoney;
            var sumNull = sum.FirstOrDefault();
            if (sumNull == 0)
                return 0;

            var s = (double)sum.Sum();
            return s;
        }

        public bool FindEmployee(string email, string id)
        {
            var em = from e in db.Employees
                     join ea in db.Users_Employees on e.EmployeeId equals ea.EmployeeId
                     join au in db.Users on ea.UserId equals au.Id
                     where au.Id == id && e.Email == email
                     select e;
            var employee = em.FirstOrDefault();
            if (employee == null)
                return true;
            else
                return false;
        }

    }
}