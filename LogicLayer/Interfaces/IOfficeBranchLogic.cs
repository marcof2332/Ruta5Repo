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
        BranchOffices OfficeSearch(int ID);
        void OAdd(BranchOffices br);
        void OModify(BranchOffices br);
        void ODelete(int br);
        List<BranchOffices> OfficeList();
    }
}
