using SMTP.WebUI.Models;
using SMTP.WebUI.Models.Entities_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMTP.WebUI.Repositories
{
    public class MoneyRepository
    {
        ApplicationDbContext db;

        public MoneyRepository()
        {
            db = new ApplicationDbContext();
        }

        public void Create(Money money)
        {
            db.Moneys.Add(money);
            db.SaveChanges();
        }
    }
}