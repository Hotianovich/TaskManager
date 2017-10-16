using SMTP.WebUI.Interfaces;
using SMTP.WebUI.Models;
using SMTP.WebUI.Models.Entities_Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMTP.WebUI.Repositories
{
    public class TasksRepository : IProjectTask<ProjectTask, ApplicationUser>
    {
        ApplicationDbContext db;
        public TasksRepository()
        {
            db = new ApplicationDbContext();
        }

        public IEnumerable<ProjectTask> GetTasks(int id)
        {
            var t = db.ProjectTasks.Where(w => w.ExecuterId.Equals(id));
            return t;
        }

        public IEnumerable<ProjectTask> GetProjectTasks(int id)
        {
            var t = db.ProjectTasks.Where(w => w.ProjectId.Equals(id));
            return t;
        }

        public IEnumerable<ProjectTask> GetEmployeeTasks(int idEm, List<int> idPr)
        {
            List<ProjectTask> listPr = new List<ProjectTask>();
            foreach (var item in idPr)
            {
                var task = from t in db.ProjectTasks
                           where t.ExecuterId == idEm && t.ProjectId == item
                           select t;

                if (task.Any())
                {
                    listPr.AddRange(task);
                }
            }
            return listPr;
        }

        public void Update(ProjectTask t)
        {
            db.Entry<ProjectTask>(t).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public ProjectTask Get(int id)
        {
            var item = db.ProjectTasks.Find(id);
            return item;
        }

        public void Create(ProjectTask t)
        {
            db.ProjectTasks.Add(t);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = db.ProjectTasks.Find(id);
            if (item != null)
            {
                db.ProjectTasks.Remove(item);
            }
            db.SaveChanges();
        }

        public void DeleteTasks(int emplId, IEnumerable<Project> projects)
        {
            foreach (var p in projects)
            {
                var item = db.ProjectTasks.Where(pt => pt.ExecuterId == emplId && pt.ProjectId == p.ProjectId);
                if (item != null)
                    db.ProjectTasks.RemoveRange(item);
                db.SaveChanges();
            }
            
        }

        public void DeleteTasksNoAuto(int emplId)
        {
            var item = db.ProjectTasks.Where(pt => pt.ExecuterId == emplId);
            if (item != null)
                db.ProjectTasks.RemoveRange(item);
            db.SaveChanges();
        }
    }
}