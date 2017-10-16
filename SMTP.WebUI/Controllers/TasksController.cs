using Microsoft.AspNet.Identity;
using SMTP.WebUI.Interfaces;
using SMTP.WebUI.Models;
using SMTP.WebUI.Models.Entities_Models;
using SMTP.WebUI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SMTP.WebUI.Controllers
{
    public class TasksController : Controller
    {
        private IAppUser<ApplicationUser> _repositoryAppUser;
        private IProjectTask<ProjectTask, ApplicationUser> _repositoryTask;
        private IProject<Project, ApplicationUser> _repositoryProject;
        private IEmployee<Employee> _repositoryEmployee;
        public TasksController(IAppUser<ApplicationUser> rep, IProjectTask<ProjectTask, ApplicationUser> tas, IProject<Project, ApplicationUser> pro, IEmployee<Employee> repoEmployee)
        {
            _repositoryAppUser = rep;
            _repositoryTask = tas;
            _repositoryProject = pro;
            _repositoryEmployee = repoEmployee;
        }
        
        public ActionResult ViewTasks()
        {
            var user = _repositoryAppUser.Find(u => u.UserName.Equals(User.Identity.Name));
            var us = user.FirstOrDefault();

            var employee = _repositoryEmployee.GetForRegistration(us.Email);
            var tasks = _repositoryTask.GetTasks(employee.EmployeeId);

            ViewBag.Tasks = tasks.Any() ? tasks : null;
            ViewBag.Id = us.Id;

            return PartialView(tasks);
        }

        public ActionResult GetTasksToList(IEnumerable<ProjectTask> model)
        {
            return PartialView(model);
        }

        public ActionResult Success(int id)
        {
            var t = _repositoryTask.Get(id);
            t.StateTask = 2;
            _repositoryTask.Update(t);
            return RedirectToAction("ViewTasks");
        }

        public ActionResult ViewEmployeeTasks(Employee employee)
        {
            var user = _repositoryAppUser.Find(u => u.UserName.Equals(User.Identity.Name));
            var us = user.FirstOrDefault();

            var proj = _repositoryProject.Get(us.Id);
            List<int> projId = new List<int>();
            foreach (var item in proj)
            {
                projId.Add(item.ProjectId);
            }

            var tasks = _repositoryTask.GetEmployeeTasks(employee.EmployeeId, projId).Reverse();
            ViewBag.Id = employee.EmployeeId;
            return PartialView(tasks);
        }

        public ActionResult ViewProjectTasks(Project project)
        {
            var tasks = _repositoryTask.GetProjectTasks(project.ProjectId).Reverse();
            ViewBag.PrId = project.ProjectId;
            return PartialView(tasks);
        }

        public ActionResult GetEmployeeTasksToList(IEnumerable<ProjectTask> model, int id)
        {
            ViewBag.Id = id;
            return PartialView(model);
        }

        public ActionResult GetProjectTasksToList(IEnumerable<ProjectTask> model, int projId)
        {
            ViewBag.Id = projId;
            return PartialView(model);
        }

        public ActionResult NewTaskRow(int id)
        {
            ViewBag.Id = id;
            var model = new ProjectTask();
            return PartialView(model);
        }

        public ActionResult NewProjectTaskRow(int id)
        {
            ViewBag.Id = id;
            var model = new ProjectTask();
            return PartialView(model);
        }


        [HttpPost]
        public async Task<ActionResult> AddTask(ProjectTask task)
        {
            if (ModelState.IsValid)
            {
                _repositoryTask.Create(task);
                var e = _repositoryEmployee.Find(f => f.EmployeeId.Equals(task.ExecuterId));
                var employee = e.FirstOrDefault();

                //IdentityMessage message = new IdentityMessage();
                //message.Body = string.Format(employee.NickName + " на Вас возложена новая задача!!!");
                //message.Destination = employee.Email;
                //message.Subject = "Dream team";
                //EmailService em = new EmailService();
                //await em.SendAsync(message);

                return RedirectToAction("ViewEmployeeTasks", employee);
            }
            else
            {
                var e = _repositoryEmployee.Find(f => f.EmployeeId.Equals(task.ExecuterId));
                var employee = e.FirstOrDefault();
                return RedirectToAction("ViewEmployeeTasks", employee);   
            }

        }

        [HttpPost]
        public async Task<ActionResult> AddProjectTask(ProjectTask task)
        {
            if (ModelState.IsValid)
            {
                _repositoryTask.Create(task);
                var p = _repositoryProject.Find(f => f.ProjectId.Equals(task.ProjectId));
                var project = p.FirstOrDefault();

                var e = _repositoryEmployee.Find(f => f.EmployeeId.Equals(task.ExecuterId));
                var employee = e.FirstOrDefault();

                //IdentityMessage message = new IdentityMessage();
                //message.Body = string.Format(employee.NickName + " на Вас возложена новая задача!!!");
                //message.Destination = employee.Email;
                //message.Subject = "Dream team";
                //EmailService em = new EmailService();
                //await em.SendAsync(message);

                return RedirectToAction("ViewProjectTasks", project);
            }
            else
            {
                var p = _repositoryProject.Find(f => f.ProjectId.Equals(task.ProjectId));
                var project = p.FirstOrDefault();
                return RedirectToAction("ViewProjectTasks", project);
            }

        }


        public JsonResult ValidateDate(string EndDate)
        {
            DateTime parsedDate;

            if (!DateTime.TryParse(EndDate, out parsedDate))
            {
                return Json("Формат (гггг/мм/дд)",
                    JsonRequestBehavior.AllowGet);
            }
            else if (DateTime.Now > parsedDate)
            {
                return Json("Старая дата",
                    JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Update(ProjectTask model)
        {
            if (ModelState.IsValid)
            {
                _repositoryTask.Update(model);
                var e = _repositoryEmployee.Find(f => f.EmployeeId.Equals(model.ExecuterId));
                var employee = e.FirstOrDefault();
                return RedirectToAction("ViewEmployeeTasks", employee);
            }
            else
            {
                var e = _repositoryEmployee.Find(f => f.EmployeeId.Equals(model.ExecuterId));
                var employee = e.FirstOrDefault();
                return RedirectToAction("ViewEmployeeTasks", employee);
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateProjectTasks(ProjectTask model, int executer)
        {
            if (ModelState.IsValid)
            {
                _repositoryTask.Update(model);
                var p = _repositoryProject.Find(f => f.ProjectId.Equals(model.ProjectId));
                var project = p.FirstOrDefault();

                if (executer != model.ExecuterId)
                {
                    var e = _repositoryEmployee.Find(f => f.EmployeeId.Equals(model.ExecuterId));
                    var employee = e.FirstOrDefault();

                    //IdentityMessage message = new IdentityMessage();
                    //message.Body = string.Format(employee.NickName + " на Вас возложена новая задача!!!");
                    //message.Destination = employee.Email;
                    //message.Subject = "Dream team";
                    //EmailService em = new EmailService();
                    //await em.SendAsync(message);
                }
                return RedirectToAction("ViewProjectTasks", project);
            }
            else
            {
                var p = _repositoryProject.Find(f => f.ProjectId.Equals(model.ProjectId));
                var project = p.FirstOrDefault();
                return RedirectToAction("ViewProjectTasks", project);
            }
        }

        public ActionResult Delete(int taskid, int executerId)
        {
            _repositoryTask.Delete(taskid);
            var e = _repositoryEmployee.Find(f => f.EmployeeId.Equals(executerId));
            var employee = e.FirstOrDefault();
            return RedirectToAction("ViewEmployeeTasks", employee);
        }

        public ActionResult DeleteProjectTask(int taskid, int projectId)
        {
            _repositoryTask.Delete(taskid);
            var p = _repositoryProject.Find(f => f.ProjectId.Equals(projectId));
            var project = p.FirstOrDefault();
            return RedirectToAction("ViewProjectTasks", project);
        }

        public ActionResult ConfirmProjectTask(int taskid, int projectId)
        {
            var t = _repositoryTask.Get(taskid);
            t.StateTask = 3;
            _repositoryTask.Update(t);
            var p = _repositoryProject.Find(f => f.ProjectId.Equals(projectId));
            var project = p.FirstOrDefault();

            if (t.PriceTask != null)
            {
                Money m = new Money();
                m.DayMoney = DateTime.Now;
                m.MyMoney = (decimal)t.PriceTask;
                m.EmployeeId = t.ExecuterId;
                MoneyRepository mr = new MoneyRepository();
                mr.Create(m);
            }
            return RedirectToAction("ViewProjectTasks", project);
        }


        public ActionResult ConfirmEmployeeTask(int taskid, int executerId)
        {
            var t = _repositoryTask.Get(taskid);
            t.StateTask = 3;
            _repositoryTask.Update(t);

            var e = _repositoryEmployee.Find(f => f.EmployeeId.Equals(executerId));
            var employee = e.FirstOrDefault();

            if (t.PriceTask != null)
            {
                Money m = new Money();
                m.DayMoney = DateTime.Now;
                m.MyMoney = (decimal)t.PriceTask;
                m.EmployeeId = t.ExecuterId;
                MoneyRepository mr = new MoneyRepository();
                mr.Create(m);
            }
            return RedirectToAction("ViewEmployeeTasks", employee);
        }
        // GET: Tasks
        public ActionResult Index()
        {
            return View();
        }
    }
}