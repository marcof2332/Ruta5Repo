using System.Collections.Generic;

using DataLayer;

namespace LogicLayer.Interfaces
{
    public interface IPackageType
    {
        PackageType PtSearch(int ID);
        void PtAdd(PackageType pt);
        void PtModify(PackageType li);
        void PtDelete(int ID);
        List<PackageType> PtList();
    }
}
