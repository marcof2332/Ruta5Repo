using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;

namespace LogicLayer.Interfaces
{
    public interface IOfficeBranchLogic
    {
        BranchOffices OBSearch(int id);
        void OBAdd(Zones zone, string address, double latitude, double longitude, string phone, DateTime opTime, DateTime clTime);
        void OBModify(Zones zone, string address, double latitude, double longitude, string phone, DateTime opTime, DateTime clTime);
        void OBDelete(int id);
        List<BranchOffices> OBListByZone(Zones zone);
    }
}
