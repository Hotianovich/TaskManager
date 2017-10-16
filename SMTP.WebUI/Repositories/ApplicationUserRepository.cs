using SMTP.WebUI.Interfaces;
using SMTP.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMTP.WebUI.Repositories
{
    public class ApplicationUserRepository : IAppUser<ApplicationUser>
    {
        ApplicationDbContext db;
        public ApplicationUserRepository()
        {
            db = new ApplicationDbContext();
        }

        public void Create(ApplicationUser t)
        {
            db.Users.Add(t);
            db.SaveChanges();
        }

        public void Delete(string id)
        {
            var item = db.Users.Find(id);
            if (item != null)
            {
                db.Users.Remove(item);
            }
            db.SaveChanges();
        }

        public IEnumerable<ApplicationUser> Find(Func<ApplicationUser, bool> predicate)
        {
            return db.Users.Where(predicate).ToList();

        }

        public ApplicationUser Get(string id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return db.Users;
        }

        public void Update(ApplicationUser t)
        {
            db.Entry<ApplicationUser>(t).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}