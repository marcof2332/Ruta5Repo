using System.Collections.Generic;
using DataLayer;

namespace LogicLayer.Interfaces
{
    public interface IVehicleLogic
    {
        Vehicles VSearch(int ID);
        void VAdd(Vehicles V);
        void VModify(Vehicles V);
        void VDelete(int ID);
        List<Vehicles> VicenceList();
    }
}
