using System.Collections.Generic;
using DataLayer;

namespace LogicLayer.Interfaces
{
    public interface IVehicleConditionLogic
    {
        VehiclesCondition VCSearch(int id);
        void VCAdd(VehiclesCondition VC);
        void VCModify(VehiclesCondition VC);
        void VCDelete(int ID);
        List<VehiclesCondition> VCList();
    }
}
