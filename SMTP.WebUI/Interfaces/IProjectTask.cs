using SMTP.WebUI.Models.Entities_Models;
using System.Collections.Generic;

namespace SMTP.WebUI.Interfaces
{
    public interface IProjectTask<T1, T2>
    {
        void Create(T1 t1);
        IEnumerable<T1> GetTasks(int id);
        IEnumerable<T1> GetProjectTasks(int id);
        IEnumerable<T1> GetEmployeeTasks(int idEm, List<int> idPr);
        void Update(T1 t1);
        T1 Get(int id);
        void Delete(int id);
        void DeleteTasks(int emplId, IEnumerable<Project> projects);
        void DeleteTasksNoAuto(int emplId);
    }
}
