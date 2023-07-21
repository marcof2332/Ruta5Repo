using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataLayer;

namespace LogicLayer.Interfaces
{
    public interface ICitiesLogic
    {
        Cities CitySearch(int id);
        void CAdd(Cities ct);
        void CModify(Cities ct);
        void CDelete(Cities ct);
        List<Cities> CityListByState(States State);
    }
}
