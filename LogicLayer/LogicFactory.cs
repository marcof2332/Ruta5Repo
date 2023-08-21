using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LogicLayer.Interfaces;

namespace LogicLayer
{
    public class LogicFactory
    {
        #region EmployeeSection
        public static IEmployeeLogic GetEmployeeLogic()
        {
            return (EmployeeLogic.GetInstance());
        }
        public static ILicenseLogic GetLicenceLogic()
        {
            return (LicenceLogic.GetInstance());
        }
        public static IRoleLogic GetRolesLogic()
        {
            return (RolesLogic.GetInstance());
        }
        #endregion

        #region Location
        public static IStateLogic GetStatesLogic()
        {
            return (StatesLogic.GetInstance());
        }
        public static ICitiesLogic GetCityLogic()
        {
            return (CitiesLogic.GetInstance());
        }
        public static IZonesLogic GetZonesLogic()
        {
            return (ZonesLogic.GetInstance());
        }
        public static IOfficeBranchLogic GetBranchLogic()
        {
            return (BranchOfficeLogic.GetInstance());
        }
        #endregion

        #region Packages
        public static IPackageTypeLogic GetPackageTypeLogic()
        {
            return (PackageTypeLogic.GetInstance());
        }
        public static IPackageLogic GetPackageLogic()
        {
            return (PackageLogic.GetInstance());
        }
        #endregion

        #region Customer
        public static ICustomerLogic GetCustomerLogic()
        {
            return (CustomerLogic.GetInstance());
        }
        #endregion

        #region VehiclesSection
        public static IVehicleConditionLogic GetVehicleConditionLogic()
        {
            return (VehicleConditionLogic.GetInstance());
        }
        public static IVehicleLogic GetVehicleLogic()
        {
            return (VehicleLogic.GetInstance());
        }
        #endregion

        #region ShippmentSection
        public static IStageLogic GetStageLogic()
        {
            return (StageLogic.GetInstance());
        }
        public static IShippmentLogic GetShippmentLogic()
        {
            return (ShippmentLogic.GetInstance());
        }
        #endregion
    }
}
