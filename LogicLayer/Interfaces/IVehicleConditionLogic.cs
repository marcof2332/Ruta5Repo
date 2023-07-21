using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;

namespace LogicLayer.Interfaces
{
    public interface IVehicleConditionLogic
    {
        VehiclesCondition VCSearch(int id);
        void VCAdd(string name);
        void VCModify(string name);
        void VCDelete(int id);
        List<VehiclesCondition> VCList();
    }
}
