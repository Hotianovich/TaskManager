using SMTP.WebUI.Models;
using System.Web.Mvc;
using SMTP.WebUI.Interfaces;
using System.Linq;
using System;
using SMTP.WebUI.Repositories;
using SMTP.WebUI.Models.Entities_Models;

namespace SMTP.WebUI.Controllers
{
    [Authorize]
    public class SanctumSanctorumController : Controller
    {
        private IAppUser<ApplicationUser> _repositoryUser;
        private IEmployee<Employee> _repositoryEmployee;
        public SanctumSanctorumController(IAppUser<ApplicationUser> repoU, IEmployee<Employee> re)
        {
            _repositoryUser = repoU;
            _repositoryEmployee = re;
        }
        // GET: SanctumSanctorum
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Money()
        { 
            return View();
        }
     
        public ActionResult GetMoney()
        {
            
            return PartialView();
        }

        [HttpPost]
        public ActionResult RezultMoney(DateTime to, DateTime from)
        {
            var users = _repositoryUser.Find(us => us.UserName.Equals(User.Identity.Name));
            var user = users.FirstOrDefault();
            var er = _repositoryEmployee.GetForRegistration(user.Email);
            EmployeeRepository employeeRep = new EmployeeRepository();
            double zp = employeeRep.Zp(er, to, from);
            ViewBag.Zp = zp;
            return PartialView();
        }
    }
}