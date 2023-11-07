using System.Collections.Generic;

using DataLayer;

namespace LogicLayer.Interfaces
{
    public interface IPackageTypeLogic
    {
        PackageTypes PtSearch(int ID);
        void PtAdd(PackageTypes pt);
        void PtModify(PackageTypes li);
        void PtDelete(int ID);
        List<PackageTypes> PtList();
    }
}
