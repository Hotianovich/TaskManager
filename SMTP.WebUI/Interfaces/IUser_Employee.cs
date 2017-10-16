using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTP.WebUI.Interfaces
{
    public interface IUser_Employee<T1, T2, T3>
    {
       
        void Create(T2 t2, T3 t3);
        void Delete(T2 t2, T3 t3);
    }
}
