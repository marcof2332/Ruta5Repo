using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;

namespace LogicLayer.Interfaces
{
    public interface IShippmentLogic
    {
        #region DropOffPackageMethods
        DropOffPackages DoPSearch(int code);
        void DoPAdd(DropOffPackages DoP);
        void DoPDelete(int ID);
        List<DropOffPackages> DoPList();
        #endregion

        #region HomePickupMethods
        HomePickups HpSearch(int code);
        int HpAdd(HomePickups Hp);
        void HpDelete(int ID);
        List<HomePickups> HpList();
        #endregion

        #region SharedMethods
        void PackageRemoval(int PackageId);
        void ShippmentStageAdd(ShippmentStages ShS);
        #endregion
    }
}
