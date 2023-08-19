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
        DropOffPackage DoPSearch(int code);
        void DoPAdd(DropOffPackage DoP);
        void DoPDelete(int ID);
        List<DropOffPackage> DoPList();
        #endregion

        #region HomePickupMethods
        HomePickup HpSearch(int code);
        void HpAdd(HomePickup Hp);
        void HpDelete(int ID);
        List<HomePickup> HpList();
        #endregion

        #region SharedMethods
        void PackageRemoval(int PackageId);
        void ShippmentStageAdd(ShippmentStage ShS);
        #endregion
    }
}
