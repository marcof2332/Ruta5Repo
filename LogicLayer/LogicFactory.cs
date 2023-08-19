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
    }
}
