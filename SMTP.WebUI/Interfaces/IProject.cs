using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTP.WebUI.Interfaces
{
    public interface IProject<T, T1>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllForMe(T1 t1);
        IEnumerable<T> Get(string id);
        T GetById(int id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        void Create(T t, string id);
        void Update(T t);
        void Delete(int id);
    }
}
