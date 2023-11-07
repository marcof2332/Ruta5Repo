using System.Collections.Generic;
using DataLayer;

namespace LogicLayer.Interfaces
{
    public interface IVehicleConditionLogic
    {
        VehiclesConditions VCSearch(int id);
        void VCAdd(VehiclesConditions VC);
        void VCModify(VehiclesConditions VC);
        void VCDelete(int ID);
        List<VehiclesConditions> VCList();
    }
}
