using System;
using System.Collections.Generic;

namespace SMTP.WebUI.Interfaces
{
    public interface IAppUser<T>
    {
        IEnumerable<T> GetAll();
        T Get(string id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        void Create(T t1);
        void Update(T t1);
        void Delete(string id);
    }
}
