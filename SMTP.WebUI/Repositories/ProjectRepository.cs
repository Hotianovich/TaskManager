using SMTP.WebUI.Interfaces;
using SMTP.WebUI.Models;
using SMTP.WebUI.Models.Entities_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMTP.WebUI.Repositories
{
    public class ProjectRepository : IProject<Project, ApplicationUser>
    {
        ApplicationDbContext db;
        public ProjectRepository()
        {
            db = new ApplicationDbContext();
        }

        public void Create(Project t, string id)
        {
            Project nProject = new Project();
            nProject.ProjectName = t.ProjectName;
            nProject.DateCreate = DateTime.Now;
            nProject.UserId = id;
            db.Projects.Add(nProject);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = db.Projects.Find(id);
            if (item != null)
            {
                db.Projects.Remove(item);
            }
            db.SaveChanges();
        }

        public IEnumerable<Project> Find(Func<Project, bool> predicate)
        {
            return db.Projects.Where(predicate).ToList();
        }

        public IEnumerable<Project> Get(string id)
        {
            var proj = from p in db.Projects
                     join au in db.Users on p.UserId equals au.Id
                     where au.Id == id
                     select p;
            return proj;
        }

        public Project GetById(int id)
        {
            var item = db.Projects.Find(id);
            return item;
        }

        public IEnumerable<Project> GetAll()
        {
            return db.Projects;
        }

        public IEnumerable<Project> GetAllForMe(ApplicationUser us)
        {
            return db.Projects.Where(p => p.UserId == us.Id);
        }

        public void Update(Project t)
        {
            db.Entry<Project>(t).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

    }
}