using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;

namespace LogicLayer.Interfaces
{
    public interface IVehicleLogic
    {
        Vehicles VSearch(int id);
        void VAdd(string plate, string vRegistration, string brandModel, double capacity, VehiclesCondition condition);
        void VModify(VehiclesCondition condition);
        void VDelete(int id);
        List<Vehicles> AvailableVList(DateTime day);
    }
}
