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
        void EAdd(Employees emp); //(int id, string name, string lastname, DateTime dateOB, string user, string pass, string celphone, string address, Roles role, Licenses li);
        void EModify(Employees emp);  //(string pass, string celphone, string address, Roles role, Licenses li);
        void EDelete(Employees emp);
        //List<Employees> AvailableEmploee(DateTime day);
    }
}
