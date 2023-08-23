using System.Collections.Generic;
using DataLayer;

namespace LogicLayer.Interfaces
{
    public interface IVehicleLogic
    {
        Vehicles VSearch(string vreg);
        void VAdd(Vehicles V);
        void VModify(Vehicles V);
        void VDelete(int id);
        List<Vehicles> VList();
    }
}
