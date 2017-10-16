using SMTP.WebUI.Interfaces;
using SMTP.WebUI.Models;
using SMTP.WebUI.Models.Entities_Models;
using System.Linq;
using System.Web.Mvc;

namespace SMTP.WebUI.Controllers
{
    public class ProjectController : Controller
    {
        private IAppUser<ApplicationUser> _repositoryAppUser;
        private IProject<Project, ApplicationUser> _repositoryProject;
        public ProjectController(IAppUser<ApplicationUser> repo, IProject<Project, ApplicationUser> pro)
        {
            _repositoryAppUser = repo;
            _repositoryProject = pro;
        }

        public ActionResult ViewProject()
        {
            var us = _repositoryAppUser.Find(u => u.Email.Equals(User.Identity.Name));
            var user = us.FirstOrDefault();
            return PartialView(user);
        }

        public ActionResult GetProject(string id)
        {
            var projects = _repositoryProject.Get(id);
            return PartialView(projects);
        }

        public ActionResult AddProject()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddProject(Project pro, string id)
        {
            try
            {
                var us = _repositoryAppUser.Get(id);
                _repositoryProject.Create(pro, id);
                return RedirectToAction("GetProject", new { id = id });
            }
            catch
            {
                return RedirectToAction("Index", "SanctumSanctorum");
            }
        }

        public ActionResult Delete(int Projectid)
        {
            _repositoryProject.Delete(Projectid);
            return RedirectToAction("Index", "SanctumSanctorum");
        }

        public ActionResult GetProjectForList(int id)
        {
            var project = _repositoryProject.GetById(id);
            return PartialView(project);
        }

        public PartialViewResult Side(string projectName, ProjectTask model, int? idPr)
        {
            ViewBag.proName = projectName;
            var us = _repositoryAppUser.Find(u => u.Email.Equals(User.Identity.Name));
            var user = us.FirstOrDefault();
            var groups = _repositoryProject.GetAllForMe(user);
            ViewBag.All = groups;
            ViewBag.Id = idPr;
            return PartialView(model);
        }

        public ActionResult ProjectForList(int id)
        {
            var p = _repositoryProject.Find(f => f.ProjectId.Equals(id));
            var project = p.FirstOrDefault();
            ViewBag.Proj = project.ProjectName;
            return PartialView();
        }

        // GET: Project
        public ActionResult Index(int projectId)
        {
            var e = _repositoryProject.Find(f => f.ProjectId.Equals(projectId));
            var project = e.FirstOrDefault();
            return View(project);
        }
    }
}