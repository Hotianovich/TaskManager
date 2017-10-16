using System;
using System.Collections.Generic;

namespace SMTP.WebUI.Interfaces
{
    public interface IEmployee<T1>
    {
        IEnumerable<T1> GetAll();
        IEnumerable<T1> Get(string id);
        T1 GetForRegistration(string email);
        IEnumerable<T1> Find(Func<T1, bool> predicate);
        bool Create(T1 t1);
        bool CreateAfterRegistration(string email, string nickName);
        void Update(T1 t1);
        void Delete(int id);
        bool FindEmployee(string email, string id);
    }
}
