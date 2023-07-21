using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;

namespace LogicLayer.Interfaces
{
    public interface IShippmentLogic
    {
        DropOffPackage DropOffSearch(int id);
        HomePickup HomePSearch(int id);

        void DropOffAdd(string name, States state);
        void HomePAdd(string name, States state);

        void ShModify(string name, States state);

        void ShDelete(int id);
        List<Shippments> ShippmentListbyDay(DateTime day);
        List<DropOffPackage> DropOffListbyDay(DateTime day);
        List<HomePickup> HomePickupListbyDay(DateTime day);

        void ShippmentStageChange(Shippments Shippment, Stages newStage, Employees stageEmp);
    }
}
