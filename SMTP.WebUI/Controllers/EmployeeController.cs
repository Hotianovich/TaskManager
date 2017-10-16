using SMTP.WebUI.Interfaces;
using SMTP.WebUI.Models;
using SMTP.WebUI.Models.Entities_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace SMTP.WebUI.Controllers
{
    public class EmployeeController : Controller
    {
        private IAppUser<ApplicationUser> _repositoryUser;
        private IEmployee<Employee> _repositoryEmployee;
        private IUser_Employee<User_Employee, ApplicationUser, Employee> _repositoryUser_Employee;
        private IProjectTask<ProjectTask, ApplicationUser> _repositoryProject_Task;
        private IProject<Project, ApplicationUser> _repositoryProject;
        public EmployeeController(IAppUser<ApplicationUser> rep, IEmployee<Employee> emp,
            IUser_Employee<User_Employee, ApplicationUser, Employee> us, IProjectTask<ProjectTask, ApplicationUser> repositoryP_T, IProject<Project, ApplicationUser> repoProject)
        {
            _repositoryUser = rep;
            _repositoryEmployee = emp;
            _repositoryUser_Employee = us;
            _repositoryProject_Task = repositoryP_T;
            _repositoryProject = repoProject;
        }

        public ActionResult ViewEmployee()
        {
            var us = _repositoryUser.Find(u => u.Email.Equals(User.Identity.Name));
            var user = us.FirstOrDefault();
            return PartialView(user);
        }

        public ActionResult GetEmployee(string id)
        {
            var employee = _repositoryEmployee.Get(id);
            return PartialView(employee);
        }

        public ActionResult EmployeeForList(int id)
        {
            var e = _repositoryEmployee.Find(f => f.EmployeeId.Equals(id));
            var employee = e.FirstOrDefault();
            ViewBag.Empl = employee.NickName.Substring(0, 2);
            return PartialView(employee);
        }

        public ActionResult AddEmployee()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<ActionResult> AddEmployee(Employee empl, string id)
        {
            try
            {
                var us = _repositoryUser.Get(id);
                _repositoryEmployee.Create(empl);

                IdentityMessage message = new IdentityMessage();
                message.Body = string.Format(us.NickName + " добавил Вас в свою команду!");
                message.Destination = empl.Email;
                message.Subject = "Dream team";
                EmailService em = new EmailService();
                await em.SendAsync(message);

                _repositoryUser_Employee.Create(us, empl);
                return RedirectToAction("GetEmployee", new { id = id});
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(Employee model)
        {
            var us = _repositoryUser.Find(u => u.Email.Equals(model.Email));
            var user = us.FirstOrDefault();
            if (user == null)
            {
                _repositoryEmployee.Delete(model.EmployeeId);
                _repositoryProject_Task.DeleteTasksNoAuto(model.EmployeeId);
            }
            else
            {
                var usBos = _repositoryUser.Find(u => u.Email.Equals(User.Identity.Name));
                var userBos = usBos.FirstOrDefault();

                var projects = _repositoryProject.Get(userBos.Id);

                _repositoryUser_Employee.Delete(userBos, model);
                _repositoryProject_Task.DeleteTasks(model.EmployeeId, projects);
            }
            return RedirectToAction("Index", "SanctumSanctorum");
        }

        public PartialViewResult DropDownEmployee(ProjectTask model, int? idEm)
        {
            if (idEm != null)
            {
                var e = _repositoryEmployee.Find(f => f.EmployeeId.Equals(idEm));
                var employee = e.FirstOrDefault();
                ViewBag.emplName = employee.NickName;
            }

            var us = _repositoryUser.Find(u => u.Email.Equals(User.Identity.Name));
            var user = us.FirstOrDefault();
            var groups = _repositoryEmployee.Get(user.Id);

            ViewBag.Boss = groups.FirstOrDefault().EmployeeId;
            ViewBag.All = groups;
            ViewBag.Id = idEm;
            return PartialView(model);
        }


        public PartialViewResult DropDownEmployeeForList(ProjectTask model, int? idEm, int index)
        {
            if (idEm != null)
            {
                var e = _repositoryEmployee.Find(f => f.EmployeeId.Equals(idEm));
                var employee = e.FirstOrDefault();
                ViewBag.emplName = employee.NickName;
            }
            var us = _repositoryUser.Find(u => u.Email.Equals(User.Identity.Name));
            var user = us.FirstOrDefault();
            var groups = _repositoryEmployee.Get(user.Id);
            ViewBag.All = groups;
            ViewBag.Id = idEm;
            ViewBag.Index = index;
            return PartialView(model);
        }

        // GET: Employee
        public ActionResult Index(int Employeeid)
        {
            var e = _repositoryEmployee.Find(f => f.EmployeeId.Equals(Employeeid));
            var employee = e.FirstOrDefault();
            return View(employee);
        }

    
        public JsonResult CheckEmail(string Email)
        {

            var us = _repositoryUser.Find(u => u.Email.Equals(User.Identity.Name));
            var user = us.FirstOrDefault();
            bool result = _repositoryEmployee.FindEmployee(Email, user.Id);

            if (result)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Введите другой email", JsonRequestBehavior.AllowGet);
            }
        }



    }
}