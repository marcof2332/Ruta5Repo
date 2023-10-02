using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;

namespace LogicLayer.Interfaces
{
    public interface IEmployeeLogic
    {
        Employees EmpLogin(string user, string pass);
        Employees ESearch(int id);
        void EAdd(Employees emp); 
        void EModify(Employees emp); 
        void EDelete(int emp);
        List<Employees> EList();
    }
}
